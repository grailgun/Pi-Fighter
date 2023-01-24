using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

namespace Untitled
{
    public class PlayerSpawnManager : SceneService
    {
        [SerializeField]
        private Player playerPrefab;
        [SerializeField]
        private Transform playerSpawnPoint;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var player = LeanPool.Spawn(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            Context.PlayerCharacter = player;
        }
    }
}