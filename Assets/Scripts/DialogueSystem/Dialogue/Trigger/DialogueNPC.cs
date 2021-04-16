using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.DialogueSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class DialogueNPC : MonoBehaviour
    {
        public Dialogue Dialogue => dialogue;
        public DialogueCharacterInfo NPCInfo => dialogueNPCInfo;
        [SerializeField] private DialogueCharacterInfo dialogueNPCInfo;
        [SerializeField] private Dialogue dialogue;     // NPC对话
        [SerializeField] private List<DialogueEvent> dialogueEvents = new List<DialogueEvent>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerConversant convesant = other.GetComponent<PlayerConversant>();
                convesant.SetDialogue(this);
            }
        }
        public void OnDialogueTriggerEvent(string _dialogueEventID, string _dialogueUniqueID)
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
        public void AddDialogueEvent(string _dialogueUniqueID, string _dialogueEventID, UnityAction<string> _dialogueAction)
        {
            // 设置对话节点中的EventID
            dialogue.GetNode(_dialogueUniqueID).SetEnterEventID(_dialogueEventID);
            // TODO: 设置对话节点中的Condition
            // 在NPC身上添加事件
            dialogueEvents.Add(new DialogueEvent(_dialogueEventID, _dialogueAction));
        }
        public void RemoveDialogueEvent(string _dialogueUniqueID, string _dialogueEventID, UnityAction<string> _dialogueAction)
        {
            // TODO: 当任务完成时或游戏退出时移除监听
            dialogue.GetNode(_dialogueUniqueID).SetEnterEventID(string.Empty);
            dialogueEvents.Remove(dialogueEvents.Find((dialogueEvent) =>
            {
                return (dialogueEvent.dialogueEventID == _dialogueEventID);
            }));
        }
    }
}
