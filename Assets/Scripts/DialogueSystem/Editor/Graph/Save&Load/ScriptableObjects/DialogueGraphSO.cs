using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "New DialogueGraphSO", menuName = "Dialogue System/Graph/DialogueGraphSO", order = 0)]
    public class DialogueGraphSO : ScriptableObject
    {
        // outputNodeUniqueID - List<portIndex, TargetNodeUniqueID>
        private Dictionary<string, List<Tuple<int, string, int>>> edgeDataDic;
        
        [SerializeField]
        private List<DialogueGraphStartNodeSaveData> startNodesSaveData = new List<DialogueGraphStartNodeSaveData>();

        [SerializeField]
        private List<DialogueGraphEndNodeSaveData> endNodesSaveData = new List<DialogueGraphEndNodeSaveData>();

        [SerializeField]
        private List<DialogueGraphTalkNodeSaveData> talkNodesSaveData = new List<DialogueGraphTalkNodeSaveData>();


        private Dictionary<string, DialogueGraphBaseNodeSaveData> saveDataDic =
            new Dictionary<string, DialogueGraphBaseNodeSaveData>();
        public void Save(DialogueGraphView graphView)
        {
            var nodes = graphView.nodes;
            saveDataDic.Clear();
            edgeDataDic = new Dictionary<string, List<Tuple<int, string, int>>>();
            startNodesSaveData = Save<DialogueGraphStartNode, DialogueGraphStartNodeSaveData>(nodes);
            endNodesSaveData = Save<DialogueGraphEndNode, DialogueGraphEndNodeSaveData>(nodes);
            talkNodesSaveData = Save<DialogueGraphTalkNode, DialogueGraphTalkNodeSaveData>(nodes);
            foreach (Edge edge in graphView.edges.Where(edge => edge.input != null))
            {
                string originNodeUniqueID = (edge.output.node as DialogueGraphBaseNode).UniqueID;
                string targetNodeUniqueID = (edge.input.node as DialogueGraphBaseNode).UniqueID;
                if (!edgeDataDic.ContainsKey(originNodeUniqueID))
                {
                    edgeDataDic.Add(originNodeUniqueID, new List<Tuple<int, string, int>>()
                    {
                        new Tuple<int, string, int>(GetOutIndex(originNodeUniqueID, targetNodeUniqueID), targetNodeUniqueID, GetInputIndex(targetNodeUniqueID, originNodeUniqueID))
                    });
                }
                else
                {
                    edgeDataDic[originNodeUniqueID].Add(new Tuple<int, string, int>(GetOutIndex(originNodeUniqueID, targetNodeUniqueID), targetNodeUniqueID, GetInputIndex(targetNodeUniqueID, originNodeUniqueID)));
                }
            }
            
            
            EditorUtility.SetDirty(this);
        }

        public void Load(DialogueGraphEditorWindow editorWindow, DialogueGraphView graphView)
        {
            
            foreach (DialogueGraphStartNodeSaveData startNode in startNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphStartNode(startNode._rectPos.position, editorWindow, graphView, startNode));
            }

            foreach (DialogueGraphEndNodeSaveData endNode in endNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphEndNode(endNode._rectPos.position, editorWindow, graphView, endNode));
            }

            foreach (DialogueGraphTalkNodeSaveData talkNode in talkNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphTalkNode(talkNode._rectPos.position, editorWindow, graphView, talkNode));
            }

            // foreach (Node graphViewNode in graphView.nodes)
            // {
            //     // 只找出口
            //     // var edges = edgesSaveData.Where(edge => edge.outputNodeUniqueID == (graphViewNode as DialogueGraphBaseNode).UniqueID);
            //     // foreach (var edge in edges)
            //     // {
            //     //     int outputIndex = GetOutIndex((graphViewNode as DialogueGraphBaseNode).UniqueID,
            //     //         edge.inputNodeUniqueID);
            //     //     int inputIndex = GetInputIndex(edge.inputNodeUniqueID,
            //     //         ((graphViewNode as DialogueGraphBaseNode).UniqueID));
            //     //     Port ip = (graphView.nodes.FirstOrDefault(node =>
            //     //             (node as DialogueGraphBaseNode).UniqueID == edge.inputNodeUniqueID) as
            //     //         DialogueGraphBaseNode)
            //     //         ._inputBasePorts[inputIndex];
            //     //     (graphViewNode as DialogueGraphBaseNode)._outputBasePorts[outputIndex].ConnectTo(ip);
            //     //     graphView.AddElement((graphViewNode as DialogueGraphBaseNode)._outputBasePorts[outputIndex]
            //     //         .ConnectTo(ip));
            //     // }
            // }
            
            foreach (Node graphViewNode in graphView.nodes)
            {
                DialogueGraphBaseNode baseNode = graphViewNode as DialogueGraphBaseNode;
                var temp = edgeDataDic.ContainsKey(baseNode.UniqueID) ? edgeDataDic[baseNode.UniqueID] : null;
                if (temp != null)
                    foreach (var tuple in temp)
                    {
                        Port ip =
                            (graphView.nodes.FirstOrDefault(t =>
                                    (t as DialogueGraphBaseNode).UniqueID == tuple.Item2) as
                                DialogueGraphBaseNode)._inputBasePorts[tuple.Item1];
                        Edge e = baseNode._outputBasePorts[tuple.Item3].ConnectTo(ip);
                        graphView.AddElement(e);
                    }
            }
        }
        
        private List<SaveT> Save<T, SaveT>(UQueryState<Node> nodes) where T : DialogueGraphBaseNode
        {
            return nodes.Where(node => node is T).Cast<T>().Select(node =>
            {
                var data = node.CreateNodeData();
                saveDataDic.Add(data._uniqueID, data);
                return data;
            }).Cast<SaveT>().ToList();
        }

        private int GetInputIndex(string originUniqueID, string targetUniqueID)
        {
            return saveDataDic[originUniqueID].GetInputPortIndex(targetUniqueID);
        }

        private int GetOutIndex(string originUniqueID, string targetUniqueID)
        {
            return saveDataDic[originUniqueID].GetOutputPortIndex(targetUniqueID);
        }

    }
}