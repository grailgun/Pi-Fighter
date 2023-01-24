using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Untitled
{
    public static class QuestionExtensions
    {
        public static Question GetQuestion(this QuestionData data)
        {
            var qDatas = data.data;
            return qDatas.Length == 0 ? null : qDatas[Random.Range(0, qDatas.Length)];
        }
    }
}