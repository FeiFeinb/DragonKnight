using UnityEngine;
namespace RPG.DialogueSystem
{
    public interface IPredicateEvaluators
    {
        /// <summary>
        /// 条件结果评估
        /// </summary>
        /// <param name="type">对话条件类型</param>
        /// <param name="paramSO">判断参数SO</param>
        /// <returns>评估结果</returns>
        bool? Evaluator(DialogueConditionType type, ScriptableObject paramSO);
    }
}