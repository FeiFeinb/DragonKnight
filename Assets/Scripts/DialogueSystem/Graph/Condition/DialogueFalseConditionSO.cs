using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "False", menuName = "Dialogue System/FalseConditionSO")]
    public class DialogueFalseConditionSO : DialogueConditionSO
    {
        public override bool Judgment()
        {
            return false;
        }
    }
}