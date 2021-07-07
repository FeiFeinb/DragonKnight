using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphStartNode : DialogueGraphBaseNode
    {
        public DialogueGraphStartNode(Vector2 position, DialogueGraphView graphView, DialogueGraphStartNodeSaveData startNodeSaveData = null) : base(position, graphView, startNodeSaveData?.UniqueID)
        {
            title = "Start Node";
            AddOutputPort("Children", Port.Capacity.Single);
            
            
        }

        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            return CreateBaseNodeData<DialogueGraphStartNodeSaveData>();
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            
        }
    }
}