// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
//
// namespace DialogueSystem.Old.Dialogue.Core.Condition
// {
//     /// <summary>
//     /// 对话条件
//     /// </summary>
//     [System.Serializable]
//     public class DialogueCondition
//     {
//         [SerializeField] private AndCondition[] _andConditions;       // 合取范式
//         
//         /// <summary>
//         /// 计算合取范式的结果
//         /// </summary>
//         /// <param name="iPredicateEvaluators">条件判断者</param>
//         /// <returns>计算结果</returns>
//         public bool Check(IEnumerable<IPredicateEvaluators> iPredicateEvaluators)
//         {
//             return _andConditions.All(andCondition => andCondition.Check(iPredicateEvaluators));
//         }
//     }
// }