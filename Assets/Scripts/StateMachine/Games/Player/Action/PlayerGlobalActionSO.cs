using RPG.Character;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerGlobalActionSO", menuName = "StateMachine/Player/Action/Global")]
    public class PlayerGlobalActionSO : StateActionSO<PlayerGlobalAction>
    {
    }

    public class PlayerGlobalAction : StateAction
    {
        private PlayerStatesManager _statesManager;
        private PlayerAnimatorController _animatorController;
        private CharacterController _controller;

        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            _controller = stateMachine.GetComponent<CharacterController>();
            _statesManager = PlayerStatesManager.Instance;
            _animatorController = stateMachine.GetComponent<PlayerAnimatorController>();
        }

        public override void OnUpdate()
        {
            if (_statesManager.isGround && !_statesManager.isJumping)
            {
                _statesManager._yOffSet = -_statesManager.gravity;
            }

            _statesManager._yOffSet -= _statesManager.gravity * Time.deltaTime;


            _controller.Move(new Vector3(0, _statesManager._yOffSet, 0) * Time.deltaTime);

            _animatorController.SetIsGrounded(_statesManager.isGround);

            if (!_statesManager.isJumping)
            {
                _animatorController.SetFallTrigger(_statesManager.isFalling);
            }
        }
    }
}