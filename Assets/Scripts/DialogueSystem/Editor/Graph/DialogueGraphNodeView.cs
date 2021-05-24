using UnityEditor.Experimental.GraphView;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphNodeView : Node
    {
        public DialogueGraphNodeView()
        {
            // 节点标题
            title = "Dialogue GraphNode";
            // 创建入连接口
            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputPort.portName = "Parents";
            inputContainer.Add(inputPort);
            // 创建出连接口
            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
            outputPort.portName = "Children";
            outputContainer.Add(outputPort);
        }
    }
}