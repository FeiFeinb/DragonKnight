using System;
using RPG.Entity;
using RPG.InputSystyem;
using RPG.Character;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerGroundMoveActionSO", menuName = "StateMachine/Player/Action/GroundMove")]
    public class PlayerGroundMoveActionSO : StateActionSO<PlayerGroundMoveAction>
    {
    }

    public class PlayerGroundMoveAction : StateAction
    {
        private Transform _transform;
        private CharacterController _controller;
        private PlayerAnimatorController _animatorController;
        private Transform _cameraTrans;
        private PlayerStatesManager _statesManager;


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
            
            _statesManager.isSprint = InputManager.Instance.inputData.GetNormalKeyDown(KeyActionType.Run);
            
            Move(horizontal, vertical);
        }

        private void Move(float horizontal, float vertical)
        {
            Vector2 circleMappingVec = new Vector2(horizontal * Mathf.Sqrt(1 - vertical * vertical / 2f),
                vertical * Mathf.Sqrt(1 - horizontal * horizontal / 2f));
            if (circleMappingVec.magnitude < 0.01) return;

            Vector3 moveDir = circleMappingVec.x * _cameraTrans.right +
                              circleMappingVec.y * new Vector3(_cameraTrans.forward.x, 0, _cameraTrans.forward.z);
                _controller.Move(moveDir * (_statesManager.currentSpeed * Time.deltaTime));

            // 设置旋转
            Vector3 turnDir = _transform.InverseTransformDirection(moveDir);
            var m_TurnAmount = Mathf.Atan2(turnDir.x, turnDir.z);
            if (circleMappingVec.magnitude < 0.1 && Mathf.Abs(m_TurnAmount) > 1) return;
            float turnSpeed = Mathf.Lerp(360, 540, turnDir.z);
            _transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
            // 设置动画
            _animatorController.SetVertical(circleMappingVec.magnitude * _statesManager.runSprintMulti);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _animatorController.SetVertical(0);
        }
    }
}