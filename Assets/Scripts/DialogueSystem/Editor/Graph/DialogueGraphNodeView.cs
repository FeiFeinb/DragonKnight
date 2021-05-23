using System;
using RPG.InventorySystem;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphNodeView : Node
    {
        public DialogueGraphNodeView()
        {
            title = "Dialogue GraphNode";
            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputPort.portName = "Parents";
            inputContainer.Add(inputPort);
            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
            outputPort.portName = "Children";
            outputContainer.Add(outputPort);
        }
    }
}