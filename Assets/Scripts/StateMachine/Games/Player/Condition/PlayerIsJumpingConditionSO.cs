using RPG.Character;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerIsJumpingConditionSO",
        menuName = "StateMachine/Player/Condition/IsJumping")]
    public class PlayerIsJumpingConditionSO : StateConditionSO<PlayerIsJumpingCondition>
    {
    }

    public class PlayerIsJumpingCondition : Condition
    {
        public override bool Statement()
        {
            return PlayerStatesManager.Instance.isJumping;
        }
    }
}