using RPG.Entity;
using RPG.QuestSystem;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "DialogueAcceptQuestEventSO", menuName = "Dialogue System/Events/AcceptQuestEventSO")]
    public class DialogueAcceptQuestEventSO : DialogueEventSO
    {
        private BusinessMan _man;
        public override void Init(GameObject obj)
        {
            _man = obj.GetComponent<BusinessMan>();
        }

        public override void RaiseEvent()
        {
            _man.StartQuest(_man.Quest);
        }
    }
}