using RPG.QuestSystem;
using UnityEngine;

namespace DialogueSystem.Graph.Events
{
    public class DialogueAcceptQuestConditionHandler : IDialogueEventHandler
    {
        public void HandleEvent(ScriptableObject sourceSO, GameObject obj, string dialogueGraphSOUniqueID)
        {
            PlayerQuestManager.Instance.AddQuest(sourceSO as QuestSO);
        }
    }
}