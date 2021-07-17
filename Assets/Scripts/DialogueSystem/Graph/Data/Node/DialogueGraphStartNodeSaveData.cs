using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 开始节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphStartNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphStartNodeSaveData(string uniqueID, Rect rectPos,
            List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
        }

        public override bool HandleData(DialogueTreeNode treeNode, GameObject obj)
        {
            treeNode.Traverse(dialogueTreeNode => { (dialogueTreeNode as DialoguePreInit)?.PreInit(obj); });
            return true;
        }
    }
}