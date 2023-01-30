using GameLokal.Toolkit;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Untitled
{
    public class QuestionView : UIPageView, IEventListener<PadEvent>
    {
        [SerializeField]
        private TextMeshProUGUI questionText;
        [SerializeField]
        private TextMeshProUGUI answerText;

        [SerializeField]
        private Image answerImage;
        [SerializeField]
        private Color answerRight;
        [SerializeField]
        private Color answerWrong;
        [SerializeField]
        private Color answerIdle;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SetQuestionDisplay("???");
            SetAnswerDisplay("...");
            SetAnswerColor(answerIdle);

            Context.ControlManager.OnQuestionChanged.AddListener(SetQuestionDisplay);
            Context.ControlManager.OnAnswerRight.AddListener(OnAnswerRight);
            Context.ControlManager.OnAnswerWrong.AddListener(OnAnswerWrong);

            EventManager.AddListener(this);
        }

        private void OnAnswerRight()
        {
            StartCoroutine(SetAnswerEffect(true));            
        }

        private void OnAnswerWrong()
        {
            StartCoroutine(SetAnswerEffect(false));
        }

        private IEnumerator SetAnswerEffect(bool isRight)
        {
            SetAnswerColor(isRight ? answerRight : answerWrong);
            
            yield return new WaitForSeconds(0.2f);

            SetAnswerDisplay("...");
            SetAnswerColor(answerIdle);
        }

        protected override void OnDeinitialize()
        {
            base.OnDeinitialize();

            Context.ControlManager.OnQuestionChanged.RemoveListener(SetQuestionDisplay);
            Context.ControlManager.OnAnswerRight.RemoveListener(OnAnswerRight);
            Context.ControlManager.OnAnswerWrong.RemoveListener(OnAnswerWrong);

            EventManager.RemoveListener(this);
        }

        public void OnEvent(PadEvent e)
        {
            SetAnswerDisplay($"{e.number}");
            SetAnswerColor(answerIdle);
        }

        private void SetAnswerDisplay(string number)
        {
            answerText?.SetText(number);
        }

        private void SetQuestionDisplay(string question)
        {
            questionText?.SetText(question);
        }

        private void SetAnswerColor(Color color)
        {
            answerImage.color = color;
        }
    }
}