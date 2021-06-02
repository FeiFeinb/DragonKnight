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
        [SerializeField] private List<DialogueGraphEdgeSaveData> edgesSaveData = new List<DialogueGraphEdgeSaveData>();

        [SerializeField] private List<DialogueGraphStartNodeSaveData> startNodesSaveData = new List<DialogueGraphStartNodeSaveData>();

        [SerializeField] private List<DialogueGraphEndNodeSaveData> endNodesSaveData = new List<DialogueGraphEndNodeSaveData>();

        [SerializeField] private List<DialogueGraphTalkNodeSaveData> talkNodesSaveData = new List<DialogueGraphTalkNodeSaveData>();

        public void Save(DialogueGraphView graphView)
        {
            List<SaveT> Save<T, SaveT>(UQueryState<Node> graphNodes) where T : DialogueGraphBaseNode
            {
                return graphNodes.Where(node => node is T).Cast<T>().Select(node => node.CreateNodeData()).Cast<SaveT>().ToList();
            }
            
            var nodes = graphView.nodes;
            startNodesSaveData = Save<DialogueGraphStartNode, DialogueGraphStartNodeSaveData>(nodes);
            endNodesSaveData = Save<DialogueGraphEndNode, DialogueGraphEndNodeSaveData>(nodes);
            talkNodesSaveData = Save<DialogueGraphTalkNode, DialogueGraphTalkNodeSaveData>(nodes);
            
            edgesSaveData.Clear();
            foreach (Edge edge in graphView.edges.Where(edge => edge.input != null))
            {
                string originNodeUniqueID = (edge.output.node as DialogueGraphBaseNode).UniqueID;
                string targetNodeUniqueID = (edge.input.node as DialogueGraphBaseNode).UniqueID;
                edgesSaveData.Add(new DialogueGraphEdgeSaveData()
                {
                    outputNodeUniqueID = originNodeUniqueID,
                    inputNodeUniqueID = targetNodeUniqueID
                });
            }

            EditorUtility.SetDirty(this);
        }


        public void Load(DialogueGraphView graphView)
        {
            // 清空连线
            foreach (Edge edge in graphView.edges)
            {
                graphView.RemoveElement(edge);
            }
                
            // 清空节点
            foreach (Node selectViewPort in graphView.nodes)
            {
                graphView.RemoveElement(selectViewPort);
            }
            
            var saveDataDic = new Dictionary<string, DialogueGraphBaseNodeSaveData>();
            
            // originNodeUniqueID - List<originPortIndex, targetNodeUniqueID, targetPortIndex>
            var edgeDataDic = new Dictionary<string, List<Tuple<int, string, int>>>();

            void LoadNode<T>(IEnumerable<DialogueGraphBaseNodeSaveData> nodesSaveData) where T : DialogueGraphBaseNode
            {
                foreach (DialogueGraphBaseNodeSaveData dialogueGraphBaseNodeSaveData in nodesSaveData)
                {
                    saveDataDic.Add(dialogueGraphBaseNodeSaveData.UniqueID, dialogueGraphBaseNodeSaveData);
                    T dialogueNode = Activator.CreateInstance(typeof(T), dialogueGraphBaseNodeSaveData.RectPos.position, graphView, dialogueGraphBaseNodeSaveData) as T;
                    dialogueNode.LoadNodeData(dialogueGraphBaseNodeSaveData);
                    graphView.AddElement(dialogueNode);
                }
            }

            LoadNode<DialogueGraphStartNode>(startNodesSaveData);
            LoadNode<DialogueGraphEndNode>(endNodesSaveData);
            LoadNode<DialogueGraphTalkNode>(talkNodesSaveData);


            foreach (DialogueGraphEdgeSaveData edgeSaveData in edgesSaveData)
            {
                string originNodeUniqueID = edgeSaveData.outputNodeUniqueID;
                string targetNodeUniqueID = edgeSaveData.inputNodeUniqueID;
                int outputPortIndex = saveDataDic.GetOutputPortIndex(originNodeUniqueID, targetNodeUniqueID);
                int inputPortIndex = saveDataDic.GetInputPortIndex(targetNodeUniqueID, originNodeUniqueID);
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
            var baseNodes = graphView.nodes.Cast<DialogueGraphBaseNode>();
            foreach (DialogueGraphBaseNode originNode in baseNodes)
            {
                // 只检测出口端口的节点连线
                if (edgeDataDic.ContainsKey(originNode.UniqueID))
                {
                    foreach (var (originPortIndex, targetNodeUniqueID, targetPortIndex) in edgeDataDic[originNode.UniqueID])
                    {
                        DialogueGraphBaseNode targetNode = baseNodes.FirstOrDefault(node => node.UniqueID == targetNodeUniqueID);
                        Edge edge = originNode.OutPutBasePorts[originPortIndex].ConnectTo(targetNode.InputBasePorts[targetPortIndex]);
                        graphView.AddElement(edge);
                    }
                }
            }
        }

        
    }
}