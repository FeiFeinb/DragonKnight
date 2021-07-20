using System.Collections.Generic;
using UnityEngine;
namespace RPG.QuestSystem
{
    public abstract class QuestSO : ScriptableObject
    {
        public string QuestUniqueID;                    // 任务标识ID
        public string Title;                            // 任务标题  
        [TextArea(10, 30)] public string Resume;          // 任务简述
        public QuestReward Reward;                      // 任务奖励
        [TextArea(10, 30)] public string Story;         // 任务背景

        public abstract IEnumerable<QuestObjective> GetObjectives();
    }
}
