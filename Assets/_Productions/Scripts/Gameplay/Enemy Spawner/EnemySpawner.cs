using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public class EnemySpawner : SceneService
    {
        [SerializeField]
        private Enemy enemy;

        [SerializeField]
        private Transform spawnPosition;

        [SerializeField]
        private float spawnRate = 1.25f;
        private float spawnRateCounter = 0;

        protected override void OnTick()
        {
            if (Time.time < spawnRateCounter + spawnRate) return;

            LeanPool.Spawn(enemy, spawnPosition.position, Quaternion.identity);
            
            spawnRateCounter = Time.time;
        }
    }
}