using CustomExtensions;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
	public class SceneUI : SceneService
	{
		// PUBLIC MEMBERS
		[ShowInInspector]
		public Canvas Canvas   { get; private set; }
		[ShowInInspector]
		public Camera UICamera { get; private set; }

		// SceneUI INTERFACE

		protected UIPageView[] _views;

		protected virtual void OnInitializeInternal()   { }
		protected virtual void OnDeinitializeInternal() { }
		protected virtual void OnTickInternal()         { }

		protected override sealed void OnInitialize()
		{
			Canvas   = GetComponent<Canvas>();
			UICamera = Canvas.worldCamera;
			_views   = GetComponentsInChildren<UIPageView>(true);

			for (int i = 0; i < _views.Length; ++i)
			{
				UIPageView view = _views[i];

				view.Initialize(this, null);
			}

			OnInitializeInternal();
		}

		protected override sealed void OnDeinitialize()
		{
			OnDeinitializeInternal();

			if (_views != null)
			{
				for (int i = 0; i < _views.Length; ++i)
				{
					_views[i].Deinitialize();
				}

				_views = null;
			}
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			Canvas.enabled = true;
		}

		protected override void OnDeactivate()
		{
			base.OnDeactivate();

			if (Canvas != null)
			{
				Canvas.enabled = false;
			}
		}

		protected override sealed void OnTick()
		{
			if (_views != null)
			{
				for (int i = 0; i < _views.Length; ++i)
				{
					UIPageView view = _views[i];
					if (view.IsOpen == true)
					{
                        view.Tick();
					}
				}
			}

			OnTickInternal();
		}
	}
}
