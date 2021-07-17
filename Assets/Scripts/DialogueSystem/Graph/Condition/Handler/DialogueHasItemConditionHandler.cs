using RPG.DialogueSystem.Graph;
using RPG.InventorySystem;
using UnityEngine;

namespace DialogueSystem.Graph
{
    public class DialogueHasItemConditionHandler : DialogueBaseConditionHandler
    {
        public bool HandleCondition(ScriptableObject sourceSO, GameObject obj)
        {
            return PlayerInventoryManager.Instance.HasItem(sourceSO as BaseItemObject);
        }
    }
}