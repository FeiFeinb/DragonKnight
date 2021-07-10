using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 结束节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphEndNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public EndDialogueNodeType EndType;     // 结束节点类型

        public DialogueGraphEndNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}