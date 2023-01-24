using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public interface IContextBehaviour
    {
        public SceneContext Context { get; set; }
    }

    public class ContextMonoBehaviour : MonoBehaviour, IContextBehaviour
    {
        public SceneContext Context { get; set; }
    }
}