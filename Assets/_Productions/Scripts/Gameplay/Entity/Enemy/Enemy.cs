using Lean.Pool;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Untitled
{
    public class Enemy : Entity, IPoolable, IHitable
    {
        //Public properties        
        [Title("Enemy Data")]
        [SerializeField]
        private EnemyData data;
        [ReadOnly]
        private Question currentQuestion;
        [SerializeField]
        private QuestionData questionData;

        //Delegates
        public UnityEvent<string> OnQuestionChanged;

        private Vector2 screenBounds;

        [Title("Hit Mask")]
        [SerializeField]
        private HurtboxType hurtBoxType;
        public HurtboxType HurtboxType => hurtBoxType;

        private void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            transform.position += Vector3.left * Time.deltaTime * data.enemySpeed;

        }

        private void LateUpdate()
        {
            if (transform.position.x < (-screenBounds.x - 1)) LeanPool.Despawn(this);
        }

        public void Hit(int number)
        {
            LeanPool.Despawn(this);
        }

        public void OnSpawn()
        {
            //SetQuestion();
        }

        private void SetQuestion()
        {
            currentQuestion = questionData.GetQuestion();
            OnQuestionChanged?.Invoke(currentQuestion.question);
        }

        public void OnDespawn()
        {
            
        }        
    }
}