using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphBaseNodeSaveData(string guid, string title, Rect rectPos)
        {
            _guid = guid;
            _title = title;
            _rectPos = rectPos;
        }
        public string _guid;
        public string _title;
        public Rect _rectPos;
        public List<DialogueGraphPortSaveData> _inputPortsData = new List<DialogueGraphPortSaveData>();
        public List<DialogueGraphPortSaveData> _outputPortsData = new List<DialogueGraphPortSaveData>();
    }
}
