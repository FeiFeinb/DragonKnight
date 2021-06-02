using System;
using System.Collections.Generic;
using RPG.SaveSystem;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
namespace RPG.DialogueSystem.Graph
{
    public abstract class DialogueGraphBaseNode : Node, ISavableNode
    {
        public string UniqueID => _uniqueID;
        public List<Port> InputBasePorts => _inputBasePorts;
        public List<Port> OutPutBasePorts => _outputBasePorts;
        
        protected readonly string _uniqueID;
        protected readonly List<Port> _inputBasePorts = new List<Port>();
        protected readonly List<Port> _outputBasePorts = new List<Port>();
        protected readonly DialogueGraphView _graphView;

        private readonly Vector2 _defaultNodeSize = new Vector2(200, 100);

        public DialogueGraphBaseNode(Vector2 position, DialogueGraphView graphView, string uniqueID = null)
        {
            _uniqueID = uniqueID ?? Guid.NewGuid().ToString();
            _graphView = graphView;
            SetPosition(new Rect(position, _defaultNodeSize));
            
            // 设置节点Style
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/LocalArts/EditorArts/DialogueGraphEditor/DialogueGraphNodeViewSheet.uss");
            styleSheets.Add(styleSheet);
        }

        protected virtual Port AddInputPort(string portName, Port.Capacity capacity)
        {
            Port inputPort = CreatePort(Orientation.Horizontal, Direction.Input, capacity);
            inputPort.portName = portName;
            _inputBasePorts.Add(inputPort);
            inputContainer.Add(inputPort);
            RefreshPorts();
            return inputPort;
        }

        protected virtual Port AddOutputPort(string portName, Port.Capacity capacity)
        {
            Port outputPort = CreatePort(Orientation.Horizontal, Direction.Output, capacity);
            outputPort.portName = portName;
            _outputBasePorts.Add(outputPort);
            outputContainer.Add(outputPort);
            RefreshPorts();
            return outputPort;
        }

        protected Port CreatePort(Orientation orientation, Direction direction, Port.Capacity capacity)
        {
            return InstantiatePort(orientation, direction, capacity, typeof(float));
        }

        protected EnumField CreateEnumField(Enum defaultValue, EventCallback<ChangeEvent<Enum>> valueChangedCallback = null)
        {
            EnumField enumField = new EnumField();
            enumField.Init(defaultValue);
            if (valueChangedCallback != null)
            {
                enumField.RegisterValueChangedCallback(valueChangedCallback);
            }
            return enumField;
        }

        protected TextField CreateTextField(EventCallback<ChangeEvent<string>> valueChangedCallback = null)
        {
            TextField textField = new TextField() { multiline = true };
            if (valueChangedCallback != null)
            {
                textField.RegisterValueChangedCallback(valueChangedCallback);
            }
            return textField;
        }

        protected ObjectField CreateObjectField<T>(EventCallback<ChangeEvent<UnityEngine.Object>> valueChangedCallback = null)
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(T),
                allowSceneObjects = false,
            };
            if (valueChangedCallback != null)
            {
                objectField.RegisterValueChangedCallback(valueChangedCallback);
            }
            return objectField;
        }

        protected Button CreateButton(string buttonName, Action buttonClickCallback)
        {
            Button button = new Button(buttonClickCallback);
            button.text = buttonName;
            return button;
        }

        public abstract DialogueGraphBaseNodeSaveData CreateNodeData();

        public abstract void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo);
    }
}