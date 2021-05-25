using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphEndNode : DialogueGraphBaseNode
    {
        public Enum EndDialogueType
        {
            End,
            Repeat,
            Restart
        }

        private EndDialogueType endDialogueType; 
        private EnumField enumField;
            
        public DialogueGraphEndNode() {}
        
        public DialogueGraphEndNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(editorWindow, graphView)
        {
            title = "Start Node";
            SetPosition(new Rect(position, _defaultNodeSize));
            AddInputNode("Parent", Port.Capacity.Multi);
            enumField = new EnumField();
            enumField.Init(endDialogueType);
        }
    }
}