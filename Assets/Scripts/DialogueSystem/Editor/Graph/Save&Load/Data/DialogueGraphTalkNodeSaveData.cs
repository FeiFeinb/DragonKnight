using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 对话节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphTalkNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        [TextArea(1, 10)]
        public string Content;                              // 对话内容
        public AudioClip TalkAudioClip;                     // 对话音频
        public InterlocutorType Interlocutor;               // 对话方
        public DialogueCharacterInfoSO CharacterInfoSO;     // 对话角色信息

        public DialogueGraphTalkNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}