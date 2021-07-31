using RPG.Character;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerIsGroundConditionSO",
        menuName = "StateMachine/Player/Condition/IsGround")]
    public class PlayerIsGroundConditionSO : StateConditionSO<PlayerIsGroundCondition>
    {
    }

    public class PlayerIsGroundCondition : Condition
    {
        public override bool Statement()
        {
            return PlayerStatesManager.Instance.isGround;
        }
    }
}