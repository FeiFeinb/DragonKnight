using System;
using System.Linq;
using RPG.DialogueSystem.Graph;
using RPG.QuestSystem;
using UnityEngine;
using UnityTemplateProjects.Test;

namespace DialogueSystem.Graph.Events
{
    public class DialogueProgressQuestConditionHandler : IDialogueEventHandler
    {
        public void HandleEvent(ScriptableObject sourceSO, GameObject obj, string dialogueGraphSOUniqueID)
        {
            DialogueQuestSO questSO = sourceSO as DialogueQuestSO;
            DialogueQuestObjective dialogueSO = questSO.DialogueQuestObjectives.FirstOrDefault(objective => objective.DialogueSO.UniqueID == dialogueGraphSOUniqueID);
            if (dialogueSO == null) throw new Exception("Cant Find DialogueGraphSO In Quest");
            TestDialogueListenerCenter.Instance.Raise(dialogueSO.DialogueSO);
        }
    }
}