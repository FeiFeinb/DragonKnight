using System.Collections.Generic;
using UnityEngine;

namespace RPG.QuestSystem
{
    [CreateAssetMenu(fileName = "New SeekItemQuestSO", menuName = "Quest System/SeekItemQuestSO")]
    public class SeekItemQuestSO : QuestSO
    {
        public List<SeekItemQuestObjective> SeekItemQuestObjectives;
        public override IEnumerable<QuestObjective> GetObjectives()
        {
            return SeekItemQuestObjectives;
        }
    }
}