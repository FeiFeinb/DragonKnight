using RPG.DialogueSystem.Graph;
using RPG.QuestSystem;
using UnityEngine;

namespace DialogueSystem.Graph
{
    public class DialogueCompleteQuestConditionHandler : IDialogueConditionHandler
    {
        public bool HandleCondition(ScriptableObject sourceSO, GameObject obj)
        {
            return PlayerQuestManager.Instance.IsQuestComplete(sourceSO as QuestSO);
        }
    }
}