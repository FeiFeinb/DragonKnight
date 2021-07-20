using UnityEngine;

namespace DialogueSystem.Graph.Events
{
    public interface IDialogueEventHandler
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="sourceSO">源SO</param>
        /// <param name="obj">对话任务所在GameObject</param>
        /// <param name="dialogueGraphSOUniqueID">所在对话SO的UniqueID</param>
        void HandleEvent(ScriptableObject sourceSO, GameObject obj, string dialogueGraphSOUniqueID);
    }
}