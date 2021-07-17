using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 事件节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphEventNodeSaveData : DialogueGraphBaseNodeSaveData, DialoguePreInit
    {
        public List<DialogueEventSO> ObjectFields;
        public DialogueGraphEventNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
            
        }

        public override bool HandleData(DialogueTreeNode treeNode, GameObject obj)
        {
            ObjectFields.ForEach(dialogueEventSO => dialogueEventSO.RaiseEvent());
            return true;
        }

        public void PreInit(GameObject obj)
        {
            ObjectFields.ForEach(dialogueEventSO => dialogueEventSO.Init(obj));
        }
    }
}