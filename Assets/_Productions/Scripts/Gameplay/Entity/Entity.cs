using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class Entity : MonoBehaviour
    {
        [Title("General Entity Component")]
        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected GameObject model;
        [SerializeField]
        protected Health health;

        public Health Health { get => health; }

        private void Awake()
        {
            OnPreInitialize();
        }

        private void Start()
        {
            OnInitialize();
        }

        protected virtual void OnPreInitialize() { }
        protected virtual void OnInitialize() { }
    }
}