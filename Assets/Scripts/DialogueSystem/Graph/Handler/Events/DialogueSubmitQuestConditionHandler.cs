using RPG.QuestSystem;
using UnityEngine;

namespace DialogueSystem.Graph.Events
{
    public class DialogueSubmitQuestConditionHandler : IDialogueEventHandler
    {
        public void HandleEvent(ScriptableObject sourceSO, GameObject obj, string dialogueGraphSOUniqueID)
        {
            PlayerQuestManager.Instance.FinishQuest(sourceSO as QuestSO);
        }
    }
}