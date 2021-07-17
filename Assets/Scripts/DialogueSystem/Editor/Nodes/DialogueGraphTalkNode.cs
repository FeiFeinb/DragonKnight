using System;
using System.Linq;
using RPG.DialogueSystem.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public sealed class DialogueGraphTalkNode : DialogueGraphMultiOutputNode
    {
        private readonly TextField _contentField;               // 对话内容
        private readonly EnumField _interlocutorField;          // 对话方类型
        private readonly ObjectField _characterInfoField;       // 对话角色信息
        private readonly ObjectField _audioClipField;           // 音频片段
        
        public DialogueGraphTalkNode(Vector2 position, DialogueGraphView graphView, DialogueGraphTalkNodeSaveData talkNodeSaveData = null) : base(position, graphView, talkNodeSaveData?.UniqueID)
        {
            title = "Talk Node";
            
            AddInputPort("Parents", Port.Capacity.Multi);

            GenerateOutputPort(talkNodeSaveData, "Child");
            
            _interlocutorField = CreateEnumField(InterlocutorType.NPC, "InterlocutorType:");
            extensionContainer.Add(_interlocutorField);

            _characterInfoField = CreateObjectField<DialogueCharacterInfoSO>("CharacterInfo:");
            extensionContainer.Add(_characterInfoField);
            
            _audioClipField = CreateObjectField<AudioClip>("AudioClip:");
            extensionContainer.Add(_audioClipField);
            
            _contentField = AddTextField();

            RefreshExpandedState();
        }

        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            DialogueGraphTalkNodeSaveData talkNodeSaveData = CreateBaseNodeData<DialogueGraphTalkNodeSaveData>();
            talkNodeSaveData.Content = _contentField.value;
            talkNodeSaveData.Interlocutor = (InterlocutorType) _interlocutorField.value;
            talkNodeSaveData.TalkAudioClip = _audioClipField.value as AudioClip;
            talkNodeSaveData.CharacterInfoSO = _characterInfoField.value as DialogueCharacterInfoSO;
            return talkNodeSaveData;
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