using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphEndNode : DialogueGraphBaseNode
    {
        private EndDialogueNodeType _endDialogueNodeType;
        
        private EnumField _enumField;

        public DialogueGraphEndNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView, DialogueGraphEndNodeSaveData endNodeSaveData = null) : base(position, editorWindow, graphView, endNodeSaveData?._uniqueID)
        {
            title = "End Node";
            _endDialogueNodeType = endNodeSaveData?._endType ?? EndDialogueNodeType.End;
            AddInputPort("Parents", Port.Capacity.Multi);
            _enumField = CreateEnumField(_endDialogueNodeType, (value) =>
            {
                _endDialogueNodeType = (EndDialogueNodeType) value.newValue;
            });
            extensionContainer.Add(_enumField);
            RefreshExpandedState();
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            return new DialogueGraphEndNodeSaveData(_uniqueID, GetPosition(), _inputBasePorts, _outputBasePorts, _graphView)
            {
                _endType = this._endDialogueNodeType
            };
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            throw new NotImplementedException();
        }
    }
}