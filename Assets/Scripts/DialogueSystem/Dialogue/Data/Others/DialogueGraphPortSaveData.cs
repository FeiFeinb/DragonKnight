using System.Collections.Generic;
using UnityEngine;
namespace RPG.DialogueSystem
{
    /// <summary>
    /// 端口数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphPortSaveData
    {
        public enum PortCapacity
        {
            Single,
            Multi
        }
        public string PortName;                                 // 端口名字
        public PortCapacity Capacity;                           // 端口连接多重性
        public List<DialogueGraphEdgeSaveData> EdgesSaveData;   // 连线数据数列

    }
}
