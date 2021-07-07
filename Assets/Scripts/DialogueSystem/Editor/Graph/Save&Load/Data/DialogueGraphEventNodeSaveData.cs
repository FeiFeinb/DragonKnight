using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 事件节点数据类
    /// </summary>
    public class DialogueGraphEventNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphEventNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}