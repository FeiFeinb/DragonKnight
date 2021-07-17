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
        public QuestSO Quest;
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
            if (collidePlayerCheck.IsCollide && Input.GetKeyDown(KeyCode.E))
            {
                dialogueNPC.StartDialogue();
            }
        }

        public void StartQuest(QuestSO quest)
        {
            PlayerQuestManager.Instance.AddQuest(quest);
        }

        public void SubmitQuest(QuestSO quest)
        {
            // 判断任务是否发送成功
            if (PlayerQuestRewardManager.Instance.SendReward(quest.questReward))
            {
                Debug.Log(string.Concat("完成了任务", quest.questTitle));
                // 任务提交完成 移除任务
                PlayerQuestManager.Instance.RemoveQuest(quest);
            }
        }
    }

}
