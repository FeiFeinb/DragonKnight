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

        public DialogueGraphBaseNode(Vector2 position, DialogueGraphEditorWindow editorWindow, DialogueGraphView graphView)
        {
            _nodeGuid = Guid.NewGuid().ToString();
            _editorWindow = editorWindow;
            _graphView = graphView;
            SetPosition(new Rect(position, _defaultNodeSize));
        }

        protected virtual Port AddInputPort(string portName, Port.Capacity capacity)
        {
            Port inputPort = CreatePort(Orientation.Horizontal, Direction.Input, capacity);
            inputPort.portName = portName;
            inputContainer.Add(inputPort);
            RefreshPorts();
            return inputPort;
        }

        protected virtual Port AddOutputPort(string portName, Port.Capacity capacity)
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

        protected EnumField CreateEnumField(Enum defaultValue, EventCallback<ChangeEvent<Enum>> valueChangedCallback)
        {
            EnumField enumField = new EnumField();
            enumField.Init(defaultValue);
            enumField.RegisterValueChangedCallback(valueChangedCallback);
            return enumField;
        }

        protected TextField CreateTextField(EventCallback<ChangeEvent<string>> valueChangedCallback)
        {
            TextField textField = new TextField() { multiline = true };
            textField.RegisterValueChangedCallback(valueChangedCallback);
            return textField;
        }

        protected ObjectField CreateObjectField<T>(EventCallback<ChangeEvent<UnityEngine.Object>> valueChangedCallback)
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(T),
                allowSceneObjects = false,
            };
            objectField.RegisterValueChangedCallback(valueChangedCallback);
            return objectField;
        }

        protected Button CreateButton(string buttonName, Action buttonClickCallback)
        {
            Button button = new Button(buttonClickCallback);
            button.text = buttonName;
            return button;
        }
    }
}