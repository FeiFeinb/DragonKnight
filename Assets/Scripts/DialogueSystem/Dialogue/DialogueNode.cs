using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.DialogueSystem
{
    public class DialogueNode : ScriptableObject
    {
        public string UniqueID => uniqueID;
        public string Text => text;
        public DialogueSpeaker Speaker => dialogueSpeaker;
        public List<string> Parents => parents;
        public List<string> Children => children;
        public Rect sizeRect => rect;
        public string EnterEventID => dialogueEnterEventID;
        public string ExitEventID => dialogueExitEventID;
        public DialogueCondition Condition => dialogueCondition;
        [SerializeField] private string uniqueID = Guid.NewGuid().ToString();       // 节点ID
        [SerializeField, TextArea(5, 10)] private string text;                      // 对话内容
        [SerializeField] private DialogueSpeaker dialogueSpeaker;                   // 对话人物
        [SerializeField] private string dialogueEnterEventID;                       // 对话Enter事件
        [SerializeField] private string dialogueExitEventID;                        // 对话Exit事件
        [SerializeField] private List<string> parents = new List<string>();         // 父节点
        [SerializeField] private List<string> children = new List<string>();        // 子节点
        [SerializeField] private DialogueCondition dialogueCondition;               // 显示对话条件
        [SerializeField] private Rect rect = new Rect(0, 0, 200, 170);              // 大小
        public void InitDialogueNode(DialogueNode parentNode)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Add DialogueNode");
            Undo.RecordObject(parentNode, "Add DialogueNode");
#endif
            parents.Add(parentNode.uniqueID);
            parentNode.children.Add(uniqueID);
        }
        public void SetText(string _text)
        {
            if (string.IsNullOrEmpty(_text)) return;
#if UNITY_EDITOR
            Undo.RecordObject(this, "Change DialogueNode Text");
#endif
            text = _text;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void SetEnterEventID(string _dialogueEventID)
        {
            if (string.IsNullOrEmpty(_dialogueEventID)) return;
#if UNITY_EDITOR
            Undo.RecordObject(this, "Change DialogueNode DialogueEventId");
#endif
            dialogueEnterEventID = _dialogueEventID;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void SetExitEventID(string _dialogueEventID)
        {
            if (string.IsNullOrEmpty(_dialogueEventID)) return;
#if UNITY_EDITOR
            Undo.RecordObject(this, "Change DialogueNode DialogueEventId");
#endif
            dialogueExitEventID = _dialogueEventID;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void SetRectPosition(Vector2 _rectPos)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Move DialogueNode Position");
#endif
            rect.position = _rectPos;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void Link(DialogueNode childNode)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Link DialogueNode");
            Undo.RecordObject(childNode, "Link DiagogueNode");
#endif
            children.Add(childNode.uniqueID);
            childNode.parents.Add(uniqueID);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void UnLink(DialogueNode childNode)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "UnLink DialogueNode");
            Undo.RecordObject(childNode, "UnLink Dialogue");
#endif
            children.Remove(childNode.uniqueID);
            childNode.parents.Remove(uniqueID);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}