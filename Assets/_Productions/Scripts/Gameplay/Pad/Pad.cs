using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Untitled
{
    public enum PadType
    {
        Number, Accept, Decline
    }

    public class Pad : UIWidget
    {
        [SerializeField]
        private PadType padType;

        [SerializeField, OnValueChanged("UpdatePadUI")]
        [ShowIf("padType", PadType.Number)]
        private int number;

        [SerializeField]
        private TextMeshProUGUI numberText;

        public void TriggerPad()
        {
            Context.GameManager.TriggerPad(padType, number);
        }

#if UNITY_EDITOR
        private void UpdatePadUI()
        {
            numberText?.SetText(number.ToString());
        }
#endif
    }
}