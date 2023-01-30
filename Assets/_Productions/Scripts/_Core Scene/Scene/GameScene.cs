using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class GameScene : CoreScene
    {
        protected override IEnumerator OnActivate()
        {
            yield return new WaitForSeconds(0.5f);
            yield return base.OnActivate();
        }
    }
}