using Lean.Pool;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Untitled
{
    public interface IHitable
    {
        public HurtboxType HurtboxType { get; } 
        public void Hit(int damage);
    }

    public class Health : MonoBehaviour, IHitable
    {
        //Public properties
        public float CurrentHealth
        {
            get => currentHealth;
            protected set
            {
                currentHealth = Mathf.Clamp(value, 0, float.MaxValue);
                OnHealthChanged?.Invoke(currentHealth);
            }
        }

        //Private method
        [Title("Health")]
        [SerializeField]
        private float maxHealth = 1;
        private float currentHealth;
        [SerializeField]
        private HurtboxType healthMask;

        [Title("Delegates")]
        public UnityEvent<float> OnHealthChanged;
        public UnityEvent OnDead;

        private void Start()
        {
            ResetHealth();
        }

        public HurtboxType HurtboxType => healthMask;
        public void Hit(int damage)
        {
            CurrentHealth -= damage;
            
            if (CurrentHealth <= 0)
            {
                OnDead?.Invoke();
                LeanPool.Despawn(gameObject);
            }
        }

        public void ResetHealth()
        {
            CurrentHealth = maxHealth;
        }
    }
}