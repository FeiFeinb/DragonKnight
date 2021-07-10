using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem.Old.Dialogue.Core
{
    /// <summary>
    /// 对话事件类
    /// </summary>
    [System.Serializable]
    public class DialogueEvent
    {
        [System.Serializable] public class DialogueUnityEvent : UnityEvent<string> { }

        public string EventID => _eventID;              // 外部获取
        public DialogueUnityEvent Event => _event;      // 外部获取
        
        [SerializeField] private string _eventID;                                            // 对话事件ID
        
        [SerializeField] private DialogueUnityEvent _event = new DialogueUnityEvent();       // 对话事件
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="dialogueAction">对话触发事件</param>
        public DialogueEvent(string eventID, UnityAction<string> dialogueAction)
        {
            _eventID = eventID;
            _event.AddListener(dialogueAction);
        }
    }

}
