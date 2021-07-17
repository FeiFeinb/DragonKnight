using System.Collections;
using System.Collections.Generic;
using DialogueSystem.Old.PlayerControl;
using UnityEngine;
using RPG.DialogueSystem.Graph;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManIsTalkingConditionSO", menuName = "StateMachine/BusinessMan/Condition/IsTalkingCondition")]
    public class BusinessManIsTalkingConditionSO : StateConditionSO<BusinessManIsTalkingCondition> {}
    public class BusinessManIsTalkingCondition : Condition
    {
        public override bool Statement()
        {
            return !OldPlayerDialogueManager.Instance.IsEmpty;
        }
    }
}