using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Untitled
{
    public class GameManager : SceneService
    {
        protected override void OnInitialize()
        {
            Context.PlayerCharacter.Health.OnDead.AddListener(OnPlayerDead);
        }

        private void OnPlayerDead()
        {
            StartCoroutine(RestartGameCoroutine());
        }

        IEnumerator RestartGameCoroutine()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}