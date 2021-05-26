using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public abstract class DialogueGraphBaseNode : Node
    {
        protected string _nodeGuid;
        protected DialogueGraphEditorWindow _editorWindow;
        protected DialogueGraphView _graphView;
        protected Vector2 _defaultNodeSize = new Vector2(200, 100);
        public DialogueGraphBaseNode(DialogueGraphEditorWindow editorWindow, DialogueGraphView graphView)
        {
            _nodeGuid = Guid.NewGuid().ToString();
            _editorWindow = editorWindow;
            _graphView = graphView;
        }

        public Port AddInputNode(string portName, Port.Capacity capacity)
        {
            Port inputPort = CreatePort(Orientation.Horizontal, Direction.Input, capacity);
            inputPort.portName = portName;
            inputContainer.Add(inputPort);
            RefreshPorts();
            return inputPort;
        }

        public Port AddOutputNode(string portName, Port.Capacity capacity)
        {
            Port outputPort = CreatePort(Orientation.Horizontal, Direction.Output, capacity);
            outputPort.portName = portName;
            outputContainer.Add(outputPort);
            RefreshPorts();
            return outputPort;
        }

        protected Port CreatePort(Orientation orientation, Direction direction, Port.Capacity capacity)
        {
            return InstantiatePort(orientation, direction, capacity, typeof(float));
        }

        protected EnumField CreateEnumField(Enum type, EventCallback<ChangeEvent<Enum>> valueChangedCallback)
        {
            EnumField enumField = new EnumField();
            enumField.Init(type);
            enumField.RegisterValueChangedCallback(valueChangedCallback);
            mainContainer.Add(enumField);
            return enumField;
        }
    }
}