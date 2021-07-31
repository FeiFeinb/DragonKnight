using System;
using System.Collections;
using System.Collections.Generic;
using RPG.InventorySystem;
using UnityEditor;
using UnityEngine;

namespace RPG.Character
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int OffHandType = Animator.StringToHash("OffHandType");
        private static readonly int MainHandType = Animator.StringToHash("MainHandType");
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        public void SetVertical(float vertical)
        {
            _animator.SetFloat(Vertical, vertical);
        }

        public void SetFallTrigger(bool isFalling)
        {
            _animator.SetBool(IsFalling, isFalling);
        }
        
        public void SetJumpTrigger()
        {
            _animator.SetTrigger(Jump);
        }

        public void SetIsGrounded(bool isGrounded)
        {
            _animator.SetBool(IsGrounded, isGrounded);
        }

        public void SetWeaponState(EquipmentType mainHandType, EquipmentType offHandType)
        {
            _animator.SetInteger(MainHandType, mainHandType.ToAnimatorInt());
            _animator.SetInteger(OffHandType, offHandType.ToAnimatorInt());
        }
    }
}

