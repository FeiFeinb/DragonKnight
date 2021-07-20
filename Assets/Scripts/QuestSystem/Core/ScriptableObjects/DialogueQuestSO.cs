using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace RPG.QuestSystem
{
    [CreateAssetMenu(fileName = "New DialogueQuestObject", menuName = "Quest System/DialogueQuest")]
    public class DialogueQuestSO : QuestSO
    {
        public List<DialogueQuestObjective> DialogueQuestObjectives;

        public override IEnumerable<QuestObjective> GetObjectives()
        {
            return DialogueQuestObjectives;
        }
    }
}