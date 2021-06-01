using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphTalkNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public string _content;                               // 对话内容
        public AudioClip _audioClip;                          // 对话音频
        public InterlocutorType _interlocutorType;            // 对话方
        public DialogueCharacterInfoSO _characterInfoSO;      // 对话角色信息

        public DialogueGraphTalkNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}