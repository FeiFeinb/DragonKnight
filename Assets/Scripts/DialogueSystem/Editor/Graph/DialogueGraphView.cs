using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphView : GraphView
    {
        private DialogueGraphEditorWindow _editorWindow;        // 编辑器窗口
        public DialogueGraphView(DialogueGraphEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            
            // 设置节点拖拽
            var dragger = new SelectionDragger()
            {
                // 不允许拖出边缘
                clampToParentEdges = true
            };
            // 其他按键触发节点拖拽
            dragger.activators.Add(new ManipulatorActivationFilter()
            {
                button = MouseButton.RightMouse,
                clickCount = 1,
                modifiers = EventModifiers.Alt
            });
            // 添加节点拖拽
            this.AddManipulator(dragger);
            
            // 设置界面缩放
            SetupZoom(ContentZoomer.DefaultMinScale, 2);
            // this.AddManipulator(new ContentZoomer());
            
            // 设置创建节点回调
            nodeCreationRequest += (info) =>
            {
                AddElement(new DialogueGraphBaseNode());
            };
            
            // 添加界面移动
            this.AddManipulator(new ContentDragger());
            // 添加举行选择框
            this.AddManipulator(new RectangleSelector());
            
            // 创建背景
            Insert(0, new GridBackground());
        }
    }
}