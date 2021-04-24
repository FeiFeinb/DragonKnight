using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.DialogueSystem;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManIsTalkingConditionSO", menuName = "FSM/BusinessMan/Condition/IsTalkingCondition")]
    public class BusinessManIsTalkingConditionSO : StateConditionSO<BusinessManIsTalkingCondition> {}
    public class BusinessManIsTalkingCondition : Condition
    {
        public override bool Statement()
        {
            return !PlayerDialogueManager.Instance.IsEmpty;
        }
    }
}


