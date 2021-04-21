using UnityEngine;
using RPG.InventorySystem;
namespace RPG.QuestSystem
{
    class SendItemDialogueQuestObjective : DialogueQuestObjective
    {
        [SerializeField] private BaseItemObject questItemObj;
    }
}