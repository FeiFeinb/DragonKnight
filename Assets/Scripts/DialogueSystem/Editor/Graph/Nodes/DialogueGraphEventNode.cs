using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphEventNode : DialogueGraphBaseNode
    {

        private readonly ObjectField _eventSOField;


        public DialogueGraphEventNode(Vector2 position, DialogueGraphView graphView, string uniqueID = null) : base(position, graphView, uniqueID)
        {
            title = "Event Node";
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

     