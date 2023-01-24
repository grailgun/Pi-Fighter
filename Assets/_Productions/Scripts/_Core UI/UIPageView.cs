using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;

namespace Untitled
{
    [RequireComponent(typeof(UIView))]
    public abstract class UIPageView : UIWidget
    {
        public bool IsOpen { get; protected set; }

        [ShowInInspector]
        protected UIContainer DoozyUIView;

        protected override void OnInitialize()
        {
            if (DoozyUIView == null)
                DoozyUIView = GetComponent<UIContainer>();

            if (IsInitalized) return;
            if (DoozyUIView == null) return;
            DoozyUIView.OnShowCallback.Event.AddListener(OpenInternal);
            DoozyUIView.OnHideCallback.Event.AddListener(CloseInternal);
        }

        internal void OpenInternal()
        {
            if (IsOpen)
                return;

            IsOpen = true;
            Visible();
            OnOpen();
        }

        internal void CloseInternal()
        {
            if (!IsOpen)
                return;

            IsOpen = false;
            Hidden();
            OnClosed();
        }

        protected virtual void OnOpen() { }
        protected virtual void OnClosed() { }
    }
}