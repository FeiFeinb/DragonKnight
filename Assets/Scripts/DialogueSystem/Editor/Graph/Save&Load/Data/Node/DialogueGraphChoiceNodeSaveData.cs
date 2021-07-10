using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphChoiceNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public string Content;              // 回应内容
        public AudioClip TalkAudioClip;     // 回应音频
        
        public DialogueGraphChoiceNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}