using RPG.Character;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerIsFallingConditionSO",
        menuName = "StateMachine/Player/Condition/IsFalling")]
    public class PlayerIsFallingConditionSO : StateConditionSO<PlayerIsFallingCondition>
    {
    }

    public class PlayerIsFallingCondition : Condition
    {
        public override bool Statement()
        {
            return PlayerStatesManager.Instance.isFalling;
        }
    }
}