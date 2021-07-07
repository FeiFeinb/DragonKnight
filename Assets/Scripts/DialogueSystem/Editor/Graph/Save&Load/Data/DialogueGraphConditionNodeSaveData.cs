using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 对话判定条件数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphConditionNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public ConditionDialogueNodeType ConditionType;
        public ScriptableObject ConditionSO;
        
        public DialogueGraphConditionNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}