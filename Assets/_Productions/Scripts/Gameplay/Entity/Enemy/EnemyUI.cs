using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Untitled
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI questionText;
        
        public void SetQuestion(string question)
        {
            questionText.text = $"{question}";
        }
    }
}