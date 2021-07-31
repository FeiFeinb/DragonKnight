using RPG.Character;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerJumpActionSO", menuName = "StateMachine/Player/Action/Jump")]
    public class PlayerJumpActionSO : StateActionSO<PlayerJumpAction>
    {
    }

    public class PlayerJumpAction : StateAction
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

        public override void OnStateEnter()
        {
            _statesManager._yOffSet = _statesManager.jumpHeight;
            _statesManager.isJumping = true;
            _animatorController.SetJumpTrigger();
        }

        public override void OnUpdate()
        {
            _controller.Move(new Vector3(0, _statesManager._yOffSet, 0) * Time.deltaTime);

            if (_statesManager.isJumping)
            {
                _statesManager.isJumping = !_statesManager.characterIsGrounded;
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _statesManager._yOffSet = 0;
            _statesManager.isJumping = false;
        }
    }
}