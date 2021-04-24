using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Entity;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "GetKeyDownEConditionSO", menuName = "FSM/Condition/GetKeyDownE")]
    public class GetKeyDownEConditionSO : StateConditionSO<GetKeyDownECondition> {}
    public class GetKeyDownECondition : Condition
    {
        public override bool Statement()
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
}