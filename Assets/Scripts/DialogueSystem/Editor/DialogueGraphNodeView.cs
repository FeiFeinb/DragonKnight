using UnityEditor.Experimental.GraphView;
namespace RPG.DialogueSystem
{
    public class DialogueGraphNodeView : Node
    {
        public DialogueGraphNodeView() : base()
        {
            title = "Dialogue GraphNode";
            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
                typeof(Port));
            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(Port));
            inputContainer.Add(inputPort);
            outputPort.Add(outputPort);
        }
    }
}