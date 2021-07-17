using System;
using System.Linq;
using RPG.DialogueSystem.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public sealed class DialogueGraphChoiceNode : DialogueGraphMultiOutputNode
    {
        private readonly TextField _contentField;           // 回应内容
        private readonly ObjectField _audioClipField;       // 回应音频
        
        
        public DialogueGraphChoiceNode(Vector2 position, DialogueGraphView graphView, DialogueGraphChoiceNodeSaveData choiceNodeSaveData = null) : base(position, graphView, choiceNodeSaveData?.UniqueID)
        {
            title = "Choice Node";
            
            AddInputPort("Input", Port.Capacity.Multi);
            
             GenerateOutputPort(choiceNodeSaveData, "Output");
            
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