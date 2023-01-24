using GameLokal.Toolkit;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public enum HurtboxType
    {
        Player = 1 << 0,
        Enemy  = 1 << 1,
        Ally   = 1 << 2,
    }

    [System.Flags]
    public enum HurtboxMask
    {
        None   = 0,
        Player = 1 << 0,
        Enemy  = 1 << 1,
        Ally   = 1 << 2,
    }

    [RequireComponent(typeof(Collider2D))]
    public class HitBox : MonoBehaviour
    {
        [SerializeField]
        private HurtboxMask hurtBoxMask;
        [ReadOnly]
        private bool isHit = false;

        private void Update()
        {
            if (isHit) return;

            isHit = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if(!LayerHelper.LayerInLayerMask(layerMask, collision.gameObject.layer)) return;

            Debug.Log("Hit Player");

            var hitAble = collision.GetComponentInParent<IHitable>();

            if (hitAble != null)
                if(hurtBoxMask.HasFlag((HurtboxMask)hitAble.HurtboxType))
                    hitAble.Hit(1);

            isHit = true;            
        }
    }
}