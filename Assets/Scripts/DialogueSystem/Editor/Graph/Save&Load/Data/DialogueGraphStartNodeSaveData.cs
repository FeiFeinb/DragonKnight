using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 开始节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphStartNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphStartNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}