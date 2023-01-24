using GameLokal.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public struct PlayerAttackEvent
    {
        public int number;
        
        public PlayerAttackEvent(int number)
        {
            this.number = number;
        }

        public static PlayerAttackEvent e;
        public static void Trigger(int number)
        {
            e.number = number;
            EventManager.TriggerEvent(e);
        }
    }
}