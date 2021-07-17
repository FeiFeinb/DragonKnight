using UnityEngine;
using UnityTemplateProjects.Test;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "DialogueProgressQuestEventSO", menuName = "Dialogue System/Events/ProgressQuestEventSO")]
    public class DialogueProgressQuestEventSO : DialogueEventSO
    {
        public override void RaiseEvent()
        {
            TestDialogueListenerCenter.Instance.Raise("KELM01");
        }
    }
}