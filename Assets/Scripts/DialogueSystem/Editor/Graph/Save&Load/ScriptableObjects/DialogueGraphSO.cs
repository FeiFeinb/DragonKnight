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

        public void Save(DialogueGraphView graphView)
        {
            var nodes = graphView.nodes;
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
            
            foreach (DialogueGraphStartNodeSaveData saveData in startNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphStartNode(saveData._rectPos.position, editorWindow, graphView));
            }

            foreach (DialogueGraphEndNodeSaveData endNode in endNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphEndNode(endNode._rectPos.position, editorWindow, graphView));
            }

            foreach (DialogueGraphTalkNodeSaveData talkNode in talkNodesSaveData)
            {
                graphView.AddElement(new DialogueGraphTalkNode(talkNode._rectPos.position, editorWindow, graphView));
            }

            foreach (DialogueGraphEdgeSaveData edgeSaveData in edgesSaveData)
            {
                
            }
        }
        
        private List<SaveT> Save<T, SaveT>(UQueryState<Node> nodes) where T : DialogueGraphBaseNode
        {
            return nodes.Where(node => node is T).Cast<T>().Select(node => node.CreateNodeData()).Cast<SaveT>().ToList();
        }
    }
}