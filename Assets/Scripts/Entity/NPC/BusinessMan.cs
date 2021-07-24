using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.QuestSystem;
using RPG.InventorySystem;
using RPG.DialogueSystem.Graph;
using RPG.Utility;
namespace RPG.Entity
{
    [RequireComponent(typeof(Animator), typeof(DialogueNPC), typeof(CapsuleCollider))]
    public class BusinessMan : BaseEntity
    {
        public bool IsCollidePlayer => collidePlayerCheck.IsCollide;
        [SerializeField] private LayerMask targetLayer;             // 对象层级
        private Animator animator;
        private DialogueNPC dialogueNPC;
        private OverlabSphereCheck collidePlayerCheck;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            dialogueNPC = GetComponent<DialogueNPC>();
            collidePlayerCheck = GetComponent<OverlabSphereCheck>();
        }

        private void Update()
        {
            if (collidePlayerCheck.IsCollide && UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                dialogueNPC.StartDialogue();
            }
        }
    }

}
