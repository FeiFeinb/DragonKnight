using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Editor.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphBaseNodeSaveData(string uniqueID, string title, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView)
        {
            _uniqueID = uniqueID;
            _title = title;
            _rectPos = rectPos;
            _inputPortsData = inputPorts.ToPortData(graphView);
            _outputPortsData = outputPorts.ToPortData(graphView);
        }
        
        public string _uniqueID;
        public string _title;
        public Rect _rectPos;
        public List<DialogueGraphPortSaveData> _inputPortsData;
        public List<DialogueGraphPortSaveData> _outputPortsData;
    }
}
