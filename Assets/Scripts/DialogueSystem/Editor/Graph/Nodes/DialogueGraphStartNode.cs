using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphStartNode : DialogueGraphBaseNode
    {

        public DialogueGraphStartNode(Vector2 position, DialogueGraphEditorWindow editorWindow,
            DialogueGraphView graphView) : base(editorWindow, graphView)
        {
            title = "Start Node";
            SetPosition(new Rect(position, _defaultNodeSize));
            AddOutputNode("Children", Port.Capacity.Single);
        }
    }
}