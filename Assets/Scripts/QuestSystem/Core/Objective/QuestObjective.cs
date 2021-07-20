using UnityEngine;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class QuestObjective
    {
        public string Description => description;
        public int TargetAmount => targetAmount;
        public string ObjectiveUniqueID => objectiveUniqueID;
        [Tooltip("任务标识ID"), SerializeField] protected string objectiveUniqueID;
        
        [Tooltip("任务描述"), SerializeField] protected string description = default;

        [Tooltip("达成任务所需要的数量"), SerializeField] protected int targetAmount = -1;

    }
}