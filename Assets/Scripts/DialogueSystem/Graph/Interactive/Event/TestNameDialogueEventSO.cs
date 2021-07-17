using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "NewName DialogueEventSO", menuName = "Dialogue System/TestName DialogueEventSO")]
    public class TestNameDialogueEventSO : DialogueEventSO
    {
        private string objName;
        public override void Init(GameObject obj)
        {
            objName = obj.name;
        }

        public override void RaiseEvent()
        {
            Debug.Log(objName);
        }
    }
}