using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem
{
    public sealed class DialogueGraphChoiceNode : DialogueGraphBaseNode
    {
        private readonly Button _addPortButton;             // 添加端口
        private readonly TextField _contentField;           // 回应内容
        private readonly ObjectField _audioClipField;       // 回应音频

        public override bool expanded
        {
            get
            {
                return base.expanded;
            }
            set
            {
                base.expanded = value;
                // 使折叠时按钮能隐藏
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
        
        public DialogueGraphChoiceNode(Vector2 position, DialogueGraphView graphView, DialogueGraphChoiceNodeSaveData choiceNodeSaveData = null) : base(position, graphView, choiceNodeSaveData?.UniqueID)
        {
            title = "Choice Node";

            Port inputPort = AddInputPort("Input", Port.Capacity.Multi);
            inputPort.style.flexGrow = 1;

            if (choiceNodeSaveData == null)
            {
                AddOutputPort("Output", Port.Capacity.Single);
            }
            else
            {
                foreach (var portSaveData in choiceNodeSaveData.OutputPortsData)
                {
                    AddOutputPort(portSaveData.PortName, portSaveData.Capacity.SwitchType());
                }
            }
            
            _addPortButton = CreateButton("+", delegate
            {
                AddOutputPort("Output", Port.Capacity.Single);
            });
            topContainer.Insert(1, _addPortButton);
            
            _audioClipField = CreateObjectField<AudioClip>("AudioClip:");
            extensionContainer.Add(_audioClipField);
            
            _contentField = AddTextField();
            
            RefreshExpandedState();
        }

        protected override Port AddOutputPort(string portName, Port.Capacity capacity, Type portType = null)
        {
            Port outputPort = base.AddOutputPort(portName, capacity, portType);
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
            
            RefreshPorts();
            return outputPort;
        }
        
        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            DialogueGraphChoiceNodeSaveData choiceNodeSaveData = CreateBaseNodeData<DialogueGraphChoiceNodeSaveData>();
            choiceNodeSaveData.Content = _contentField.value;
            choiceNodeSaveData.TalkAudioClip = _audioClipField.value as AudioClip;
            return choiceNodeSaveData;
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            DialogueGraphChoiceNodeSaveData choiceNodeSaveData = stateInfo as DialogueGraphChoiceNodeSaveData;
            _contentField.value = choiceNodeSaveData.Content;
            _audioClipField.value = choiceNodeSaveData.TalkAudioClip;
        }
    }
}