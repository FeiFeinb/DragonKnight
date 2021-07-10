using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Editor.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphBaseNodeSaveData
    {
        public string UniqueID => _uniqueID;
        public Rect RectPos => _rectPos;
        public List<DialogueGraphPortSaveData> InputPortsData => _inputPortsData;
        public List<DialogueGraphPortSaveData> OutputPortsData => _outputPortsData;
        
        [SerializeField] private string _uniqueID;                                      // 节点ID
        [SerializeField] private Rect _rectPos;                                         // 节点位置与大小
        [SerializeField] private List<DialogueGraphPortSaveData> _inputPortsData;       // 输入端口数据数列
        [SerializeField] private List<DialogueGraphPortSaveData> _outputPortsData;      // 输出端口数据数列
        
        public DialogueGraphBaseNodeSaveData(string uniqueID, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView)
        {
            _uniqueID = uniqueID;
            _rectPos = rectPos;
            _inputPortsData = inputPorts.ToPortData(graphView);
            _outputPortsData = outputPorts.ToPortData(graphView);
        }
    }
}
