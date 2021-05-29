using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphEndNode : DialogueGraphBaseNode
    {
        private EndDialogueNodeType _endDialogueNodeType = EndDialogueNodeType.Restart;
        private EnumField _enumField;


        public DialogueGraphEndNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "End Node";
            AddInputPort("Parents", Port.Capacity.Multi);
            _enumField = CreateEnumField(_endDialogueNodeType, (value) =>
            {
                _endDialogueNodeType = (EndDialogueNodeType) value.newValue;
            });
            extensionContainer.Add(_enumField);
            RefreshExpandedState();
        }

        public override DialogueGraphBaseNodeSaveData CreateState()
        {
            return new DialogueGraphEndNodeSaveData(_guid, title, GetPosition())
            {
                _endType = this._endDialogueNodeType
            };
        }

        public override void LoadState(DialogueGraphBaseNodeSaveData stateInfo)
        {
            throw new NotImplementedException();
        }
    }
}