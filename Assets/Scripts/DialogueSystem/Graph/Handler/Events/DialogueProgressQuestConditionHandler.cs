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
            if (questSO == null) throw new NullReferenceException("无法找到对应的QuestSO");
            DialogueQuestObjective dialogueSO = questSO.DialogueQuestObjectives.FirstOrDefault(objective => objective.DialogueSO.UniqueID == dialogueGraphSOUniqueID);
            if (dialogueSO == null) throw new NullReferenceException($"无法在{questSO.name}中找到对应的DialogueSO");
            TestDialogueListenerCenter.Instance.Raise(dialogueSO.DialogueSO);
        }
    }
}