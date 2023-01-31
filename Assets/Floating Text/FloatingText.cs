using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FloatingText
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro floatingText;
        [SerializeField]
        private float moveSpeed = 1f;
        [SerializeField]
        private float lifeTime = 0.5f;
        private float lifeTimeCounter = 0;

        private Transform textTransform;

        private void Awake()
        {
            textTransform = floatingText.transform;
        }

        public void SetText(string text, Vector3 position)
        {
            floatingText.text = text;
            transform.position = position;

            textTransform.localPosition = Vector3.zero;

            lifeTimeCounter = 0;
        }

        private void Update()
        {
            textTransform.position += Vector3.up * Time.deltaTime * moveSpeed;
            textTransform.position += Vector3.left * Time.deltaTime * moveSpeed * 0.25f;

            lifeTimeCounter += Time.deltaTime;
            if (lifeTimeCounter > lifeTime)
                LeanPool.Despawn(this);
        }
    }
}