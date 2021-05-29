using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    [CreateAssetMenu(fileName = "New DialogueGraphSO", menuName = "Dialogue System/Graph/DialogueGraphSO", order = 0)]
    public class DialogueGraphSO : ScriptableObject
    {
        [SerializeField] private List<DialogueGraphStartNodeSaveData> startNodesSaveData = new List<DialogueGraphStartNodeSaveData>();
        [SerializeField] private List<DialogueGraphEndNodeSaveData> endNodesSaveData = new List<DialogueGraphEndNodeSaveData>();
        [SerializeField] private List<DialogueGraphTalkNodeSaveData> talkNodesSaveData = new List<DialogueGraphTalkNodeSaveData>();
        
        public void Save(GraphView graphView)
        {
            var nodes = graphView.nodes;
            startNodesSaveData = Save<DialogueGraphStartNode, DialogueGraphStartNodeSaveData>(nodes);
            endNodesSaveData = Save<DialogueGraphEndNode, DialogueGraphEndNodeSaveData>(nodes);
            talkNodesSaveData = Save<DialogueGraphTalkNode, DialogueGraphTalkNodeSaveData>(nodes);
        }
        private List<SaveT> Save<T, SaveT>(UQueryState<Node> nodes) where T : DialogueGraphBaseNode
        {
            return nodes.Where(node => node is T).Cast<T>().Select(node => node.CreateState()).Cast<SaveT>().ToList();
        }
    }
}