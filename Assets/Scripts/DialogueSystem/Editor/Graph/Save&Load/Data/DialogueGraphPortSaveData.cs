using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphPortSaveData
    {
        public string PortName;
        public Port.Capacity Capacity;
        public List<DialogueGraphEdgeSaveData> EdgesSaveData;
    }
}