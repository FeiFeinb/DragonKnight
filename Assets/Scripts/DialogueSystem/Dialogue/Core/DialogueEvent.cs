using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.DialogueSystem
{

    [System.Serializable]
    public class DialogueEvent
    {
        [System.Serializable] public class DialogueUnityEvent : UnityEvent<string> { }
        public string dialogueEventID;      // 事件ID
        public DialogueUnityEvent dialogueEvent = new DialogueUnityEvent();

        public DialogueEvent(string _dialogueEventID, UnityAction<string> _dialogueAction)
        {
            dialogueEventID = _dialogueEventID;
            dialogueEvent.AddListener(_dialogueAction);
        }
    }

}
