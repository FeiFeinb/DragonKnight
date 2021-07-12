using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Old.Dialogue.Core;
using DialogueSystem.Old.Dialogue.ScriptableObjects;
using RPG.DialogueSystem;
using RPG.Module;
using UnityEngine;

namespace DialogueSystem.Old.PlayerControl
{
    public class OldPlayerDialogueManager : BaseSingletonWithMono<OldPlayerDialogueManager>
    {
        public DialogueCharacterInfoSO DialoguePlayerInfo => dialoguePlayerInfo;  // 外部获取
        public bool IsPlayerChoice => isPlayerChoice;                           // 外部获取
        public bool IsLastNode => currentDialogueNode.Children.Count == 0;      // 外部获取
        public bool IsEmpty => currentDialogue == null;                         // 外部获取
        public DialogueNPC CurrentDialogueNPC => currentDialogueNPC;            // 外部获取
        public string NPCName => currentDialogueNPC.NPCInfo.CharacterName;      // 外部获取
        public Sprite NPCHeadSculpture => currentDialogueNPC.NPCInfo.HeadSculpture;
        public DialogueNodeSO CurrentDialogueNode => currentDialogueNode;     // 外部获取
        public Action<DialogueSO> onDialogueChange;                         // 对话物体改变时
        public Action onDialogueNodeChange;                     // 对话节点改变时
        [SerializeField] private DialogueCharacterInfoSO dialoguePlayerInfo;  // 玩家信息
        private DialogueSO currentDialogue;                       // 当前对话物体
        private DialogueNodeSO currentDialogueNode;               // 当前节点
        private DialogueNPC currentDialogueNPC;                 // 当前对话NPC
        private bool isPlayerChoice;                            // 是否位于玩家选择界面
        
        public void SetDialogue(DialogueNPC _currentNPC)
        {
            currentDialogue = _currentNPC.Content;
            currentDialogueNode = currentDialogue.GetRootNode();
            currentDialogueNPC = _currentNPC;
            isPlayerChoice = false;
            onDialogueChange?.Invoke(currentDialogue);
        }
        public void ResetDialogue()
        {
            currentDialogue = null;
            currentDialogue = null;
            currentDialogueNPC = null;
            isPlayerChoice = false;
            onDialogueChange?.Invoke(currentDialogue);
        }
        public IEnumerable<DialogueNodeSO> GetChoice()
        {
            foreach (DialogueNodeSO playerChild in ConditionFitter(currentDialogue.GetInteractiveChildren(currentDialogueNode)))
            {
                yield return playerChild;
            }
        }
        public IEnumerable<IPredicateEvaluators> GetIPredicateEvaluators()
        {
            // 获取所有判断接口
            return GetComponents<IPredicateEvaluators>();
        }
        private IEnumerable<DialogueNodeSO> ConditionFitter(IEnumerable<DialogueNodeSO> dialogueNodes)
        {
            // TODO: 改善遍历逻辑
            // 筛选符合Condition的节点
            foreach (DialogueNodeSO node in dialogueNodes)
            {
                if (node.Condition.Check(GetIPredicateEvaluators()))
                {
                    yield return node;
                }
            }
        }
        public void Next()
        {
            // TODO: 将整个对话系统的运行方式改为状态模式
            // TODO: 当玩家具有任务时才显示特定的玩家选项或NPC对话
            // 获取可交互节点
            isPlayerChoice = ConditionFitter(currentDialogue.GetInteractiveChildren(currentDialogueNode)).Count() > 0;
            // 进行下一个节点的转换
            if (!isPlayerChoice)
            {
                // 无可交互节点
                // TODO: 完成多个NPC或玩家对话节点时 选择机制的判定
                // 获取所有不可交互的对话节点
                DialogueNodeSO[] nonInteractiveChildren = ConditionFitter(currentDialogue.GetNonInteractiveChildren(currentDialogueNode)).ToArray();
                // 推动节点变换 移动至下一节点
                currentDialogueNPC.TryTriggerDialogueEvent(currentDialogueNode?.ExitEventID, currentDialogueNode?.UniqueID);
                currentDialogueNode = (nonInteractiveChildren.Length == 0 ? null : nonInteractiveChildren[0]);
                onDialogueNodeChange?.Invoke();
                return;
            }
            // 触发对话事件 此事件为下一个对话Enter事件
            currentDialogueNPC.TryTriggerDialogueEvent(currentDialogueNode?.ExitEventID, currentDialogueNode?.UniqueID);
            // 调用节点变化事件
            onDialogueNodeChange?.Invoke();
        }
        public void Choose(DialogueNodeSO chooseNode)
        {
            // 选择选项
            currentDialogueNode = chooseNode;
            // 出发选择事件
            Next();
        }
    }
}

