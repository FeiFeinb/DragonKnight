using System;
using System.Collections.Generic;
using DialogueSystem.Graph;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 事件节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphEventNodeSaveData : DialogueGraphBaseNodeSaveData, DialoguePreInit
    {
        [System.Serializable]
        public class EventTuple
        {
            public DialogueEventType EventType;
            public ScriptableObject SO;
        }

        public List<EventTuple> EventFieldsData;

        public DialogueGraphEventNodeSaveData(string uniqueID, Rect rectPos,
            List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(
            uniqueID, rectPos, inputPortsData, outputPortsData)
        {

        }

        public override bool HandleData(DialogueTreeNode treeNode, GameObject obj)
        {
            EventFieldsData.ForEach(valueTuple =>
            {
                DialogueHandler.Instance.EventHandlers[valueTuple.EventType].HandleEvent(valueTuple.SO, obj, treeNode.DialogueGraphSOUniqueID);
            });
            return true;
        }

        public void PreInit(GameObject obj)
        {
            EventFieldsData.ForEach(tuple =>
            {
                if (tuple.EventType == DialogueEventType.Others && tuple.SO is DialogueEventSO dialogueEventSO)
                {
                    dialogueEventSO.Init(obj);
                }
            });
        }
    }
}