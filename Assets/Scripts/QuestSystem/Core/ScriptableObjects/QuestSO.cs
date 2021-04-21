using System.Collections.Generic;
using UnityEngine;
namespace RPG.QuestSystem
{
    public abstract class QuestSO : ScriptableObject
    {
        public string questTitle;                           // 任务标题  
        [TextArea(3, 3)] public string questResume;         // 任务简述
        public QuestReward questReward;                     // 任务奖励
        [TextArea(10, 30)] public string questStory;        // 任务背景

        public abstract IEnumerable<QuestObjective> GetObjectives();
    }
}
