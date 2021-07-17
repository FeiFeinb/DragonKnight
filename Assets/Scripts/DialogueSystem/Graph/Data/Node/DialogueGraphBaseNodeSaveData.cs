using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 节点数据类
    /// </summary>
    [System.Serializable]
    public abstract class DialogueGraphBaseNodeSaveData
    {
        public string UniqueID => _uniqueID;
        public Rect RectPos => _rectPos;
        public List<DialogueGraphPortSaveData> InputPortsData => _inputPortsData;
        public List<DialogueGraphPortSaveData> OutputPortsData => _outputPortsData;
        
        [SerializeField] private string _uniqueID;                                      // 节点ID
        [SerializeField] private Rect _rectPos;                                         // 节点位置与大小
        [SerializeField] private List<DialogueGraphPortSaveData> _inputPortsData;       // 输入端口数据数列
        [SerializeField] private List<DialogueGraphPortSaveData> _outputPortsData;      // 输出端口数据数列
        
        public DialogueGraphBaseNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData)
        {
            _uniqueID = uniqueID;
            _rectPos = rectPos;
            _inputPortsData = inputPortsData;
            _outputPortsData = outputPortsData;
        }

        /// <summary>
        /// 处理节点数据
        /// </summary>
        /// <param name="treeNode">所在对话树节点</param>
        /// <param name="obj">所在GameObject</param>
        /// <returns>是否需要等待玩家响应</returns>
        public abstract bool HandleData(DialogueTreeNode treeNode, GameObject obj);
    }
}
