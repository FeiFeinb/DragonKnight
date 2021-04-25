using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManIdleStateActionSO", menuName = "StateMachine/BusinessMan/StateAction/Idle")]
    public class BusinessManIdleStateActionSO : StateActionSO<BusinessManIdleStateAction> {}
    public class BusinessManIdleStateAction : StateAction
    {
        private BusinessMan businessMan;
        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            businessMan = stateMachine.GetComponent<BusinessMan>();
        }
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            businessMan.SetIdleState();
            Debug.Log("进入Idle状态");

        }
        public override void OnUpdate()
        {
            Debug.Log("Idle");
        }
    }
}

