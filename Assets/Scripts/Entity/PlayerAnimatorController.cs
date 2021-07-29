using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    public void SetVertical(float vertical)
    {
        _animator.SetFloat(Vertical, vertical);
    }

    public void SetJumpTrigger()
    {
        _animator.SetTrigger(Jump);

    }
    
    public void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGrounded, isGrounded);
    }

}
