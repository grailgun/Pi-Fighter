using GameLokal.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    [CreateAssetMenu(menuName = "Question/Data")]
    public class QuestionData : CsvScriptableObject<Question>
    {
        
    }

    [System.Serializable]
    public class Question
    {
        public string question;
        public int answer;
    }
}