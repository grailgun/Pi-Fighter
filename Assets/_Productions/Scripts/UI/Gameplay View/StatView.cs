using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Untitled
{
    public class StatView : UIPageView
    {
        [Title("Health")]
        [SerializeField]
        private TextMeshProUGUI healthText;

        private Health observedHealth;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            observedHealth = Context.PlayerCharacter.Health;
            observedHealth.OnHealthChanged.AddListener(OnPlayerHealthChanged);
        }

        protected override void OnDeinitialize()
        {
            base.OnDeinitialize();
            observedHealth.OnHealthChanged.RemoveListener(OnPlayerHealthChanged);
        }



        private void OnPlayerHealthChanged(float amount)
        {
            healthText?.SetText($"{amount}");
        }
    }
}