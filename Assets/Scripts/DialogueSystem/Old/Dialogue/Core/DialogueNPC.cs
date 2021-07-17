// using System.Collections.Generic;
// using System.Linq;
// using DialogueSystem.Old.Dialogue.ScriptableObjects;
// using DialogueSystem.Old.PlayerControl;
// using RPG.DialogueSystem.Graph;
// using UnityEngine;
// using UnityEngine.Events;
//
// namespace DialogueSystem.Old.Dialogue.Core
// {
//     /// <summary>
//     /// 可对话NPC类
//     /// </summary>
//     public class DialogueNPC : MonoBehaviour
//     {
//         public DialogueSO Content => _content;                              // 外部获取
//         public DialogueCharacterInfoSO NPCInfo => _npcInfo;                 // 外部获取
//         
//         
//         [SerializeField] private DialogueCharacterInfoSO _npcInfo;                           // NPC角色信息
//         
//         [SerializeField] private DialogueSO _content;                                        // NPC对话内容
//         
//         [SerializeField] private List<DialogueEvent> _events = new List<DialogueEvent>();    // 事件数列
//         
//         /// <summary>
//         /// 开始对话
//         /// </summary>
//         public void StartDialogue()
//         {
//             OldPlayerDialogueManager.Instance?.SetDialogue(this);
//         }
//         
//         /// <summary>
//         /// 结束对话
//         /// </summary>
//         public void ResetDialogue()
//         {
//             OldPlayerDialogueManager.Instance?.ResetDialogue();
//         }
//         
//         /// <summary>
//         /// 尝试触发对话事件
//         /// </summary>
//         /// <param name="dialogueEventID">对话事件ID</param>
//         /// <param name="dialogueUniqueID">对话ID</param>
//         internal void TryTriggerDialogueEvent(string dialogueEventID, string dialogueUniqueID)
//         {
//             if (string.IsNullOrEmpty(dialogueEventID)) return;
//             foreach (var dialogueEvent in _events.Where(dialogueEvent => dialogueEvent.EventID.Equals(dialogueEventID)))
//             {
//                 dialogueEvent.Event?.Invoke(dialogueUniqueID);
//             }
//         }
//         
//         /// <summary>
//         /// 添加进入对话事件
//         /// </summary>
//         /// <param name="dialogueEventID">对话事件ID</param>
//         /// <param name="dialogueUniqueID">对话ID</param>
//         /// <param name="dialogueAction">对话Action</param>
//         internal void AddDialogueEnterEvent(string dialogueEventID, string dialogueUniqueID, UnityAction<string> dialogueAction)
//         {
//             // 设置对话节点中的EventID
//             _content.GetNode(dialogueUniqueID).SetEnterEventID(dialogueEventID);
//             // TODO: 设置对话节点中的Condition
//             // 在NPC身上添加事件
//             foreach (var dialogueEvent in _events.Where(dialogueEvent => dialogueEvent.EventID == dialogueEventID))
//             {
//                 dialogueEvent.Event.AddListener(dialogueAction);
//                 return;
//             }
//             _events.Add(new DialogueEvent(dialogueEventID, dialogueAction));
//         }
//         
//         /// <summary>
//         /// 移除进入对话事件
//         /// </summary>
//         /// <param name="dialogueEventID">对话事件ID</param>
//         /// <param name="dialogueUniqueID">对话ID</param>
//         internal void RemoveDialogueEnterEvent(string dialogueEventID, string dialogueUniqueID)
//         {
//             _content.GetNode(dialogueUniqueID).SetEnterEventID(string.Empty);
//             // 移除整个Event
//             _events.Remove(_events.Find(dialogueEvent => dialogueEvent.EventID == dialogueEventID));
//         }
//     }
// }
