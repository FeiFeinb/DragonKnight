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

        private EndDialogueType endDialogueType = EndDialogueType.Restart;
        private EnumField enumField;


        public DialogueGraphEndNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(editorWindow, graphView)
        {
            title = "End Node";
            SetPosition(new Rect(position, _defaultNodeSize));
            AddInputNode("Parent", Port.Capacity.Multi);
            CreateEnumField(endDialogueType, (value) =>
            {
                endDialogueType = (EndDialogueType) value.newValue;
            });
        }
    }
}