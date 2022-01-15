using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Graph;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "New DialogueGraphSO", menuName = "Dialogue System/Graph/DialogueGraphSO")]
    public class DialogueGraphSO : ScriptableObject
    {
        public string UniqueID = Guid.NewGuid().ToString();
        public List<DialogueGraphEdgeSaveData> edgesSaveData = new List<DialogueGraphEdgeSaveData>(); // 连线数据数列

        public List<DialogueGraphStartNodeSaveData>
            startNodesSaveData = new List<DialogueGraphStartNodeSaveData>(); // 开始节点数列

        public List<DialogueGraphEndNodeSaveData> endNodesSaveData = new List<DialogueGraphEndNodeSaveData>(); // 结束节点数列

        public List<DialogueGraphTalkNodeSaveData>
            talkNodesSaveData = new List<DialogueGraphTalkNodeSaveData>(); // 对话节点数列

        public List<DialogueGraphConditionNodeSaveData> conditionNodesSaveData =
            new List<DialogueGraphConditionNodeSaveData>(); // 条件节点数列

        public List<DialogueGraphEventNodeSaveData> eventNodesSaveData = new List<DialogueGraphEventNodeSaveData>();

        public List<DialogueGraphChoiceNodeSaveData> choiceNodesSaveData = new List<DialogueGraphChoiceNodeSaveData>();

        private readonly Dictionary<string, DialogueGraphBaseNodeSaveData> _cachedDic =
            new Dictionary<string, DialogueGraphBaseNodeSaveData>();


        public DialogueTreeNode GetDialogueTree()
        {
            var cacheDic = GetCachedDictionary();

            // 生成节点树
            void CreateNodeTree(DialogueGraphBaseNodeSaveData baseNodeSaveData, DialogueTreeNode startTreeNode)
            {
                DialogueTreeNode currentNode = startTreeNode;
                foreach (var outputPortData in baseNodeSaveData.OutputPortsData)
                {
                    var dialogueGraphEdgesSaveData = outputPortData.EdgesSaveData;
                    if (dialogueGraphEdgesSaveData == null || dialogueGraphEdgesSaveData.Count == 0)
                    {
                        currentNode.childrenNodePair.Add(new DialogueTreeNode.TreeNodePair(null, false));
                        continue;
                    }
                    
                    // 找对孩子的UniqueID
                    string childUniqueID = dialogueGraphEdgesSaveData[0].InputNodeUniqueID;
                    DialogueGraphBaseNodeSaveData childNode = cacheDic[childUniqueID];
                    var newTreeNode = new DialogueTreeNode(UniqueID, childNode);
                    
                    currentNode.childrenNodePair.Add(new DialogueTreeNode.TreeNodePair(newTreeNode, true));
                    // 根据UniqueID找到对应的节点 
                    CreateNodeTree(cacheDic[childUniqueID], newTreeNode);
                }
            }

            if (startNodesSaveData == null || startNodesSaveData.Count == 0)
            {
                throw new Exception("缺少Start Node");
            }
            else if (startNodesSaveData.Count > 1)
            {
                throw new Exception("Start Node节点数大于一");
            }
            else
            {
                DialogueGraphBaseNodeSaveData startNodeSaveData = startNodesSaveData[0];
                DialogueTreeNode rootNode = new DialogueTreeNode(UniqueID, startNodeSaveData);
                CreateNodeTree(startNodeSaveData, rootNode);
                return rootNode;
            }
        }

        public Dictionary<string, DialogueGraphBaseNodeSaveData> GetCachedDictionary()
        {
            void CacheDataInDic(DialogueGraphBaseNodeSaveData saveData)
            {
                _cachedDic.Add(saveData.UniqueID, saveData);
            }
            
            // if (_cachedDic.Count != 0) 
            // return _cachedDic;
            _cachedDic.Clear();
            startNodesSaveData.ForEach(CacheDataInDic);
            endNodesSaveData.ForEach(CacheDataInDic);
            talkNodesSaveData.ForEach(CacheDataInDic);
            conditionNodesSaveData.ForEach(CacheDataInDic);
            eventNodesSaveData.ForEach(CacheDataInDic);
            choiceNodesSaveData.ForEach(CacheDataInDic);
            return _cachedDic;
        }

        public IEnumerable<DialogueGraphBaseNodeSaveData> GetAllNode()
        {
            foreach (DialogueGraphStartNodeSaveData startNodeSaveData in startNodesSaveData)
            {
                yield return startNodeSaveData;
            }

            foreach (DialogueGraphEndNodeSaveData endNodeSaveData in endNodesSaveData)
            {
                yield return endNodeSaveData;
            }

            foreach (DialogueGraphTalkNodeSaveData talkNodeSaveData in talkNodesSaveData)
            {
                yield return talkNodeSaveData;
            }

            foreach (DialogueGraphConditionNodeSaveData conditionNodeSaveData in conditionNodesSaveData)
            {
                yield return conditionNodeSaveData;
            }

            foreach (DialogueGraphEventNodeSaveData eventNodeSaveData in eventNodesSaveData)
            {
                yield return eventNodeSaveData;
            }

            foreach (DialogueGraphChoiceNodeSaveData choiceNodeSaveData in choiceNodesSaveData)
            {
                yield return choiceNodeSaveData;
            }
        }

        public void Clear()
        {
            startNodesSaveData.Clear();
            endNodesSaveData.Clear();
            talkNodesSaveData.Clear();
            eventNodesSaveData.Clear();
            conditionNodesSaveData.Clear();
            choiceNodesSaveData.Clear();
            edgesSaveData.Clear();
        }
    }
}