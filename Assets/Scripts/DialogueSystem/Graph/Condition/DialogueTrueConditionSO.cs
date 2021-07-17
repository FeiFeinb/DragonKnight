using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "True", menuName = "Dialogue System/TrueConditionSO")]
    public class DialogueTrueConditionSO : DialogueConditionSO
    {
        public override bool Judgment()
        {
            return true;
        }
    }
}