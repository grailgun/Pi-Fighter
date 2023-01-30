using GameLokal.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public struct PadEvent
    {
        public PadType padType;
        public int number;

        public PadEvent(PadType type, int number)
        {
            this.padType = type;
            this.number = number;
        }

        public static PadEvent e;
        public static void TriggerEvent(PadType type, int number)
        {
            e.padType = type;
            e.number = number;
            EventManager.TriggerEvent(e);
        }
    }
}