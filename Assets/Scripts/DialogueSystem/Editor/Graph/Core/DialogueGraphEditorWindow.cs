using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Graph;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueGraphEditorWindow : EditorWindow
    {
        private DialogueGraphSO _selectSO;          // 对话SO
        private DialogueGraphView _selectView;      // 对话节点编辑器窗口
        
        private Label _selectSONameLabel;           // 当前对话SO显示标签

        [MenuItem("Window/DialogueGraph")]
        private static DialogueGraphEditorWindow ShowDialogueGraphWindow()
        {
            DialogueGraphEditorWindow window = GetWindow<DialogueGraphEditorWindow>(false, "DialogueGraph");
            window.minSize = new Vector2(400, 300);
            return window;
        }

        /// <summary>
        /// 双击打开资源
        /// </summary>
        /// <param name="instanceID">资源ID</param>
        /// <param name="line"></param>
        /// <returns>处理结果</returns>
        [OnOpenAsset(0)]
        private static bool OnDoubleClickAsset(int instanceID, int line)
        {
            DialogueGraphSO selectSO = EditorUtility.InstanceIDToObject(instanceID) as DialogueGraphSO;
            if (selectSO == null) return false;

            DialogueGraphEditorWindow window = ShowDialogueGraphWindow();
            // OnOpenAsset回调不包含Selection Change
            window.Load(selectSO);
            return true;
        }

        /// <summary>
        /// 单击资源
        /// </summary>
        private void OnClickAsset()
        {
            // 重新绘制编辑器界面
            Load(Selection.activeObject as DialogueGraphSO);
        }

        /// <summary>
        /// 加载对话SO
        /// </summary>
        /// <param name="selectSO">对话SO</param>
        private void Load(DialogueGraphSO selectSO)
        {
            if (selectSO == null) return;
            
            _selectSO = selectSO;
            // 刷新窗口上端Label显示
            _selectSONameLabel.text = _selectSO == null ? "Current Object is Null" : $"Current Object: {_selectSO.name}";
        }
        
        private void OnEnable()
        {
            // 添加单击资源监听
            Selection.selectionChanged += OnClickAsset;
            
            // 先创建窗口组件(Toolbar)
            CreateWindowComponents();
            // 再创建对话节点编辑器界面
            CreateDialogueGraphView();
        }

        private void OnDisable()
        {
            // 移除单击资源监听
            Selection.selectionChanged -= OnClickAsset;
        }
        

        private void CreateWindowComponents()
        {
            // 创建各个组件
            Toolbar windowToolbar = new Toolbar();
            Button saveButton = new Button();
            _selectSONameLabel = new Label();
            
            // 传统艺能
            saveButton.text = "Save";
            saveButton.clicked += delegate
            {
                Debug.Log("Save Button Clicked");
                _selectSO.Save(_selectView);
            };
            
            // 设置顶部信息显示栏Style
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/LocalArts/EditorArts/DialogueGraphEditor/DialogueGraphViewSheet.uss"); 
            rootVisualElement.styleSheets.Add(styleSheet);
            _selectSONameLabel.AddToClassList("_selectSONameLabelSheet");
            
            // 将Button加入Toolbar中
            windowToolbar.Add(saveButton);
            // 将Label加入Toolbar中
            windowToolbar.Add(_selectSONameLabel);
            // 将Toolbar加入窗口绘制中
            rootVisualElement.Add(windowToolbar);
        }

        private void CreateDialogueGraphView()
        {
            // 往窗口中添加GraphView
            _selectView = new DialogueGraphView(this)
            {
                style = {flexGrow = 1},
            };
            // _selectView.StretchToParentSize();
            // 将节点编辑器加入窗口绘制中
            rootVisualElement.Add(_selectView);
        }
    }
}