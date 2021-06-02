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
        public string UniqueID => _uniqueID;
        public Rect RectPos => _rectPos;
        public List<DialogueGraphPortSaveData> InputPortsData => _inputPortsData;
        public List<DialogueGraphPortSaveData> OutputPortsData => _outputPortsData;
        
        [SerializeField] private string _uniqueID;
        [SerializeField] private Rect _rectPos;
        [SerializeField] private List<DialogueGraphPortSaveData> _inputPortsData;
        [SerializeField] private List<DialogueGraphPortSaveData> _outputPortsData;
        
        public DialogueGraphBaseNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView)
        {
            _uniqueID = uniqueID;
            _rectPos = rectPos;
            _inputPortsData = inputPorts.ToPortData(graphView);
            _outputPortsData = outputPorts.ToPortData(graphView);
        }
    }
}
