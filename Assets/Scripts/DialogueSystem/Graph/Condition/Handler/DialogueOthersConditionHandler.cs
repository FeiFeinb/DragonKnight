using RPG.DialogueSystem.Graph;
using UnityEngine;

namespace DialogueSystem.Graph
{
    public class DialogueOthersConditionHandler : DialogueBaseConditionHandler
    {
        public bool HandleCondition(ScriptableObject sourceSO, GameObject obj)
        {
            DialogueConditionSO dialogueConditionSO = sourceSO as DialogueConditionSO;
            dialogueConditionSO.Init(obj);
            // [0]为True [1]为False
            return dialogueConditionSO.Judgment();
        }
    }
}