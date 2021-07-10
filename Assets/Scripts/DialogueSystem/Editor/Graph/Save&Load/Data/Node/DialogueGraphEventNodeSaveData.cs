using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 事件节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphEventNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public List<DialogueEventSO> ObjectFields;
        public DialogueGraphEventNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
            
        }
    }
}