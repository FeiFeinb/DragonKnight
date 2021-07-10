using System;
using System.Collections.Generic;
using DialogueSystem.Editor.Graph;
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
        public string UniqueID => _uniqueID; // 外部获取
        public List<Port> InputBasePorts => _inputBasePorts; // 外部获取
        public List<Port> OutPutBasePorts => _outputBasePorts; // 外部获取

        protected readonly string _uniqueID; // 节点ID
        protected readonly List<Port> _inputBasePorts = new List<Port>(); // 输入端口组
        protected readonly List<Port> _outputBasePorts = new List<Port>(); // 输出端口组
        protected readonly DialogueGraphView _graphView; // 所在节点编辑器图

        private readonly Vector2 _defaultNodeSize = new Vector2(200, 100); // 节点默认大小

        public DialogueGraphBaseNode(Vector2 position, DialogueGraphView graphView, string uniqueID = null)
        {
            // 信息初始化
            _uniqueID = uniqueID ?? Guid.NewGuid().ToString();
            _graphView = graphView;
            SetPosition(new Rect(position, _defaultNodeSize));

            // 创建隐藏的节点 用于节点折叠
            AddFakeOutputPort();
            
            // 设置节点Style
            StyleSheet styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(DialogueGraphAssetsPath.DialogueGraphNodeViewSheet);
            styleSheets.Add(styleSheet);
        }

        /// <summary>
        /// 添加输入端口
        /// </summary>
        /// <param name="portName">端口名字</param>
        /// <param name="capacity">端口连接多重性</param>
        /// <param name="portType">端口类型</param>
        /// <returns>生成的端口</returns>
        protected virtual Port AddInputPort(string portName, Port.Capacity capacity, Type portType = null)
        {
            Port inputPort = CreatePort(Orientation.Horizontal, Direction.Input, capacity,
                portType ?? typeof(DialogueGraphBaseNode));
            inputPort.portName = portName;
            // 添加输入端口记录
            _inputBasePorts.Add(inputPort);
            // 往节点中添加此端口
            inputContainer.Add(inputPort);
            RefreshPorts();
            return inputPort;
        }

        private Port AddFakeOutputPort()
        {
            Port fakePort = CreatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(DialogueGraphBaseNode));
            fakePort.name = "fakePort";
            outputContainer.Add(fakePort);
            return fakePort;
        }

        /// <summary>
        /// 添加输入端口
        /// </summary>
        /// <param name="portName">端口名字</param>
        /// <param name="capacity">端口连接多重性</param>
        /// <param name="portType">端口类型</param>
        /// <returns>生成的端口</returns>
        protected virtual Port AddOutputPort(string portName, Port.Capacity capacity, Type portType = null)
        {
            Port outputPort = CreatePort(Orientation.Horizontal, Direction.Output, capacity,
                portType ?? typeof(DialogueGraphBaseNode));
            outputPort.portName = portName;
            // 添加输出端口记录
            _outputBasePorts.Add(outputPort);
            // 往节点中添加此端口
            outputContainer.Add(outputPort);
            RefreshPorts();
            return outputPort;
        }


        /// <summary>
        /// 创建TextField
        /// </summary>
        /// <param name="valueChangedCallback">值变更回调</param>
        /// <returns>生成的TextField</returns>
        protected TextField AddTextField(EventCallback<ChangeEvent<string>> valueChangedCallback = null)
        {
            VisualTreeAsset treeAsset =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DialogueGraphAssetsPath
                    .DialogueGraphNodeHorizontalTextField);
            VisualElement textFieldGroup = treeAsset.Instantiate();

            // 在制作UI时已经设置好了name标签 所以直接按名查找
            TextField textField = textFieldGroup.Q<TextField>(DialogueGraphUSSName.DIALOGUE_NODE_TEXT_FIELD);
            if (valueChangedCallback != null)
            {
                textField.RegisterValueChangedCallback(valueChangedCallback);
            }

            extensionContainer.Add(textFieldGroup);
            return textField;
        }

        protected virtual (Foldout, VisualElement) AddFoldout(string labelStr)
        {
            VisualTreeAsset foldoutTreeAsset =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DialogueGraphAssetsPath
                    .DialogueGraphNodeObjectFieldFoldout);

            VisualElement objectFieldFoldout = foldoutTreeAsset.Instantiate();

            Foldout foldout = objectFieldFoldout.Q<Foldout>(DialogueGraphUSSName.DIALOGUE_NODE_FOLDOUT);
            foldout.Q<VisualElement>("unity-checkmark").style.alignSelf = new StyleEnum<Align>(Align.Center);
            Label label = foldout.Q<Label>();
            label.text = labelStr;
            label.name = DialogueGraphUSSName.DIALOGUE_NODE_LABEL;

            extensionContainer.Add(objectFieldFoldout);
            return (foldout, objectFieldFoldout);
        }

        /// <summary>
        /// 创建端口
        /// </summary>
        /// <param name="orientation">连线方向</param>
        /// <param name="direction">输入/输出类型</param>
        /// <param name="capacity">端口连接多重性</param>
        /// <param name="portType">端口类型</param>
        /// <returns>生成的端口</returns>
        protected Port CreatePort(Orientation orientation, Direction direction, Port.Capacity capacity, Type portType)
        {
            Port newPort = InstantiatePort(orientation, direction, capacity, portType);
            return newPort;
        }

        /// <summary>
        /// 创建EnumField
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="labelStr">标签字符</param>
        /// <param name="valueChangedCallback">值变更回调</param>
        /// <returns>生成的EnumField</returns>
        protected EnumField CreateEnumField(Enum defaultValue, string labelStr = null,
            EventCallback<ChangeEvent<Enum>> valueChangedCallback = null)
        {
            EnumField enumField = new EnumField
            {
                label = labelStr
            };
            // EnumField初始化
            enumField.Init(defaultValue);
            enumField.labelElement.name = DialogueGraphUSSName.DIALOGUE_NODE_LABEL;
            if (valueChangedCallback != null)
            {
                enumField.RegisterValueChangedCallback(valueChangedCallback);
            }

            return enumField;
        }

        /// <summary>
        /// 创建ObjectField
        /// </summary>
        /// <param name="labelStr">标签字符</param>
        /// <param name="valueChangedCallback">值变更回调</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns>生成的ObjectField</returns>
        protected ObjectField CreateObjectField<T>(string labelStr = null,
            EventCallback<ChangeEvent<UnityEngine.Object>> valueChangedCallback = null)
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(T),
                allowSceneObjects = false,
                label = labelStr
            };
            objectField.labelElement.name = DialogueGraphUSSName.DIALOGUE_NODE_LABEL;
            if (valueChangedCallback != null)
            {
                objectField.RegisterValueChangedCallback(valueChangedCallback);
            }

            return objectField;
        }

        /// <summary>
        /// 创建Button
        /// </summary>
        /// <param name="buttonName">Button名字</param>
        /// <param name="buttonClickCallback">点击回调</param>
        /// <returns>生成的Button</returns>
        protected Button CreateButton(string buttonName, Action buttonClickCallback)
        {
            Button button = new Button(buttonClickCallback)
            {
                text = buttonName
            };
            return button;
        }

        protected T CreateBaseNodeData<T>() where T : DialogueGraphBaseNodeSaveData
        {
            return Activator.CreateInstance(typeof(T), _uniqueID, GetPosition(), _inputBasePorts,
                _outputBasePorts, _graphView) as T;
        }

        public abstract bool CanConnectNode(DialogueGraphBaseNode targetNode);

        public abstract DialogueGraphBaseNodeSaveData CreateNodeData();

        public abstract void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo);
    }
}