using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphView : GraphView
    {
        private DialogueGraphEditorWindow _editorWindow;
        public DialogueGraphView(DialogueGraphEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            // 设置节点拖拽
            var dragger = new SelectionDragger()
            {
                // 不允许拖出边缘
                clampToParentEdges = true
            };
            // 其他触发案件
            dragger.activators.Add(new ManipulatorActivationFilter()
            {
                button = MouseButton.RightMouse,
                clickCount = 1,
                modifiers = EventModifiers.Alt
            });
            this.AddManipulator(dragger);
            
            // 设置缩放
            // this.AddManipulator(new ContentZoomer());
            SetupZoom(ContentZoomer.DefaultMinScale, 2);
            
            // 设置创建节点回调
            nodeCreationRequest += (info) =>
            {
                AddElement(new DialogueGraphNodeView());
            };
            
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new RectangleSelector());
            // 创建背景
            GridBackground background = new GridBackground();
            Insert(0, new GridBackground());
            // background.StretchToParentSize();
        }
    }
}