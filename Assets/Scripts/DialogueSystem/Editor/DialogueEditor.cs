using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;
using System;
namespace RPG.DialogueSystem
{
    public class DialogueEditor : EditorWindow
    {
        private Dialogue selectedDialogue;      // 对话物件
        [NonSerialized] private DialogueNode draggingNode = null;       // 拖拽节点
        [NonSerialized] private DialogueNode connectingNode = null;     // 待连接父节点
        [NonSerialized] private GUIStyle npcNodeStyle;                  // NPC节点背景风格
        [NonSerialized] private GUIStyle playerNodeStyle;               // 玩家节点背景风格
        [NonSerialized] private Vector2 dragOffSet;                     // 节点拖拽便宜量
        [NonSerialized] private Vector2 scrollerPos;                    // 滑动条坐标
        [NonSerialized] private Vector2 dragOriginPos;                  // 界面拖拽量
        [NonSerialized] private bool isDragScene;                       // 是否拖拽界面

        [NonSerialized] private int canvasSize = 4000;                  // 画布大小
        [NonSerialized] private int backGroundSize = 30;                // 画布缩放
        [NonSerialized] private GUILayoutOption[] textFieldOption = new GUILayoutOption[] { GUILayout.Height(30) };

        [MenuItem("Window/Dialogue")]
        public static void ShowDialogueWindow()
        {
            // 手动创建窗口
            var window = GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        // 当选中资源切换时调用(双击)
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            // 检测打开资源
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            // 打开资源则打开编辑窗口
            if (dialogue)
            {
                ShowDialogueWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            // 加载时添加监听
            Selection.selectionChanged += OnSelectionChanged;
            // 初始化GUIStyle
            npcNodeStyle = new GUIStyle();
            playerNodeStyle = new GUIStyle();
            // 设置背景
            npcNodeStyle.normal.background = EditorGUIUtility.Load("NPCNodeBackGround.png") as Texture2D;
            playerNodeStyle.normal.background = EditorGUIUtility.Load("PlayerNodeBackGround.png") as Texture2D;
            // 设置信息与背景框间距
            npcNodeStyle.padding = new RectOffset(10, 10, 10, 10);
            playerNodeStyle.padding = new RectOffset(10, 10, 10, 10);
        }

        // 当点击资源切换时调用(单击)
        private void OnSelectionChanged()
        {
            Dialogue tempDialogue = Selection.activeObject as Dialogue;
            if (tempDialogue != null)
            {
                // 只有当选择另一个对话物件时才更新
                selectedDialogue = tempDialogue;
                Repaint();
            }
        }
        private void OnGUI()
        {
            // 未选择任何对话节点
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Selected Dialogue");
                return;
            }
            // 开启滚动条区域
            scrollerPos = EditorGUILayout.BeginScrollView(scrollerPos);
            OnDrawBackGround();
            // 设置节点位置
            OnMoveNode();
            // 遍历对话
            foreach (DialogueNode node in selectedDialogue.GetNodes())
            {
                // 绘制节点间贝塞尔曲线
                DrawNodeConnection(node);
                // 绘制每个节点
                DrawNode(node);
            }
            // 结束滚动条区域
            EditorGUILayout.EndScrollView();
        }

        private void OnDrawBackGround()
        {
            // 设置画布大小
            Rect cansvasRect = GUILayoutUtility.GetRect(canvasSize, canvasSize);
            // 绘制画布背景
            Texture2D canvasTex = Resources.Load("CanvasBackGround") as Texture2D;
            Rect canvasCoord = new Rect(0, 0, canvasSize / backGroundSize, canvasSize / backGroundSize);
            GUI.DrawTextureWithTexCoords(cansvasRect, canvasTex, canvasCoord);
        }

        private void OnMoveNode()
        {
            // 处理当前鼠标事件
            // 准备拖拽节点 或 场景
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                // 获取鼠标位置所在的节点
                draggingNode = GetDragNode(Event.current.mousePosition);
                // 准备拖拽节点
                if (draggingNode != null)
                {
                    // 设置Inspector窗口显示的物体
                    Selection.activeObject = draggingNode;
                    // 记录偏移量
                    dragOffSet = draggingNode.sizeRect.position - Event.current.mousePosition;
                }
                // 准备拖拽场景
                else if (!isDragScene)
                {
                    // 设置Inspector窗口显示的物体
                    Selection.activeObject = selectedDialogue;
                    isDragScene = true;
                    dragOriginPos = Event.current.mousePosition;
                }
            }
            // 正在拖动节点
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                // 加上鼠标偏移量 对节点位置进行实时跟踪
                draggingNode.SetRectPosition(Event.current.mousePosition + dragOffSet);
                // GUI布局发生了改变
                GUI.changed = true;
            }
            // 正在拖拽场景
            else if (Event.current.type == EventType.MouseDrag && isDragScene)
            {
                scrollerPos += dragOriginPos - Event.current.mousePosition;
                GUI.changed = true;
            }
            // 结束拖拽节点
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                // 结束拖拽
                draggingNode = null;
            }
            // 结束拖拽场景
            else if (Event.current.type == EventType.MouseUp && isDragScene)
            {
                isDragScene = false;
            }
        }
        private DialogueNode GetDragNode(Vector2 mousePosition)
        {
            // 获取Dialogue数组中最靠后的节点 (越靠后证明越慢绘制 证明它在界面中被绘制在最顶层)
            DialogueNode dragNode = null;
            foreach (DialogueNode node in selectedDialogue.GetNodes())
            {
                if (node.sizeRect.Contains(mousePosition))
                {
                    dragNode = node;
                }
            }
            return dragNode;
        }

        private void DrawNodeConnection(DialogueNode node)
        {
            // 连接线与框图之间的间距
            float lineDis = 10f;
            // 设定起始点
            Vector3 startVec = new Vector2(node.sizeRect.xMax + lineDis, node.sizeRect.center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetChildren(node))
            {
                // 设置终点
                Vector3 endVec = new Vector2(childNode.sizeRect.xMin - lineDis, childNode.sizeRect.center.y);
                Vector3 bezierOffSet = new Vector3((startVec.x - endVec.x) * 0.8f, 0, 0);
                Handles.DrawBezier(startVec, endVec, startVec - bezierOffSet, endVec + bezierOffSet, Color.white, null, 5f);
            }
        }

        private void DrawNode(DialogueNode node)
        {
            // 开启边框绘制 判断节点类型
            switch (node.Speaker)
            {
                case DialogueSpeaker.PlayerChoice:
                    GUILayout.BeginArea(node.sizeRect, playerNodeStyle);
                    break;
                default:
                    GUILayout.BeginArea(node.sizeRect, npcNodeStyle);
                    break;
            }
            // 绘制信息
            node.SetText(EditorGUILayout.TextField(node.Text, textFieldOption));
            // 开启横向布局
            GUILayout.BeginHorizontal();
            // 添加增加节点的按钮
            if (GUILayout.Button("+"))
            {
                selectedDialogue.CreateNode(node);
            }
            // 添加连接其他节点的按钮
            if (connectingNode == null)
            {
                if (GUILayout.Button("Connect"))
                {
                    connectingNode = node;
                }
            }
            else if (connectingNode.UniqueID == node.UniqueID)
            {
                if (GUILayout.Button("Cancle"))
                {
                    connectingNode = null;
                }
            }
            else if (connectingNode.Children.Contains(node.UniqueID))
            {
                if (GUILayout.Button("UnLink"))
                {
                    connectingNode.UnLink(node);
                    connectingNode = null;
                }
            }
            else if (connectingNode.Parents.Contains(node.UniqueID))
            {
                if (GUILayout.Button("Cant Link To Parent"))
                {

                }
            }
            else
            {
                if (GUILayout.Button("Link"))
                {
                    connectingNode.Link(node);
                    connectingNode = null;
                }
            }
            // 添加删除节点的按钮
            if (GUILayout.Button("-"))
            {
                // 删除节点
                selectedDialogue.DeleteNode(node);
            }
            // 结束横向布局
            GUILayout.EndHorizontal();
            // 绘制事件ID
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("DialogueEnter EventID");
            node.SetEnterEventID(EditorGUILayout.TextField(node.EnterEventID));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("DialogueExit EventID");
            node.SetExitEventID(EditorGUILayout.TextField(node.ExitEventID));
            // 结束边框绘制
            GUILayout.EndArea();
        }
    }
}