using UnityEngine;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class QuestObjective
    {
        public string ObjectiveDescription => objectiveDescription;
        public int ObjectiveTarget => objectiveTarget;
        [SerializeField] private string objectiveDescription = default;             // 任务描述
        [SerializeField] private int objectiveTarget = -1;                          // 达成任务所需要的数
    }
}