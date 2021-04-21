using UnityEngine;
using System.Collections.Generic;

namespace RPG.QuestSystem
{
    [CreateAssetMenu(fileName = "New DialogueQuestObject", menuName = "Quest System/DialogueQuest")]
    public class DialogueQuestSO : QuestSO
    {
        public List<DialogueQuestObjective> dialogueQuestObjectives;

        public override IEnumerable<QuestObjective> GetObjectives()
        {
            foreach (DialogueQuestObjective dialogueQuestObjective in dialogueQuestObjectives)
            {
                yield return dialogueQuestObjective;
            }
        }
    }
}