using GameLokal.Toolkit;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class Player : Entity, IEventListener<PlayerAttackEvent>
    {
        //Public properties
        public Vector3 AttackPoint { get => transform.position + attackPoint; }
        public Vector2 AttackSize { get => attackSize * 0.5f; }

        [Title("Attack Area")]
        [SerializeField]
        private Vector3 attackPoint;
        [SerializeField]
        private Vector3 attackSize;
        [SerializeField]
        private HurtboxMask attackMask;
        private void OnEnable()
        {
            EventManager.AddListener(this);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(this);
        }

        public void OnEvent(PlayerAttackEvent e)
        {
            TriggerAttack(e.number);
            animator.SetTrigger("Attack");
        }

        private void TriggerAttack(int number)
        {
            var cols = Physics2D.OverlapBoxAll(AttackPoint, attackSize, 0);

            foreach (var col in cols)
            {
                var enemy = col.GetComponent<IHitable>();
                if (enemy != null)
                    if (attackMask.HasFlag((HurtboxMask)enemy.HurtboxType))
                        enemy.Hit(number);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(AttackPoint, attackSize);
        }
#endif
    }
}