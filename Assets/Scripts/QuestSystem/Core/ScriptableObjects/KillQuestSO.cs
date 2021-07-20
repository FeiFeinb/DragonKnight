using System.Collections.Generic;
using UnityEngine;
namespace RPG.QuestSystem
{
    [CreateAssetMenu(fileName = "New KillQuestObject", menuName = "Quest System/KillQuest")]

    public class KillQuestSO : QuestSO
    {
        public List<KillQuestObjective> KillQuestObjectives;        // 任务目标

        public override IEnumerable<QuestObjective> GetObjectives()
        {
            return KillQuestObjectives;
        }
    }
}