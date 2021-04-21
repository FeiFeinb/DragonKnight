using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "GuardSwitch2ConditionSO", menuName = "FSM/GuardSwitch2ConditionSO")]
    public class GuardSwitch2ConditionSO : StateConditionSO<GuardSwitch2Condition> {}
    public class GuardSwitch2Condition : Condition
    {
        public override bool Statement()
        {
            return Input.GetKeyDown(KeyCode.G);
        }
    }
}