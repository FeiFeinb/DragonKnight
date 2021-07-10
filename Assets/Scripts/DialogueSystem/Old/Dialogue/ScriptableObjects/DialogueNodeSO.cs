using System;
using System.Collections.Generic;
using DialogueSystem.Old.Dialogue.Core;
using DialogueSystem.Old.Dialogue.Core.Condition;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Old.Dialogue.ScriptableObjects
{
    /// <summary>
    /// 对话节点SO
    /// </summary>
    public class DialogueNodeSO : ScriptableObject
    {
        public string UniqueID => uniqueID;                                 // 外部获取
        public string Content => _content;                                  // 外部获取
        public DialogueSpeaker Speaker => _speaker;                         // 外部获取
        public List<string> Parents => _parents;                            // 外部获取
        public List<string> Children => _children;                          // 外部获取
        public Rect sizeRect => _rect;                                      // 外部获取
        public string EnterEventID => _dialogueEnterEventID;                // 外部获取
        public string ExitEventID => _dialogueExitEventID;                  // 外部获取
        public DialogueCondition Condition => _condition;                   // 外部获取

        [SerializeField] private string uniqueID = Guid.NewGuid().ToString();               // 节点ID

        [SerializeField, TextArea(5, 10)] private string _content;                          // 对话内容

        [SerializeField] private DialogueSpeaker _speaker;                                  // 对话人物

        [SerializeField] private DialogueCondition _condition;                              // 显示对话条件

        [SerializeField] private string _dialogueEnterEventID;                              // 对话Enter事件

        [SerializeField] private string _dialogueExitEventID;                               // 对话Exit事件

        [SerializeField] private List<string> _parents = new List<string>();                // 父节点

        [SerializeField] private List<string> _children = new List<string>();               // 子节点

        [SerializeField] private Rect _rect = new Rect(0, 0, 200, 170);     // 大小

        /// <summary>
        /// 初始化对话节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        public void InitDialogueNode(DialogueNodeSO parentNode)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Add DialogueNode");
            Undo.RecordObject(parentNode, "Add DialogueNode");
#endif
            _parents.Add(parentNode.uniqueID);
            parentNode._children.Add(uniqueID);
            
        }

        /// <summary>
        /// 设置对话节点文字内容
        /// </summary>
        /// <param name="textStr">文字内容</param>
        public void SetText(string textStr)
        {
            if (textStr == _content) return;
            if (!string.IsNullOrEmpty(textStr))
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, "Change NodeText");
#endif
            }
            _content = textStr;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// 设置进入对话事件ID
        /// </summary>
        /// <param name="dialogueEventID">ID</param>
        public void SetEnterEventID(string dialogueEventID)
        {
            if (dialogueEventID == _dialogueEnterEventID) return;
            if (!string.IsNullOrEmpty(dialogueEventID))
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, "Change EnterEventId");
#endif
            }
            _dialogueEnterEventID = dialogueEventID;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// 设置退出对话事件ID
        /// </summary>
        /// <param name="dialogueEventID">ID</param>
        public void SetExitEventID(string dialogueEventID)
        {
            if (dialogueEventID == _dialogueExitEventID) return;
            if (!string.IsNullOrEmpty(dialogueEventID) && dialogueEventID != _dialogueExitEventID)
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, "Change ExitEventId");
#endif
            }
            _dialogueExitEventID = dialogueEventID;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// 设置节点位置
        /// </summary>
        /// <param name="rectPos">位置</param>
        public void SetRectPosition(Vector2 rectPos)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Move DialogueNode");
#endif
            _rect.position = rectPos;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// 连接父子节点
        /// </summary>
        /// <param name="childNode">子节点</param>
        public void Link(DialogueNodeSO childNode)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Link DialogueNode");
            Undo.RecordObject(childNode, "Link DialogueNode");
#endif
            _children.Add(childNode.uniqueID);
            childNode._parents.Add(uniqueID);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// 解除连接父子节点
        /// </summary>
        /// <param name="childNode">子节点</param>
        public void UnLink(DialogueNodeSO childNode)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "UnLink DialogueNode");
            Undo.RecordObject(childNode, "UnLink Dialogue");
#endif
            _children.Remove(childNode.uniqueID);
            childNode._parents.Remove(uniqueID);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}