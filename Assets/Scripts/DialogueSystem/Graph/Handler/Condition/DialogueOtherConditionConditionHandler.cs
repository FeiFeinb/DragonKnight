using RPG.DialogueSystem.Graph;
using UnityEngine;

namespace DialogueSystem.Graph
{
    public class DialogueOtherConditionConditionHandler : IDialogueConditionHandler
    {
        public bool HandleCondition(ScriptableObject sourceSO, GameObject obj)
        {
            DialogueConditionSO dialogueConditionSO = sourceSO as DialogueConditionSO;
            // [0]为True [1]为False
            return dialogueConditionSO.Judgment();
        }
    }
}