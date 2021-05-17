using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.DialogueSystem
{
    /// <summary>
    /// 或条件
    /// </summary>
    [System.Serializable]
    public class OrCondition
    {
        [SerializeField] private DialogueConditionType _type;        // 条件判断类型
        
        [SerializeField] private ScriptableObject _paramSO;          // 判断参数SO
        
        [SerializeField] public bool _expectedResult = true;         // 期望结果

        /// <summary>
        /// 计算是否满足条件
        /// </summary>
        /// <param name="iPredicateEvaluators">条件判断者</param>
        /// <returns>计算结果</returns>
        public bool Check(IEnumerable<IPredicateEvaluators> iPredicateEvaluators)
        {
            return iPredicateEvaluators.Select(iEvaluator => iEvaluator.Evaluator(_type, _paramSO)).
                Where(result => result != null).All(result => result == _expectedResult);
        }
    }
}