using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem
{
    [CreateAssetMenu(fileName = "New DialogueGraphSO", menuName = "Dialogue System/Graph/DialogueGraphSO")]
    public class DialogueGraphSO : ScriptableObject
    {
        [SerializeField]
        public List<DialogueGraphEdgeSaveData> edgesSaveData = new List<DialogueGraphEdgeSaveData>(); // 连线数据数列

        [SerializeField]
        public List<DialogueGraphStartNodeSaveData>
            startNodesSaveData = new List<DialogueGraphStartNodeSaveData>(); // 开始节点数列

        [SerializeField]
        public List<DialogueGraphEndNodeSaveData>
            endNodesSaveData = new List<DialogueGraphEndNodeSaveData>(); // 结束节点数列

        [SerializeField]
        public List<DialogueGraphTalkNodeSaveData>
            talkNodesSaveData = new List<DialogueGraphTalkNodeSaveData>(); // 对话节点数列

        [SerializeField]
        public List<DialogueGraphConditionNodeSaveData> conditionNodesSaveData =
            new List<DialogueGraphConditionNodeSaveData>(); // 条件节点数列

        [SerializeField]
        public List<DialogueGraphEventNodeSaveData> eventNodesSaveData = new List<DialogueGraphEventNodeSaveData>();

        [SerializeField]
        public List<DialogueGraphChoiceNodeSaveData> choiceNodesSaveData = new List<DialogueGraphChoiceNodeSaveData>();

    }
}