using System;
using System.Linq;
using RPG.InventorySystem;
using RPG.Module;
using UnityEngine;

namespace RPG.Character
{
    public class PlayerStatesManager : BaseSingletonWithMono<PlayerStatesManager>
    {
        public float runSprintMulti => currentSpeed / runSpeed;
        public float currentSpeed => isSprint ? sprintSpeed : runSpeed;
        
        public bool isGround;
        public bool isJumping;
        public bool canJump;
        public bool isSprint;
        public bool isFalling;
        [Space]
        public float runSpeed = 4.2f;
        public float sprintSpeed = 6.8f;
        public float jumpSpeed = 5f;
        public float jumpHeight = 6;
        public float gravity = 15;
        [Space]
        [SerializeField] private bool _isDrawGizmos;
        [SerializeField] private Transform _rightFootTrans;
        [SerializeField] private Transform _leftFootTrans;
        [SerializeField] private float _footCheckRadius;


        [SerializeField] private Transform _buttonJumpCheckTrans;
        [SerializeField] private float _jumpCheckRadius;
        
        private readonly Collider[] _footCachedColliders = new Collider[5];
        private readonly Collider[] _jumpCachedColliders = new Collider[5];
        private readonly Collider[] _bottomCachedColliders = new Collider[5];

        [SerializeField] private LayerMask _groundCheckLayer;

        public float _yOffSet;
        private CharacterController _controller;
        private PlayerAnimatorController _animatorController;
        public bool characterIsGrounded => _controller.isGrounded;
        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animatorController = GetComponent<PlayerAnimatorController>();
            
            // 添加装备武器和动画之间的监听逻辑
            foreach (EquipmentInventorySlot handSlot in PlayerInventoryManager.Instance.equipmentObject.
                equipmentInventorySlot.Where(slot => slot.equipmentSlotType == EquipmentSlotType.MainHand 
                                                     || slot.equipmentSlotType == EquipmentSlotType.OffHand))
            {
                handSlot.AddAfterUpdateListener(delegate { SwitchWeapon(); });
            }
        }

        private void Update()
        {
            int sizeRight = Physics.OverlapSphereNonAlloc(_rightFootTrans.position, _footCheckRadius, _footCachedColliders, _groundCheckLayer);
            int sizeLeft = Physics.OverlapSphereNonAlloc(_leftFootTrans.position, _footCheckRadius, _footCachedColliders, _groundCheckLayer);
            
            var position = _buttonJumpCheckTrans.position;
            int sizeCenter = Physics.OverlapSphereNonAlloc(position, _jumpCheckRadius, _jumpCachedColliders, _groundCheckLayer);
            int underfootInRadiusColliderAmount = Physics.OverlapCapsuleNonAlloc(position, position + Vector3.down * 2f, _jumpCheckRadius, _bottomCachedColliders, _groundCheckLayer);
            
            isFalling = underfootInRadiusColliderAmount == 0;
            isGround = sizeRight > 0 || sizeLeft > 0;
            canJump = sizeCenter > 0;
        }
        
        private void OnDrawGizmosSelected()
        {
            if (!_isDrawGizmos) return;
            Gizmos.DrawSphere(_rightFootTrans.position, _footCheckRadius);
            Gizmos.DrawSphere(_leftFootTrans.position, _footCheckRadius);
        }
        
        public void SwitchWeapon()
        {
            var equipmentState = PlayerInventoryManager.Instance.equipmentObject.GetCurrentWeaponState();
            _animatorController.SetWeaponState(equipmentState.Item1, equipmentState.Item2);
        }
    }
}