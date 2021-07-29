using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerRelaxMoveToIdleConditionSO", menuName = "StateMachine/Player/Condition/RelaxMoveToIdle")]
    public class PlayerRelaxMoveToIdleConditionSO : StateConditionSO<PlayerRelaxMoveToIdleCondition> { }

    public class PlayerRelaxMoveToIdleCondition : Condition
    {
        public override bool Statement()
        {
            return true;
        }
    }
}