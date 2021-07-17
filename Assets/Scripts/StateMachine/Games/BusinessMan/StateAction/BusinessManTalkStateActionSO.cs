using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "BusinessManTalkStateActionSO", menuName = "StateMachine/BusinessMan/StateAction/Talk")]
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
            // businessMan.SetTalkState();
            Debug.Log("进入Talk状态");
        }
        public override void OnUpdate()
        {
            Debug.Log("Talk");
        }
    }
}
