using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
    public class FootIKMotion : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private Vector3 _leftFootIKVec;
        private Vector3 _rightFootIKVec;
        
        private Vector3 _leftFootVec;
        private Vector3 _rightFootVec;
        
        [SerializeField] private Quaternion _leftFootQuaternion;
        [SerializeField] private Quaternion _rightFootQuaternion;

        [SerializeField] private LayerMask _layer;

        [Range(0, 1), SerializeField] private float _groundOffSet = 0.1f;
        [SerializeField] private float _groundCheckDistance = 0.5f;

        [SerializeField] private bool _isEnableIK;

        private void Update()
        {
            Debug.DrawLine(_leftFootIKVec, _leftFootIKVec + Vector3.down,Color.red);
            Debug.DrawLine(_rightFootIKVec, _rightFootIKVec + Vector3.down, Color.red);
            if (Physics.Raycast(_leftFootIKVec, Vector3.down, out RaycastHit leftHitInfo, _groundCheckDistance, _layer))
            {
                _leftFootVec = leftHitInfo.point + Vector3.up * _groundOffSet;
                _leftFootQuaternion = Quaternion.FromToRotation(Vector3.up, leftHitInfo.normal) * transform.rotation;
                Debug.DrawRay(leftHitInfo.point, leftHitInfo.normal, Color.black);
            }
            
            if (Physics.Raycast(_rightFootIKVec, Vector3.down, out RaycastHit rightHitInfo, _groundCheckDistance, _layer))
            {
                _rightFootVec = rightHitInfo.point + Vector3.up * _groundOffSet;
                _rightFootQuaternion = Quaternion.FromToRotation(Vector3.up, rightHitInfo.normal) * transform.rotation;
                Debug.DrawRay(rightHitInfo.point, rightHitInfo.normal, Color.black);
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _leftFootIKVec = _animator.GetIKPosition(AvatarIKGoal.LeftFoot);
            _rightFootIKVec = _animator.GetIKPosition(AvatarIKGoal.RightFoot);
            
            if (!_isEnableIK) return;
            float leftWeight = _animator.GetFloat("LIK");
            float rightWeight = _animator.GetFloat("RIK");
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftWeight);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightWeight);
            
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightWeight);
            
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootVec);
            _animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootVec);
            
            _animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootQuaternion);
            _animator.SetIKRotation(AvatarIKGoal.RightFoot, _rightFootQuaternion);
        }
    }
    
}
