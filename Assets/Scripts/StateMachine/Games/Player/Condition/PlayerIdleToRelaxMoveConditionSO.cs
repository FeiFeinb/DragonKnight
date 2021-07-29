using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerIdleToRelaxMoveConditionSO", menuName = "StateMachine/Player/Condition/IdleToRelaxMove")]
    public class PlayerIdleToRelaxMoveConditionSO : StateConditionSO<PlayerIdleToRelaxMoveCondition> {}

    public class PlayerIdleToRelaxMoveCondition : Condition
    {
        public override bool Statement()
        {
            return true;
        }
    }
}