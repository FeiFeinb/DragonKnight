using RPG.DialogueSystem.Graph;
using RPG.InventorySystem;
using UnityEngine;

namespace DialogueSystem.Graph
{
    public class DialogueHasItemConditionHandler : IDialogueConditionHandler
    {
        public bool HandleCondition(ScriptableObject sourceSO, GameObject obj)
        {
            return PlayerInventoryManager.Instance.HasItem(sourceSO as BaseItemObject);
        }
    }
}