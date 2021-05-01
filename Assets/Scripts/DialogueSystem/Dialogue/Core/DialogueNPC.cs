using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.DialogueSystem;
namespace RPG.DialogueSystem
{
    public class DialogueNPC : MonoBehaviour
    {
        public DialogueSO DialogueContent => dialogueContent;
        public DialogueCharacterInfoSO NPCInfo => npcInfo;
        [SerializeField] private DialogueCharacterInfoSO npcInfo;     // NPC角色信息
        [SerializeField] private DialogueSO dialogueContent;          // NPC对话内容
        [SerializeField] private List<DialogueEvent> dialogueEvents = new List<DialogueEvent>();    // 事件数列
        
        public void StartDialogue()
        {
            PlayerDialogueManager.Instance?.SetDialogue(this);
        }
        public void ResetDialogue()
        {
            PlayerDialogueManager.Instance?.ResetDialogue();
        }
        internal void OnDialogueTriggerEvent(string _dialogueEventID, string _dialogueUniqueID)
        {
            if (string.IsNullOrEmpty(_dialogueEventID)) return;
            foreach (DialogueEvent dialogueEvent in dialogueEvents)
            {
                // 名称匹配 执行事件
                if (dialogueEvent.dialogueEventID.Equals(_dialogueEventID))
                {
                    dialogueEvent.dialogueEvent?.Invoke(_dialogueUniqueID);
                }
            }
        }
        internal void AddDialogueEnterEvent(string _dialogueUniqueID, string _dialogueEventID, UnityAction<string> _dialogueAction)
        {
            // 设置对话节点中的EventID
            dialogueContent.GetNode(_dialogueUniqueID).SetEnterEventID(_dialogueEventID);
            // TODO: 设置对话节点中的Condition
            // 在NPC身上添加事件
            // TODO: 判定列表中是否存在此事件 若存在则直接往事件中AddListener
            dialogueEvents.Add(new DialogueEvent(_dialogueEventID, _dialogueAction));
        }
        internal void RemoveDialogueEnterEvent(string _dialogueUniqueID, string _dialogueEventID)
        {
            dialogueContent.GetNode(_dialogueUniqueID).SetEnterEventID(string.Empty);
            // 移除整个Event
            dialogueEvents.Remove(dialogueEvents.Find((dialogueEvent) => (dialogueEvent.dialogueEventID == _dialogueEventID)));
        }
    }
}
