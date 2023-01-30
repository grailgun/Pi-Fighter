using GameLokal.Toolkit;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Untitled
{
    public class ControlManager : SceneService, IEventListener<PadEvent>
    {
        [Title("Questions")]
        [ReadOnly]
        private Question currentQuestion;
        [SerializeField]
        private QuestionData questionData;

        //Delegates
        public UnityEvent<string> OnQuestionChanged;
        public UnityEvent OnAnswerRight;
        public UnityEvent OnAnswerWrong;

        protected override void OnInitialize()
        {
            EventManager.AddListener(this);
        }

        protected override void OnDeinitialize()
        {
            EventManager.RemoveListener(this);
        }

        protected override void OnActivate()
        {
            SetQuestion();
        }

        private void TriggerPad(PadType type, int number = 0)
        {
            switch (type)
            {
                case PadType.Number:
                    ProcessNumber(number);
                    break;

                case PadType.Accept:
                    ProcessAccept();
                    break;

                case PadType.Decline:
                    ProcessDecline();
                    break;
            }
        }

        protected virtual void ProcessNumber(int number)
        {
            StartCoroutine(ProcessAnswer(number));            
        }

        private IEnumerator ProcessAnswer(int number)
        {
            yield return new WaitForSeconds(0.1f);
            
            if (number != currentQuestion.answer)
            {
                OnAnswerWrong?.Invoke();
            }
            else
            {
                OnAnswerRight?.Invoke();
                TriggerAttack(number);
            }
        }

        private void TriggerAttack(int number)
        {
            PlayerAttackEvent.Trigger(number);

            SetQuestion();
        }

        private void SetQuestion()
        {
            currentQuestion = questionData.GetQuestion();
            OnQuestionChanged?.Invoke(currentQuestion.question);
        }

        protected virtual void ProcessAccept()
        {
            Debug.Log("Accepted");
        }

        protected virtual void ProcessDecline()
        {
            Debug.Log("Declined");
        }

        public void OnEvent(PadEvent e)
        {
            TriggerPad(e.padType, e.number);
        }
    }
}