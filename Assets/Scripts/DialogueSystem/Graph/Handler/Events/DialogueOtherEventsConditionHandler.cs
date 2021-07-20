using RPG.DialogueSystem.Graph;
using UnityEngine;

namespace DialogueSystem.Graph.Events
{
    public class DialogueOtherEventsConditionHandler : IDialogueEventHandler
    {
        public void HandleEvent(ScriptableObject sourceSO, GameObject obj, string dialogueGraphSOUniqueID)
        { 
            (sourceSO as DialogueEventSO)?.RaiseEvent();
        }
    }
}