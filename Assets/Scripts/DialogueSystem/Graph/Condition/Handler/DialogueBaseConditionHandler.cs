using RPG.DialogueSystem.Graph;
using UnityEngine;

namespace DialogueSystem.Graph
{
    /// <summary>
    /// 对话条件处理接口
    /// </summary>
    public interface DialogueBaseConditionHandler
    {
        /// <summary>
        /// 条件处理
        /// </summary>
        /// <param name="sourceSO">源条件SO</param>
        /// <param name="obj">NPC挂载GameObject</param>
        /// <returns></returns>
        bool HandleCondition(ScriptableObject sourceSO, GameObject obj);
    }
}