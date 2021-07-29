using RPG.Module;
using UnityEngine;

namespace RPG.Entity
{
    public class PlayerStatesManager : BaseSingletonWithMono<PlayerStatesManager>
    {
        public float walkRunMulti => currentSpeed / moveSpeed;
        public float currentSpeed => isRun ? runSpeed : moveSpeed;
        
        public bool isGround;
        public bool isJumping;
        public bool canJump;
        public bool isRun;
        public bool isJump;
        
        [Space]
        public float moveSpeed = 10;
        public float runSpeed = 20;
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

        [SerializeField] private LayerMask _groundCheckLayer;

        private void Update()
        {
            int sizeRight = Physics.OverlapSphereNonAlloc(_rightFootTrans.position, _footCheckRadius, _footCachedColliders, _groundCheckLayer);
            int sizeLeft = Physics.OverlapSphereNonAlloc(_leftFootTrans.position, _footCheckRadius, _footCachedColliders, _groundCheckLayer);
            int sizeCenter = Physics.OverlapSphereNonAlloc(_buttonJumpCheckTrans.position, _jumpCheckRadius, _jumpCachedColliders, _groundCheckLayer);
            isGround = sizeRight > 0 || sizeLeft > 0;
            canJump = sizeCenter > 0;
        }

        private void OnDrawGizmosSelected()
        {
            if (!_isDrawGizmos) return;
            Gizmos.DrawSphere(_rightFootTrans.position, _footCheckRadius);
            Gizmos.DrawSphere(_leftFootTrans.position, _footCheckRadius);
        }
    }
}