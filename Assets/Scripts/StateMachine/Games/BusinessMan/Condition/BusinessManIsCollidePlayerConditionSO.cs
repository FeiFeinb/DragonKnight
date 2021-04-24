using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManIsCollidePlayerConditionSO", menuName = "FSM/BusinessMan/Condition/IsCollidePlayer")]
    public class BusinessManIsCollidePlayerConditionSO : StateConditionSO<BusinessManIsCollidePlayerCondition> {}
    public class BusinessManIsCollidePlayerCondition : Condition
    {
        private BusinessMan businessMan;

        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            businessMan = stateMachine.GetComponent<BusinessMan>();
        }
        public override bool Statement()
        {
            return businessMan.IsCollidePlayer;
        }
    }
}