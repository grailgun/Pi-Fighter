using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class GamepadView : UIPageView
    {
        [SerializeField]
        private Pad[] pads;



#if UNITY_EDITOR
        [Button]
        private void SearchPad()
        {
            pads = GetComponentsInChildren<Pad>();
        }
#endif
    }
}