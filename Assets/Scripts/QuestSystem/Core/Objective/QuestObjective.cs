using UnityEngine;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class QuestObjective
    {
        public string Description => description;
        public int Target => target;
        public string UniqueID => uniqueID;
        [Tooltip("任务标识ID"), SerializeField] private string uniqueID;
        
        [Tooltip("任务描述"), SerializeField] private string description = default;

        [Tooltip("达成任务所需要的数量"), SerializeField] private int target = -1;

    }
}