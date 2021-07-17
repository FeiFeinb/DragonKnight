using RPG.Entity;
using RPG.QuestSystem;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "DialogueSubmitQuestEventSO", menuName = "Dialogue System/Events/SubmitQuestEventSO")]
    public class DialogueSubmitQuestEventSO : DialogueEventSO
    {
        private BusinessMan _man;
        public override void Init(GameObject obj)
        {
            _man = obj.GetComponent<BusinessMan>();
        }

        public override void RaiseEvent()
        {
            _man.SubmitQuest(_man.Quest);
        }
    }
}