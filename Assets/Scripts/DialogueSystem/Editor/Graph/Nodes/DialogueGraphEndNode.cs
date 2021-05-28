using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphEndNode : DialogueGraphBaseNode
    {
        private enum EndDialogueType
        {
            End,
            Repeat,
            Restart
        }

        private EndDialogueType _endDialogueType = EndDialogueType.Restart;
        private EnumField _enumField;


        public DialogueGraphEndNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "End Node";
            AddInputPort("Parents", Port.Capacity.Multi);
            _enumField = CreateEnumField(_endDialogueType, (value) =>
            {
                _endDialogueType = (EndDialogueType) value.newValue;
            });
            extensionContainer.Add(_enumField);
            RefreshExpandedState();
        }
    }
}