using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class CoreScene : MonoBehaviour
    {
		// PUBLIC MEMBERS

		public bool ContextReady { get; private set; }
		public bool IsActive { get; private set; }
		public SceneContext Context => _context;

		// PRIVATE MEMBERS

		[SerializeField]
		private bool _selfInitialize;
		[SerializeField]
		private SceneContext _context;

		private bool _isInitialized;

		[SerializeField]
		private List<SceneService> _services = new List<SceneService>();

		// PUBLIC METHODS
		public void Initialize()
		{
			if (_isInitialized)
				return;

			PrepareContext();
			CollectServices();

			OnInitialize();

			_isInitialized = true;
		}

		public void PrepareContext()
		{
			if (ContextReady)
				return;

			OnPrepareContext(_context);

			ContextReady = true;
		}


		public void Deinitialize()
		{
			if (!_isInitialized)
				return;

			Deactivate();

			OnDeinitialize();

			_isInitialized = false;
		}

		public IEnumerator Activate()
		{
			if (!_isInitialized)
				yield break;

			yield return OnActivate();

			IsActive = true;
		}

		public void Deactivate()
		{
			if (!IsActive)
				return;

			OnDeactivate();

			IsActive = false;
		}

		public T GetService<T>() where T : SceneService
		{
			for (int i = 0, count = _services.Count; i < count; i++)
			{
				if (_services[i] is T service)
					return service;
			}

			return null;
		}

		public void Quit()
		{
			Deinitialize();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		// MONOBEHAVIOUR

		protected void Awake()
		{
			if (_selfInitialize)
			{
				Initialize();
			}
		}

		protected IEnumerator Start()
		{
			if (!_isInitialized)
				yield break;

			if (_selfInitialize && !IsActive)
			{
				// UI cannot be initialized in Awake, Canvas elements need to Awake first
				AddService(_context.UI);

				yield return Activate();
			}
		}

		protected virtual void Update()
		{
			if (!IsActive)
				return;

			OnTick();
		}

		protected virtual void LateUpdate()
		{
			if (!IsActive)
				return;

			OnLateTick();
		}

		protected void OnDestroy()
		{
			Deinitialize();
		}

		protected void OnApplicationQuit()
		{
			Deinitialize();
		}

		// PROTECTED METHODS

		protected virtual void OnPrepareContext(SceneContext context)
		{

		}

		protected virtual void CollectServices()
		{
			var services = GetComponentsInChildren<SceneService>(true);

			foreach (var service in services)
			{
				AddService(service);
			}
		}

		protected virtual void OnInitialize()
		{
			for (int i = 0; i < _services.Count; i++)
			{
				_services[i].Initialize(this, Context);
			}
		}

		protected virtual IEnumerator OnActivate()
		{
			for (int i = 0; i < _services.Count; i++)
			{
				_services[i].Activate();
			}

			yield break;
		}

		protected virtual void OnTick()
		{
			for (int i = 0, count = _services.Count; i < count; i++)
			{
				_services[i].Tick();
			}
		}

		protected virtual void OnLateTick()
		{
			for (int i = 0, count = _services.Count; i < count; i++)
			{
				_services[i].LateTick();
			}
		}

		protected virtual void OnDeactivate()
		{
			for (int i = 0; i < _services.Count; i++)
			{
				_services[i].Deactivate();
			}
		}

		protected virtual void OnDeinitialize()
		{
			for (int i = 0; i < _services.Count; i++)
			{
				_services[i].Deinitialize();
			}

			_services.Clear();
		}

		public void AddService(SceneService service)
		{
			if (service == null)
			{
				Debug.LogError($"Missing service");
				return;
			}

			if (_services.Contains(service))
			{
				Debug.LogError($"Service {service.gameObject.name} already added.");
				return;
			}

			_services.Add(service);

			if (_isInitialized)
			{
				service.Initialize(this, Context);
			}

			if (IsActive)
			{
				service.Activate();
			}
		}

		public void RemoveService(SceneService service)
		{
			if (service == null)
			{
				Debug.LogError($"Missing service");
				return;
			}

			if (_services.Contains(service) == true)
			{
				_services.Remove(service);
				return;
			}
		}
	}
}