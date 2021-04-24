using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManTalkStateActionSO", menuName = "FSM/BusinessMan/StateAction/Talk")]
    public class BusinessManTalkStateActionSO : StateActionSO<BusinessManTalkStateAction> {}
    public class BusinessManTalkStateAction : StateAction
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
            businessMan.SetTalkState();
        }
        public override void OnUpdate()
        {
            Debug.Log("Talk");
        }
    }
}
