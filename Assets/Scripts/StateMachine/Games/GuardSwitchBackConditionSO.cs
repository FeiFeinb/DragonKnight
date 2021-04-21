using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "New GuardSwitchBackConditionSO", menuName = "FSM/GuardSwitchBackConditionSO")]
    public class GuardSwitchBackConditionSO : StateConditionSO<GuardSwitchBackCondition> { }
    public class GuardSwitchBackCondition : Condition
    {
        public override bool Statement()
        {
            return Input.GetKeyDown(KeyCode.J);
        }
    }
}