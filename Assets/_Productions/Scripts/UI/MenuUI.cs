using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class MenuUI : SceneUI
    {
        protected override void OnInitializeInternal()
        {
            Debug.Log("Menu UI Initialized");
        }

        protected override void OnActivate()
        {
            Debug.Log("Menu UI Active");
        }
    }
}