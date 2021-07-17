using RPG.DialogueSystem.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
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