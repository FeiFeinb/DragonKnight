using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphPortSaveData
    {
        public string _portName;
        public Port.Capacity _capacity;
        public List<DialogueGraphEdgeSaveData> edgesSaveData = new List<DialogueGraphEdgeSaveData>();
    }
}