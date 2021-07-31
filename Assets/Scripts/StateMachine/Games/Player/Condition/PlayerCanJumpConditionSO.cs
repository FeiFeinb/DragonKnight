using RPG.Character;
using RPG.InputSystyem;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerCanJumpConditionSO",
        menuName = "StateMachine/Player/Condition/CanJump")]
    public class PlayerCanJumpConditionSO : StateConditionSO<PlayerCanJumpCondition>
    {
    }

    public class PlayerCanJumpCondition : Condition
    {
        private PlayerStatesManager _statesManager;

        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            _statesManager = PlayerStatesManager.Instance;
        }

        public override bool Statement()
        {
            return _statesManager.canJump && InputManager.Instance.inputData.GetNormalKeyDown(KeyActionType.Jump) &&
                   !_statesManager.isJumping;
        }
    }
}