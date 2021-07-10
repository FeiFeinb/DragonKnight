using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem.Old.Dialogue.Core.Condition
{
    /// <summary>
    /// 与条件
    /// </summary>
    [System.Serializable]
    public class AndCondition
    {
        [SerializeField] private OrCondition[] _orConditions;      // 简单合取表达式

        /// <summary>
        /// 计算简单合取表达式的结果
        /// </summary>
        /// <param name="iPredicateEvaluators">条件判断者</param>
        /// <returns>计算结果</returns>
        public bool Check(IEnumerable<IPredicateEvaluators> iPredicateEvaluators)
        {
            return _orConditions.Any(orCondition => orCondition.Check(iPredicateEvaluators));
        }
    }
}