using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphEndNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        
        public EndDialogueNodeType EndType;

        public DialogueGraphEndNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}