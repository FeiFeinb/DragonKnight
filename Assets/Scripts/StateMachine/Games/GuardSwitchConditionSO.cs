using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "New GuardSwitchConditionSO", menuName = "FSM/GuardSwitchConditionSO")]
    public class GuardSwitchConditionSO : StateConditionSO<GuardSwitchCondition> { }
    public class GuardSwitchCondition : Condition
    {
        public override bool Statement()
        {
            return Input.GetKeyDown(KeyCode.H);
        }
    }
}