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
        [SerializeField] 
        private List<DialogueGraphEdgeSaveData> edgesSaveData = new List<DialogueGraphEdgeSaveData>();

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
            edgesSaveData = graphView.edges.Where(edge => edge.input != null).Select(graphViewEdge =>
                new DialogueGraphEdgeSaveData()
                {
                    inputNodeUniqueID = (graphViewEdge.input.node as DialogueGraphBaseNode)?.UniqueID,
                    outputNodeUniqueID = (graphViewEdge.output.node as DialogueGraphBaseNode)?.UniqueID
                }).ToList();
            startNodesSaveData = Save<DialogueGraphStartNode, DialogueGraphStartNodeSaveData>(nodes);
            endNodesSaveData = Save<DialogueGraphEndNode, DialogueGraphEndNodeSaveData>(nodes);
            talkNodesSaveData = Save<DialogueGraphTalkNode, DialogueGraphTalkNodeSaveData>(nodes);
            EditorUtility.SetDirty(this);
        }

        public void Load(DialogueGraphEditorWindow editorWindow, DialogueGraphView graphView)
        {
            
            foreach (DialogueGraphStartNodeSaveData startNode in startNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphStartNode(startNode._rectPos.position, editorWindow, graphView)
                {
                    _uniqueID = startNode._uniqueID,
                    title = startNode._title
                });
            }

            foreach (DialogueGraphEndNodeSaveData endNode in endNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphEndNode(endNode._rectPos.position, editorWindow, graphView)
                {
                    _uniqueID = endNode._uniqueID,
                    title = endNode._title
                });
            }

            foreach (DialogueGraphTalkNodeSaveData talkNode in talkNodesSaveData)
            {
                DialogueGraphTalkNode tempNode = new DialogueGraphTalkNode(talkNode._rectPos.position, editorWindow, graphView)
                {
                    _uniqueID = talkNode._uniqueID,
                    title = talkNode._title
                };
                foreach (var temp in talkNode._outputPortsData)
                {
                    tempNode.AddOutputPort(temp._portName, temp._capacity);
                }
                graphView.AddElement(tempNode);
            }


            foreach (Node graphViewNode in graphView.nodes)
            {
                // 只找出口
                var edges = edgesSaveData.Where(edge => edge.outputNodeUniqueID == (graphViewNode as DialogueGraphBaseNode).UniqueID);
                foreach (var edge in edges)
                {
                    int outputIndex = GetOutIndex((graphViewNode as DialogueGraphBaseNode).UniqueID, edge.inputNodeUniqueID);
                    int inputIndex = GetInputIndex(edge.inputNodeUniqueID,
                        ((graphViewNode as DialogueGraphBaseNode).UniqueID));
                    // Edge newEdge = new Edge()
                    // {
                    //     output = (graphViewNode as DialogueGraphBaseNode)._outputBasePorts[outputIndex],
                    //     input = (graphView.nodes.FirstOrDefault(node => (node as DialogueGraphBaseNode).UniqueID == edge.inputNodeUniqueID) as DialogueGraphBaseNode)._inputBasePorts[inputIndex]
                    // };
                    // newEdge.output.Connect(newEdge);
                    // newEdge.input.Connect(newEdge);
                    Port ip = (graphView.nodes.FirstOrDefault(node => (node as DialogueGraphBaseNode).UniqueID == edge.inputNodeUniqueID) as DialogueGraphBaseNode)._inputBasePorts[inputIndex];
                    (graphViewNode as DialogueGraphBaseNode)._outputBasePorts[outputIndex].ConnectTo(ip);
                    graphView.AddElement((graphViewNode as DialogueGraphBaseNode)._outputBasePorts[outputIndex].ConnectTo(ip));
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

        private int GetInputIndex(string originUniqueID, string uniqueID)
        {
            // var saveData = talkNodesSaveData.FirstOrDefault(data => data._uniqueID == originUniqueID);
            // return saveData.GetInputPortIndex(uniqueID);
            return saveDataDic[originUniqueID].GetInputPortIndex(uniqueID);
        }

        private int GetOutIndex(string originUniqueID, string uniqueID)
        {
            // var saveData = talkNodesSaveData.FirstOrDefault(data => data._uniqueID == originUniqueID);
            // return saveData.GetOutputPortIndex(uniqueID);
            return saveDataDic[originUniqueID].GetOutputPortIndex(uniqueID);
        }

    }
}