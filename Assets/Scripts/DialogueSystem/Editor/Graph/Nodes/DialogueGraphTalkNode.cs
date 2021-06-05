using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphTalkNode : DialogueGraphBaseNode
    {
        private readonly Button _addPortButton;                 // 添加端口Button
        
        private readonly TextField _contentField;               // 对话内容TextField
        private readonly EnumField _interlocutorField;          // 对话方类型EnumField
        private readonly ObjectField _characterInfoField;       // 对话角色信息ObjectField
        private readonly ObjectField _audioClipField;           // 音频片段ObjectField

        public override bool expanded
        {
            get
            {
                return base.expanded;
            }
            set
            {
                base.expanded = value;
                SetElementDisplay(_addPortButton, expanded);
                RefreshExpandedState();
            }
        }

        private void SetElementDisplay(VisualElement element, bool display)
        {
            element.style.display = display
                ? new StyleEnum<DisplayStyle>(DisplayStyle.Flex)
                : new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
        
        public DialogueGraphTalkNode(Vector2 position, DialogueGraphView graphView, DialogueGraphTalkNodeSaveData talkNodeSaveData = null) : base(position, graphView, talkNodeSaveData?.UniqueID)
        {
            title = "Talk Node";
            
            Port inputPort = AddInputPort("Parents", Port.Capacity.Multi);
            inputPort.style.flexGrow = 1;

            // 创建隐藏的节点
            outputContainer.Add(CreatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, "fakePort")); 
            
            if (talkNodeSaveData == null)
            {
                AddOutputPort("Children", Port.Capacity.Single);
            }
            else
            {
                foreach (var portSaveData in talkNodeSaveData.OutputPortsData)
                {
                    AddOutputPort(portSaveData.PortName, portSaveData.Capacity);
                }
            }

            // 创建添加端口Button
            _addPortButton = CreateButton("+", delegate
            {
                AddOutputPort("Children", Port.Capacity.Single);
            });
            topContainer.Insert(1, _addPortButton);
            
            _interlocutorField = CreateEnumField(InterlocutorType.NPC, "Interlocutor:");
            extensionContainer.Add(_interlocutorField);

            _characterInfoField = CreateObjectField<DialogueCharacterInfoSO>("CharacterInfo:");
            extensionContainer.Add(_characterInfoField);
            
            _audioClipField = CreateObjectField<AudioClip>("AudioClip:");
            extensionContainer.Add(_audioClipField);
            
            _contentField = AddTextField();

            RefreshExpandedState();
        }
        
        protected override Port AddOutputPort(string portName, Port.Capacity capacity)
        {
            Port outputPort = CreatePort(Orientation.Horizontal, Direction.Output, capacity);
            outputPort.portName = portName;

            // 创建删除端口Button
            Button deleteButton = CreateButton("X", delegate
            {
                // 不允许清空所有端口
                if (_outputBasePorts.Count <= 1) return;
                // 移除端口间连线
                foreach (var edge in _graphView.edges.Where(edge => edge.output == outputPort))
                {
                    edge.input.Disconnect(edge);
                    edge.output.Disconnect(edge);
                    _graphView.RemoveElement(edge);
                }
                // 移除输出节点记录
                _outputBasePorts.Remove(outputPort);
                // 从节点中移除端口
                outputContainer.Remove(outputPort);
                RefreshPorts();
            });
            outputPort.contentContainer.Add(deleteButton);
            
            // 添加输入端口记录
            _outputBasePorts.Add(outputPort);
            // 往节点中添加此端口
            outputContainer.Add(outputPort);
            RefreshPorts();
            return outputPort;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            return new DialogueGraphTalkNodeSaveData(_uniqueID, GetPosition(), _inputBasePorts, _outputBasePorts, _graphView)
            {
                Content = _contentField.value,
                Interlocutor = (InterlocutorType)_interlocutorField.value,
                TalkAudioClip = _audioClipField.value as AudioClip,
                CharacterInfoSO = _characterInfoField.value as DialogueCharacterInfoSO
            };
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            DialogueGraphTalkNodeSaveData talkNodeSaveData = stateInfo as DialogueGraphTalkNodeSaveData;
            _contentField.value = talkNodeSaveData.Content;
            _interlocutorField.value = talkNodeSaveData.Interlocutor;
            _audioClipField.value = talkNodeSaveData.TalkAudioClip;
            _characterInfoField.value = talkNodeSaveData.CharacterInfoSO;
        }
    }
}