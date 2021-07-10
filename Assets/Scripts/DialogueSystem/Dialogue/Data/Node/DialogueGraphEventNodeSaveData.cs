using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem
{
    /// <summary>
    /// 事件节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphEventNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public List<DialogueEventSO> ObjectFields;
        public DialogueGraphEventNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
            
        }
    }
}