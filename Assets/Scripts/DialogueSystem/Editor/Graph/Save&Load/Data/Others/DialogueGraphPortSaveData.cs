using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 端口数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphPortSaveData
    {
        public string PortName;                                 // 端口名字
        public Port.Capacity Capacity;                          // 端口连接多重性
        public List<DialogueGraphEdgeSaveData> EdgesSaveData;   // 连线数据数列
    }
}