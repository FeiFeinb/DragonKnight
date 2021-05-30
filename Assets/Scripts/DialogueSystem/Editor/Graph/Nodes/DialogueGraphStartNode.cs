using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphStartNode : DialogueGraphBaseNode
    {
        public DialogueGraphStartNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(position, editorWindow, graphView)
        {
            title = "Start Node";
            AddOutputPort("Children", Port.Capacity.Single);
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            return new DialogueGraphStartNodeSaveData(_uniqueID, title, GetPosition(), _inputBasePorts, _outputBasePorts, _graphView);
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}