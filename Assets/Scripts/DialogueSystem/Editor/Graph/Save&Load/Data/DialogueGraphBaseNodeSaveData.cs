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

        public int GetInputPortIndex(string outputNodeUniqueID)
        {
            for (int i = 0; i < _inputPortsData.Count; i++)
            {
                if (_inputPortsData[i].edgesSaveData.Any(dialogueGraphEdgeSaveData => dialogueGraphEdgeSaveData.inputNodeUniqueID == outputNodeUniqueID))
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetOutputPortIndex(string inputNodeUniqueID)
        {
            for (int i = 0; i < _outputPortsData.Count; i++)
            {
                if (_outputPortsData[i].edgesSaveData.Any(dialogueGraphEdgeSaveData => dialogueGraphEdgeSaveData.inputNodeUniqueID == inputNodeUniqueID))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
