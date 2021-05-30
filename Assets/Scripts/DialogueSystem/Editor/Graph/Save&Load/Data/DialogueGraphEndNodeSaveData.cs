using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphEndNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public EndDialogueNodeType _endType;

        public DialogueGraphEndNodeSaveData(string uniqueID, string title, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, title, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}