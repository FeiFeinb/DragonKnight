using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "NewPos DialogueEventSO", menuName = "Dialogue System/TestPos DialogueEventSO")]
    public class TestPosDialogueEventSO : DialogueEventSO
    {
        private Vector3 pos;
        public override void Init(GameObject obj)
        {
            pos = obj.transform.position;
        }

        public override void RaiseEvent()
        {
            Debug.Log(pos);
        }
    }
}