using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem
{
    /// <summary>
    /// 对话判定条件数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphConditionNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public ConditionDialogueNodeType ConditionType;
        public ScriptableObject ConditionSO;
        
        public DialogueGraphConditionNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
        }
    }
}