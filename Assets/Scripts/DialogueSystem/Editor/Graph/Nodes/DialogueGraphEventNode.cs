using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphEventNode : DialogueGraphBaseNode
    {
        private readonly ObjectField _eventSOField;
        
        
        public DialogueGraphEventNode(Vector2 position, DialogueGraphView graphView,
            DialogueGraphEventNodeSaveData eventNodeSaveData = null) : base(position, graphView,
            eventNodeSaveData?.UniqueID)
        {
            title = "Event Node";

            AddInputPort("Parents", Port.Capacity.Single);
            AddOutputPort("Children", Port.Capacity.Single);
            
            
        }


        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            throw new System.NotImplementedException();
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}

     