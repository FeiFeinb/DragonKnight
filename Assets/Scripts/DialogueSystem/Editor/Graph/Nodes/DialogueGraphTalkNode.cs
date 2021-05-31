using System.Linq;
using System.Runtime.InteropServices;
using RPG.SaveSystem;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphTalkNode : DialogueGraphBaseNode
    {
        private string _content;                                // 对话内容
        private AudioClip _audioClip;                           // 对话音频
        private InterlocutorType _interlocutorType;             // 对话方
        private DialogueCharacterInfoSO _characterInfoSO;       // 对话角色信息
    
        private Label _characterNameLabel;
        private EnumField _interlocutorField;
        private TextField _contentField;
        private ObjectField _characterInfoField;
        private ObjectField _audioClipField;
        private Button _addChoiceButton;
        
        public DialogueGraphTalkNode(Vector2 position, DialogueGraphEditorWindow editorWindow, DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "Talk Node";
            
            Port inputPort = AddInputPort("Parents", Port.Capacity.Multi);
            inputPort.style.flexGrow = 1;
            
            // AddOutputPort("Children", Port.Capacity.Single);
            
            _addChoiceButton = CreateButton("+", delegate
            {
                AddOutputPort("Children", Port.Capacity.Single);
            });
            topContainer.Insert(1, _addChoiceButton);
            
            _interlocutorField = CreateEnumField(_interlocutorType, (value) =>
            {
                _interlocutorType = (InterlocutorType) value.newValue;
            });
            extensionContainer.Add(_interlocutorField);
            
            _contentField = CreateTextField((value) =>
            {
                _content = value.newValue;
            });
            extensionContainer.Add(_contentField);
            
            _characterInfoField = CreateObjectField<DialogueCharacterInfoSO>((value) =>
            {
                _characterInfoSO = value.newValue as DialogueCharacterInfoSO;
            });
            extensionContainer.Add(_characterInfoField);
            
            _audioClipField = CreateObjectField<AudioClip>((value) =>
            {
                _audioClip = value.newValue as AudioClip;
            });
            extensionContainer.Add(_audioClipField);
            
            RefreshExpandedState();
        }

        public override Port AddOutputPort(string portName, Port.Capacity capacity)
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
            return new DialogueGraphTalkNodeSaveData(_uniqueID, title, GetPosition(), _inputBasePorts, _outputBasePorts, _graphView)
            {
                _content = this._content,
                _audioClip = this._audioClip,
                _interlocutorType = this._interlocutorType,
                _characterInfoSO = this._characterInfoSO
            };
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}