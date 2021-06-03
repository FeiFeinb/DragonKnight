using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Editor.Graph;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "New DialogueGraphSO", menuName = "Dialogue System/Graph/DialogueGraphSO", order = 0)]
    public class DialogueGraphSO : ScriptableObject
    {
        [SerializeField] private List<DialogueGraphEdgeSaveData> edgesSaveData = new List<DialogueGraphEdgeSaveData>();                 // 连线数据数列

        [SerializeField] private List<DialogueGraphStartNodeSaveData> startNodesSaveData = new List<DialogueGraphStartNodeSaveData>();  // 开始节点数列
    
        [SerializeField] private List<DialogueGraphEndNodeSaveData> endNodesSaveData = new List<DialogueGraphEndNodeSaveData>();        // 结束节点数列

        [SerializeField] private List<DialogueGraphTalkNodeSaveData> talkNodesSaveData = new List<DialogueGraphTalkNodeSaveData>();     // 对话节点数列

        /// <summary>
        /// 保存图中节点
        /// </summary>
        /// <param name="graphView">节点图</param>
        public void Save(DialogueGraphView graphView)
        {
            List<SaveT> CaptureNodesData<T, SaveT>(UQueryState<Node> graphNodes) where T : DialogueGraphBaseNode
            {
                return graphNodes.Where(node => node is T).Cast<T>().Select(node => node.CreateNodeData()).Cast<SaveT>().ToList();
            }
            
            var nodes = graphView.nodes;
            // 储存节点数据
            startNodesSaveData = CaptureNodesData<DialogueGraphStartNode, DialogueGraphStartNodeSaveData>(nodes);
            endNodesSaveData = CaptureNodesData<DialogueGraphEndNode, DialogueGraphEndNodeSaveData>(nodes);
            talkNodesSaveData = CaptureNodesData<DialogueGraphTalkNode, DialogueGraphTalkNodeSaveData>(nodes);
            
            // 储存连线数据
            edgesSaveData.Clear();
            foreach (Edge edge in graphView.edges.Where(edge => edge.input != null))
            {
                edgesSaveData.Add(new DialogueGraphEdgeSaveData
                {
                    OutputNodeUniqueID = ((DialogueGraphBaseNode)edge.output.node).UniqueID,
                    InputNodeUniqueID = ((DialogueGraphBaseNode)edge.input.node).UniqueID
                });
            }
            
            // 脏标记
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// 加载对话节点
        /// </summary>
        /// <param name="graphView">目标对话图</param>
        public void Load(DialogueGraphView graphView)
        {
            // 清空
            graphView.ClearEdgesAndNodes();
            
            var nodeDataDic = new Dictionary<string, DialogueGraphBaseNodeSaveData>();
            // originNodeUniqueID - List<originPortIndex, targetNodeUniqueID, targetPortIndex>
            var edgeDataDic = new Dictionary<string, List<Tuple<int, string, int>>>();

            void LoadNode<T>(IEnumerable<DialogueGraphBaseNodeSaveData> nodesSaveData) where T : DialogueGraphBaseNode
            {
                foreach (DialogueGraphBaseNodeSaveData dialogueGraphBaseNodeSaveData in nodesSaveData)
                {
                    // 缓存节点数据进字典中
                    nodeDataDic.Add(dialogueGraphBaseNodeSaveData.UniqueID, dialogueGraphBaseNodeSaveData);
                    // 生成节点
                    T dialogueNode = (T)Activator.CreateInstance(typeof(T), dialogueGraphBaseNodeSaveData.RectPos.position, graphView, dialogueGraphBaseNodeSaveData);
                    // 加载节点数据
                    dialogueNode.LoadNodeData(dialogueGraphBaseNodeSaveData);
                    graphView.AddElement(dialogueNode);
                }
            }

            LoadNode<DialogueGraphStartNode>(startNodesSaveData);
            LoadNode<DialogueGraphEndNode>(endNodesSaveData);
            LoadNode<DialogueGraphTalkNode>(talkNodesSaveData);
            
            foreach (DialogueGraphEdgeSaveData edgeSaveData in edgesSaveData)
            {
                string originNodeUniqueID = edgeSaveData.OutputNodeUniqueID;
                string targetNodeUniqueID = edgeSaveData.InputNodeUniqueID;
                int outputPortIndex = nodeDataDic.GetOutputPortIndex(originNodeUniqueID, targetNodeUniqueID);
                int inputPortIndex = nodeDataDic.GetInputPortIndex(targetNodeUniqueID, originNodeUniqueID);
                // 缓存连线数据进字典中
                if (edgeDataDic.ContainsKey(originNodeUniqueID))
                {
                    edgeDataDic[originNodeUniqueID].Add(new Tuple<int, string, int>(outputPortIndex, targetNodeUniqueID, inputPortIndex));
                }
                else
                {
                    edgeDataDic.Add(originNodeUniqueID, new List<Tuple<int, string, int>>
                    {
                        new Tuple<int, string, int>(outputPortIndex, targetNodeUniqueID, inputPortIndex)
                    });
                }
            }

            // 连线
            var baseNodes = graphView.nodes.Cast<DialogueGraphBaseNode>().ToList();
            foreach (DialogueGraphBaseNode originNode in baseNodes)
            {
                // 只检测出口端口的节点连线
                if (edgeDataDic.ContainsKey(originNode.UniqueID))
                {
                    foreach (var (originPortIndex, targetNodeUniqueID, targetPortIndex) in edgeDataDic[originNode.UniqueID])
                    {
                        DialogueGraphBaseNode targetNode = baseNodes.FirstOrDefault(node => node.UniqueID == targetNodeUniqueID);
                        if (targetNode == null) continue;
                        Edge edge = originNode.OutPutBasePorts[originPortIndex].ConnectTo(targetNode.InputBasePorts[targetPortIndex]);
                        graphView.AddElement(edge);
                    }
                }
            }
        }

        
    }
}