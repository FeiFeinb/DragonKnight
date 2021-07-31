using RPG.Character;
using RPG.InputSystyem;
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

        private float forwardSpeed;
        private bool isRun;
        public override void OnStateEnter()
        {
            _statesManager._yOffSet = _statesManager.jumpHeight;
            _statesManager.isJumping = true;
            _animatorController.SetJumpTrigger();
            forwardSpeed = InputManager.Instance.inputData.GetAxisKeyValue(KeyActionType.MoveVertical);
            isRun = InputManager.Instance.inputData.GetNormalKeyDown(KeyActionType.Run);
        }

        public override void OnUpdate()
        {
            float jumpSpeed =
                isRun ? _statesManager.jumpSpeed * _statesManager.runSprintMulti : _statesManager.jumpSpeed; 
            _controller.Move(_statesManager.transform.forward * (forwardSpeed * Time.deltaTime * jumpSpeed));
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