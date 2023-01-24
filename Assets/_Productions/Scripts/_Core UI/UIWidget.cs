using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
	public abstract class UIWidget : UIBehaviour
	{
		// PUBLIC MEMBERS

		public bool IsVisible { get; private set; }

		[SerializeField]
		private bool visibleOnInitialize = false;
		[SerializeField]
		private bool visibleOnEnable = false;
		[SerializeField]
		private bool hiddenOnDisable = false;

		// PROTECTED MEMBERS

		protected bool          IsInitalized { get; private set; }
		protected SceneUI       SceneUI      { get; private set; }
		protected SceneContext  Context      { get { return SceneUI.Context; } }
		protected UIWidget      Owner        { get; private set; }

		protected List<UIWidget> _children = new List<UIWidget>();

		internal void Initialize(SceneUI sceneUI, UIWidget owner)
		{
			if (IsInitalized)
				return;

			SceneUI = sceneUI;
			Owner = owner;

			_children.Clear();
			GetChildWidgets(transform, _children);

			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Initialize(sceneUI, this);
			}

			OnInitialize();

			IsInitalized = true;

			if (visibleOnInitialize)
				Visible();
		}

		internal void Deinitialize()
		{
			if (!IsInitalized)
				return;

			Hidden();

			OnDeinitialize();

			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Deinitialize();
			}

			_children.Clear();

			IsInitalized = false;

			SceneUI = null;
			Owner = null;
		}

		internal void Visible()
		{
			if (!IsInitalized)
				return;

			if (IsVisible)
				return;

			if (!gameObject.activeSelf)
				return;

			IsVisible = true;

			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Visible();
			}

			OnVisible();
		}

		internal void Hidden()
		{
			if (!IsVisible)
				return;

			IsVisible = false;

			OnHidden();

			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Hidden();
			}
		}

		internal void Tick()
		{
			if (!IsInitalized)
				return;

			if (!IsVisible)
				return;

			OnTick();

			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Tick();
			}
		}

		public void AddChild(UIWidget widget)
		{
			if (widget == null || widget == this)
				return;

			if (_children.Contains(widget))
			{
				Debug.LogError($"Widget {widget.name} is already added as child of {name}");
				return;
			}

			_children.Add(widget);

			widget.Initialize(SceneUI, this);
		}

		public void RemoveChild(UIWidget widget)
		{
			int childIndex = _children.IndexOf(widget);

			if (childIndex < 0)
			{
				Debug.LogError($"Widget {widget.name} is not child of {name} and cannot be removed");
				return;
			}

			widget.Deinitialize();

			_children.RemoveAt(childIndex);
		}

		// MONOBEHAVIOR

		protected virtual void OnEnable()
		{
			if(visibleOnEnable)
				Visible();
		}

		protected virtual void OnDisable()
		{
			if(hiddenOnDisable)
				Hidden();
		}

		// UIWidget INTERFACE

		protected virtual void OnInitialize() { }
		protected virtual void OnDeinitialize() { }
		protected virtual void OnVisible() { }
		protected virtual void OnHidden() { }
		protected virtual void OnTick() { }

		// PRIVATE MEMBERS

		private static void GetChildWidgets(Transform transform, List<UIWidget> widgets)
		{
			foreach (Transform child in transform)
			{
				var childWidget = child.GetComponent<UIWidget>();

				if (childWidget != null)
				{
					widgets.Add(childWidget);
				}
				else
				{
					// Continue searching deeper in hierarchy
					GetChildWidgets(child, widgets);
				}
			}
		}
	}
}
