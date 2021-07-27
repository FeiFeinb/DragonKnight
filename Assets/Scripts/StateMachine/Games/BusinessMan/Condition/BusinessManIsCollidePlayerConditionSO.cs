using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
using RPG.Utility;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManIsCollidePlayerConditionSO", menuName = "StateMachine/BusinessMan/Condition/IsCollidePlayer")]
    public class BusinessManIsCollidePlayerConditionSO : StateConditionSO<BusinessManIsCollidePlayerCondition> {}
    public class BusinessManIsCollidePlayerCondition : Condition
    {
        private JudgmentOverlapSectorCheck overlapCheck;

        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            overlapCheck = stateMachine.GetComponentInChildren<JudgmentOverlapSectorCheck>();
        }
        public override bool Statement()
        {
            return overlapCheck.isCollide;
        }
    }
}