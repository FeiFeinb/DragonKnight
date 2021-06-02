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
        private readonly EnumField _enumField;

        public DialogueGraphEndNode(Vector2 position,
            DialogueGraphView graphView, DialogueGraphEndNodeSaveData endNodeSaveData = null) : base(position, graphView, endNodeSaveData?.UniqueID)
        {
            title = "End Node";
            AddInputPort("Parents", Port.Capacity.Multi);
            _enumField = CreateEnumField(EndDialogueNodeType.End);
            extensionContainer.Add(_enumField);
            RefreshExpandedState();
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            return new DialogueGraphEndNodeSaveData(_uniqueID, GetPosition(), _inputBasePorts, _outputBasePorts, _graphView)
            {
                EndType = (EndDialogueNodeType)_enumField.value
            };
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            _enumField.value = (stateInfo as DialogueGraphEndNodeSaveData).EndType;
        }
    }
}