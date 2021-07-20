using System;
using System.Collections.Generic;
using DialogueSystem.Graph;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 对话判定条件数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphConditionNodeSaveData : DialogueGraphBaseNodeSaveData, DialoguePreInit
    {
        public DialogueConditionType dialogueConditionType;

        public ScriptableObject SourceSO;

        // TODO: 制作Others类型的条件
        public DialogueGraphConditionNodeSaveData(string uniqueID, Rect rectPos,
            List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(
            uniqueID, rectPos, inputPortsData, outputPortsData)
        {
        }

        public override bool HandleData(DialogueTreeNode treeNode, GameObject obj)
        { 
            bool result = DialogueHandler.Instance.ConditionHandlers[dialogueConditionType].HandleCondition(SourceSO, obj);
            treeNode.childrenNodePair[0].SetCanThrough(result);
            treeNode.childrenNodePair[1].SetCanThrough(!result);
            return true;
        }

        public void PreInit(GameObject obj)
        {
            if (dialogueConditionType == DialogueConditionType.Others && SourceSO is DialogueConditionSO conditionSO)
            {
                conditionSO.Init(obj);
            }
        }
    }
}