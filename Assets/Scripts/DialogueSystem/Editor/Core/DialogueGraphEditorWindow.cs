using RPG.DialogueSystem.Graph;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DialogueGraphEditorWindow : EditorWindow
    {
        private DialogueGraphSO _selectSO;          // 对话SO
        private DialogueGraphView _selectView;      // 对话节点编辑器图
        private Label _selectSONameLabel;           // 当前对话SO显示标签

        /// <summary>
        /// 打开编辑器窗口
        /// </summary>
        /// <returns>窗口</returns>
        [MenuItem("Window/DialogueGraph")]
        private static DialogueGraphEditorWindow ShowDialogueEditorWindow()
        {
            var window = GetWindow<DialogueGraphEditorWindow>(false, "DialogueGraph");
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
            var selectSO = EditorUtility.InstanceIDToObject(instanceID) as DialogueGraphSO;
            if (selectSO == null) return false;

            var window = ShowDialogueEditorWindow();
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
            if (selectSO == null)
            {
                _selectSONameLabel.text = "Current Object is Null";
                return;
            }
            
            _selectSO = selectSO;
            _selectSONameLabel.text = $"Current Object: {_selectSO.name}";
            _selectSO.Load(_selectView);
        }
        
        private void OnEnable()
        {
            // 添加单击资源监听
            Selection.selectionChanged += OnClickAsset;

            // 先创建窗口组件(Toolbar)
            CreateWindowComponents();
            // 再创建对话节点编辑器界面
            CreateDialogueGraphView();
            
            // 加载
            Load(_selectSO);
        }

        private void OnDisable()
        {
            // 移除单击资源监听
            Selection.selectionChanged -= OnClickAsset;
        }
        
        /// <summary>
        /// 创建窗口组件
        /// </summary>
        private void CreateWindowComponents()
        {
            VisualTreeAsset treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DialogueGraphAssetsPath.DialogueGraphWindowToolbar);
            VisualElement toolbarField = treeAsset.Instantiate();
            // 查早三个组件
            Button saveButton = toolbarField.Q<Button>(DialogueGraphUSSName.DIALOGUE_WINDOW_SAVE_BUTTON);
            Button loadButton = toolbarField.Q<Button>(DialogueGraphUSSName.DIALOGUE_WINDOW_LOAD_BUTTON);
            _selectSONameLabel = toolbarField.Q<Label>(DialogueGraphUSSName.DIALOGUE_WINDOW_SELECT_LABEL);
            // 按钮回调设置
            saveButton.clicked += delegate
            {
                _selectSO.Save(_selectView);
            };
            loadButton.clicked += delegate
            {
                _selectSO.Load(_selectView);
            };
            rootVisualElement.Add(toolbarField);
        }

        /// <summary>
        /// 创建节点编辑器
        /// </summary>
        private void CreateDialogueGraphView()
        {
            // 往窗口中添加GraphView
            _selectView = new DialogueGraphView(this);
            // 将节点编辑器加入窗口绘制中
            rootVisualElement.Add(_selectView);
        }
    }
}