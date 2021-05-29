using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphStartNode : DialogueGraphBaseNode
    {
        public DialogueGraphStartNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "Start Node";
            AddOutputPort("Children", Port.Capacity.Single);
        }

        public override DialogueGraphBaseNodeSaveData CreateState()
        {
            return new DialogueGraphStartNodeSaveData(_guid, title, GetPosition());
        }

        public override void LoadState(DialogueGraphBaseNodeSaveData stateInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}