using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphTalkNode : DialogueGraphBaseNode
    {
        private readonly TextField _contentField;
        private readonly EnumField _interlocutorField;
        private readonly ObjectField _characterInfoField;
        private readonly ObjectField _audioClipField;
        
        public DialogueGraphTalkNode(Vector2 position, DialogueGraphView graphView, DialogueGraphTalkNodeSaveData talkNodeSaveData = null) : base(position, graphView, talkNodeSaveData?.UniqueID)
        {
            title = "Talk Node";
            
            Port inputPort = AddInputPort("Parents", Port.Capacity.Multi);
            inputPort.style.flexGrow = 1;

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

            Button _addChoiceButton = CreateButton("+", delegate
            {
                AddOutputPort("Children", Port.Capacity.Single);
            });
            topContainer.Insert(1, _addChoiceButton);
            
            _interlocutorField = CreateEnumField(InterlocutorType.NPC);
            extensionContainer.Add(_interlocutorField);
            
            _contentField = CreateTextField();
            extensionContainer.Add(_contentField);
            
            _characterInfoField = CreateObjectField<DialogueCharacterInfoSO>();
            extensionContainer.Add(_characterInfoField);
            
            _audioClipField = CreateObjectField<AudioClip>();
            extensionContainer.Add(_audioClipField);
            
            RefreshExpandedState();
        }

        protected override Port AddOutputPort(string portName, Port.Capacity capacity)
        {
            Port outputPort = CreatePort(Orientation.Horizontal, Direction.Output, capacity);
            outputPort.portName = portName;

            Button deleteButton = new Button(delegate
            {
                if (outputContainer.childCount <= 1) return;
                foreach (var edge in _graphView.edges.Where(edge => edge.output == outputPort))
                {
                    edge.input.Disconnect(edge);
                    edge.output.Disconnect(edge);
                    _graphView.RemoveElement(edge);
                }

                _outputBasePorts.Remove(outputPort);
                outputContainer.Remove(outputPort);
                RefreshPorts();
            });
            deleteButton.text = "X";
            outputPort.contentContainer.Add(deleteButton);
            
            _outputBasePorts.Add(outputPort);
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