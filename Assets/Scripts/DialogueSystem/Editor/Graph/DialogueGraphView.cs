using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
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
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            // this.AddManipulator(new ContentZoomer());
            
            // 设置创建节点回调
            nodeCreationRequest += (info) =>
            {
                AddElement(new DialogueGraphEndNode(new Vector2(0, 0), _editorWindow, this));
            };
            
            // 添加界面移动
            this.AddManipulator(new ContentDragger());
            // 添加举行选择框
            this.AddManipulator(new RectangleSelector());
            
            // 创建背景
            Insert(0, new GridBackground());

            DialogueSearchWindowProvider provider = ScriptableObject.CreateInstance<DialogueSearchWindowProvider>();
            provider.OnSelectEntryCallback = OnEntry;
            nodeCreationRequest = (info) =>
            {
                SearchWindow.Open(new SearchWindowContext(info.screenMousePosition), provider);
            };
        }
        

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            // 节点不允许自我连接 只允许方向不同且类型相同的端口连接
            return ports.Where(port => startPort.node != port.node && startPort.direction != port.direction && port.portType == startPort.portType).ToList();
        }

        private bool OnEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            if (!(SearchTreeEntry.userData is Type nodeType)) return false;
            Vector2 nodePosition = contentViewContainer.WorldToLocal(context.screenMousePosition - _editorWindow.position.position);
            DialogueGraphBaseNode node = Activator.CreateInstance(nodeType, nodePosition, _editorWindow, this) as DialogueGraphBaseNode;
            AddElement(node);
            return true;
        }
    }
}