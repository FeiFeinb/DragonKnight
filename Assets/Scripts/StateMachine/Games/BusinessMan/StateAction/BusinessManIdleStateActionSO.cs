using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
using RPG.Utility;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManIdleStateActionSO", menuName = "StateMachine/BusinessMan/StateAction/Idle")]
    public class BusinessManIdleStateActionSO : StateActionSO<BusinessManIdleStateAction> {}
    public class BusinessManIdleStateAction : StateAction
    {
        private JudgmentOverlapSectorCheck overlapCheck;
        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            overlapCheck = stateMachine.GetComponent<JudgmentOverlapSectorCheck>();
        }
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("进入Idle状态");
        }
        public override void OnUpdate()
        {
            Debug.Log("Idle");
        }
    }
}

