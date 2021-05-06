using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.InventorySystem;
using RPG.QuestSystem;
namespace RPG.DialogueSystem
{
    public enum DialogueConditionType
    {
        // 拥有任务
        HasQuest,
        // 完成任务
        CompleteQuest,
        // 拥有物品
        HasItem
    }
    [System.Serializable]
    public class DialogueCondition
    {
        public AndCondition[] andConditions;
        public bool Check(IEnumerable<IPredicateEvaluators> iPredicateEvaluators)
        {
            foreach (AndCondition andCondition in andConditions)
            {
                if (!andCondition.Check(iPredicateEvaluators))
                {
                    return false;
                }
            }
            return true;
        }

        [System.Serializable]
        public class AndCondition
        {
            public OrCondition[] orConditions;
            public bool Check(IEnumerable<IPredicateEvaluators> iPredicateEvaluators)
            {
                foreach (OrCondition orCondition in orConditions)
                {
                    if (orCondition.Check(iPredicateEvaluators))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [System.Serializable]
        public class OrCondition
        {
            public DialogueConditionType predicate;         // 条件判断类型
            public ScriptableObject paramSO;                // 判断参数SO    
            // public string parameters;                       // 判断参数
            public bool expectedResult = true;              // 期望结果
            public bool Check(IEnumerable<IPredicateEvaluators> iPredicateEvaluators)
            {
                // 检测是否符合条件
                foreach (var iEvaluator in iPredicateEvaluators)
                {
                    bool? result = iEvaluator.Evaluator(predicate, paramSO);
                    // 无类型Condition 跳过本次循环
                    if (result == null) continue;
                    // 不满足Condition 即不满足条件
                    if (result != expectedResult) return false;
                }
                // 满足条件或无Condition
                return true;
            }
        }

    }
}