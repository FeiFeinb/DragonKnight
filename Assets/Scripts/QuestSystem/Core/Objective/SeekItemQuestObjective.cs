using UnityEngine;
using RPG.InventorySystem;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class SeekItemQuestObjective : QuestObjective
    {
        public BaseItemObject QuestItemObj => questItemObj;
        [SerializeField] private BaseItemObject questItemObj;           // 任务物品
    }
}