using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    [System.Serializable]
    public class SceneContext
    {
        public string Testing = "Context";

        //GAMEPLAY
        public ControlManager GameManager;

        //GENERAL
        public SceneUI UI;

        //CHARACTER
        public Player PlayerCharacter;
    }
}
