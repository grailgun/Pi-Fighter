using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class ControlManager : SceneService
    {
        [Title("Questions")]
        [ReadOnly]
        private Question currentQuestion;
        [SerializeField]
        private QuestionData questionData;

        [SerializeField]
        private float triggerRate = 0.25f;
        private float triggerRateCounter = 0;

        public void TriggerPad(PadType type, int number = 0)
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
            if (Time.time < triggerRateCounter + triggerRate) return;

            Debug.Log(number);

            PlayerAttackEvent.Trigger(number);

            triggerRateCounter = Time.time;
        }   
        
        protected virtual void ProcessAccept()
        {
            Debug.Log("Accepted");
        }

        protected virtual void ProcessDecline()
        {
            Debug.Log("Declined");
        }
    }
}