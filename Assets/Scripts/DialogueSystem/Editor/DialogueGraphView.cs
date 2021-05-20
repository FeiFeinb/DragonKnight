using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem
{
    public class DialogueGraphView : GraphView
    {
        public DialogueGraphView() : base()
        {
            AddElement(new DialogueGraphNodeView());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new ContentZoomer());
        }
    }
}