using System;
using RPG.Entity;
using RPG.InputSystyem;
using RPG.InventorySystem;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerRelaxMoveStateActionSO", menuName = "StateMachine/Player/Action/RelaxMove")]
    public class PlayerRelaxMoveStateActionSO : StateActionSO<PlayerRelaxMoveStateAction>
    {
    }

    public class PlayerRelaxMoveStateAction : StateAction
    {
        private Transform _transform;
        private CharacterController _controller;
        private PlayerAnimatorController _animatorController;
        private Transform _cameraTrans;
        private PlayerStatesManager _statesManager;

        private float _yOffSet;

        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            _controller = stateMachine.GetComponent<CharacterController>();
            _transform = stateMachine.GetComponent<Transform>();
            _animatorController = stateMachine.GetComponent<PlayerAnimatorController>();
            _statesManager = PlayerStatesManager.Instance;
            Camera mainCamera = Camera.main;
            if (!mainCamera) throw new NullReferenceException("无法找到场景摄像机");
            _cameraTrans = mainCamera.transform;
        }

        public override void OnUpdate()
        {
            float horizontal = InputManager.Instance.inputData.GetAxisKeyValue(KeyActionType.MoveHorizontal);
            float vertical = InputManager.Instance.inputData.GetAxisKeyValue(KeyActionType.MoveVertical);
            _statesManager.isJump = InputManager.Instance.inputData.GetNormalKeyDown(KeyActionType.Jump);
            _statesManager.isRun = InputManager.Instance.inputData.GetNormalKeyDown(KeyActionType.Run);
            
            Move(horizontal, vertical);
            Fall();
        }

        private void Move(float horizontal, float vertical)
        {
            Vector2 circleMappingVec = new Vector2(horizontal * Mathf.Sqrt(1 - vertical * vertical / 2f),
                vertical * Mathf.Sqrt(1 - horizontal * horizontal / 2f));
            if (circleMappingVec.magnitude < 0.01) return;

            Vector3 moveDir = circleMappingVec.x * _cameraTrans.right + circleMappingVec.y *
                Vector3.Scale(_cameraTrans.forward, new Vector3(1, 0, 1)).normalized;
            _controller.Move(moveDir * (_statesManager.currentSpeed * Time.deltaTime));

            // 设置旋转
            Vector3 turnDir = _transform.InverseTransformDirection(moveDir);
            var m_TurnAmount = Mathf.Atan2(turnDir.x, turnDir.z);
            float turnSpeed = Mathf.Lerp(180, 360, turnDir.z);
            _transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);

            // 设置动画
            _animatorController.SetVertical(circleMappingVec.magnitude * _statesManager.walkRunMulti);
        }


        private void Fall()
        {
            if (_statesManager.isGround && !_statesManager.isJumping)
            {
                _yOffSet = 0;
            }

            if (_statesManager.canJump && _statesManager.isJump && !_statesManager.isJumping)
            {
                _yOffSet = _statesManager.jumpHeight;
                _statesManager.isJumping = true;
                _animatorController.SetJumpTrigger();
            }

            _yOffSet -= _statesManager.gravity * Time.deltaTime;
            _controller.Move(new Vector3(0, _yOffSet, 0) * Time.deltaTime);
            
            if (_statesManager.isJumping)
            {
                _statesManager.isJumping = !_controller.isGrounded;
            }

            _animatorController.SetIsGrounded(_statesManager.isGround);
        }
    }
}