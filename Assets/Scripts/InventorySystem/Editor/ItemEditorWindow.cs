using System;
using System.Collections.Generic;
using System.Linq;
using RPG.InventorySystem;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ItemEditorWindow : EditorWindow
{
    private readonly string createPath = "Assets/Resources/Items/";

    /// <summary>
    /// 捕获的BaseItemObject数列
    /// </summary>
    private readonly List<BaseItemObject> _baseItemObjects = new List<BaseItemObject>();

    /// <summary>
    /// 双击选中的BaseItemObject
    /// </summary>
    private static BaseItemObject _doubleClickSelectObject = null;

    /// <summary>
    /// 编辑器窗口中的ScrollView
    /// </summary>
    private ScrollView _scrollView;

    /// <summary>
    /// 编辑器窗口中的ListView的ContentContainer
    /// </summary>
    private VisualElement _listViewContainer;

    /// <summary>
    /// 创建选项中的下拉列表
    /// </summary>
    private DropdownField _dropdownField;

    private TextField _textField;

    private ListView itemListView;

    private const string ScrollViewName = "details-scroll-view";

    private const string ListViewContainerName = "unity-content-container";

    private const string ItemListViewName = "item-list-view";

    private const string RefreshButtonName = "refresh-button";

    private const string SaveAllButtonName = "save-all-button";

    private const string DeleteButtonName = "delete-button";

    private const string ToolbarRightContainerName = "toolbar-right-container";
    
    private const string ItemNameTextFieldName = "item-name-textfield";
    
    private const string CreateButtonName = "create-button";

    private const string ItemChooseListViewLabelName = "item-choose-list-view-label";
    
    private const string ItemListViewLabelName = "item-list-view-label";
    
    private const string ImageContainerName = "image-container";
    
    private const string PropertyContainerName = "property-container";
    /// <summary>
    /// 创建物品编辑器窗口
    /// </summary>
    /// <returns>新创建的窗口</returns>
    [MenuItem("Window/BaseItemEditor")]
    public static ItemEditorWindow CreateWindow()
    {
        ItemEditorWindow window = GetWindow<ItemEditorWindow>(false, nameof(ItemEditorWindow));
        window.minSize = new Vector2(800, 600);
        return window;
    }

    /// <summary>
    /// 双击BaseItemObject资源回调
    /// </summary>
    /// <param name="instanceID">资源ID</param>
    /// <param name="line"></param>
    /// <returns>是否已处理此次回调</returns>
    [OnOpenAsset(0)]
    public static bool OnDoubleClickAsset(int instanceID, int line)
    {
        _doubleClickSelectObject = EditorUtility.InstanceIDToObject(instanceID) as BaseItemObject;
        if (_doubleClickSelectObject == null) return false;

        CreateWindow();
        return true;
    }

    public void OnEnable()
    {
        // 导入UXML并添加
        VisualTreeAsset treeAsset =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Scripts/InventorySystem/Editor/ItemEditorWindow.uxml");
        VisualElement itemEditorElement = treeAsset.Instantiate();
        itemEditorElement.style.flexGrow = 1;
        rootVisualElement.Add(itemEditorElement);

        // 获取UXML中的元素
        _scrollView = itemEditorElement.Q<ScrollView>(ScrollViewName);
        _listViewContainer = itemEditorElement.Q<VisualElement>(ListViewContainerName);

        // 创建Toolbar按钮
        CreateToolbarButtons();
        // 创建编辑栏
        CreateItemEditorBar();
    }

    private void Refresh()
    {
        _baseItemObjects.Clear();
        string[] itemsGuid = AssetDatabase.FindAssets("t:BaseItemObject");
        foreach (var t in itemsGuid)
        {
            string path = AssetDatabase.GUIDToAssetPath(t);
            _baseItemObjects.Add(AssetDatabase.LoadAssetAtPath<BaseItemObject>(path));
        }

        itemListView = rootVisualElement.Q<ListView>(ItemListViewName);
        itemListView.Refresh();
    }


    /// <summary>
    /// 创建Toolbar
    /// </summary>
    private void CreateToolbarButtons()
    {
        // 找到刷新按钮并注册刷新回调
        rootVisualElement.Q<Button>(RefreshButtonName).clickable.clicked += Refresh;

        // 找到保存按钮并注册保存回调
        rootVisualElement.Q<Button>(SaveAllButtonName).clickable.clicked += AssetDatabase.SaveAssets;

        rootVisualElement.Q<Button>(DeleteButtonName).clickable.clicked += delegate
        {
            // 删除选中的Scriptable
            AssetDatabase.DeleteAsset($"{createPath}{(itemListView.selectedItem as BaseItemObject).name}.asset");
            Refresh();
            if (_baseItemObjects.Count > 0)
            {
                itemListView.selectedIndex = (int) Mathf.Clamp(itemListView.selectedIndex - 1, 0, Mathf.Infinity);
            }
            else
            {
                _scrollView.Clear();
            }
        };


        VisualElement container = rootVisualElement.Q<VisualElement>(ToolbarRightContainerName);
        _dropdownField = new DropdownField(new List<string> {"Equipment", "Food"}, 0);
        container.Add(_dropdownField);

        _textField = container.Q<TextField>(ItemNameTextFieldName);

        rootVisualElement.Q<Button>(CreateButtonName).clickable.clicked += () =>
        {
            BaseItemObject createObj;
            // 创建ScriptableObject
            // TODO: 优化创建资源和删除资源的逻辑 以及选择指向的逻辑
            switch (_dropdownField.value)
            {
                case "Equipment":
                    createObj = CreateInstance<EquipmentItemObject>();
                    AssetDatabase.CreateAsset(createObj, $"{createPath}{_textField.text}.asset");
                    break;
                case "Food":
                    createObj = CreateInstance<FoodItemObject>();
                    AssetDatabase.CreateAsset(createObj, $"{createPath}{_textField.text}.asset");
                    break;
                default:
                    throw new Exception($"Cant Find CreateType {_dropdownField.value}");
            }

            Refresh();
            // 将选择指向新创建的物体
            itemListView.selectedIndex = _baseItemObjects.IndexOf(createObj);
        };
    }

    /// <summary>
    /// 创建编辑栏
    /// </summary>
    private void CreateItemEditorBar()
    {
        // 开始前清空
        _baseItemObjects.Clear();
        // 由类型BaseIemObject找到对应的资源 并添加至数列
        string[] itemsGuid = AssetDatabase.FindAssets("t:BaseItemObject");
        foreach (var t in itemsGuid)
        {
            string path = AssetDatabase.GUIDToAssetPath(t);
            _baseItemObjects.Add(AssetDatabase.LoadAssetAtPath<BaseItemObject>(path));
        }

        // 初始化ListView
        itemListView = rootVisualElement.Q<ListView>(ItemListViewName);
        itemListView.makeItem = () => new Label();
        itemListView.itemsSource = _baseItemObjects;
        itemListView.bindItem = (element, i) =>
        {
            Label label = element as Label;
            label.name = itemListView.selectedIndex == i ? ItemChooseListViewLabelName : ItemListViewLabelName;
            label.text = _baseItemObjects[i].name;
        };

        // 读取UXML
        var visualTree =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Scripts/InventorySystem/Editor/ItemEditorImageGroup.uxml");

        itemListView.onSelectionChange += objects =>
        {
            _scrollView.Clear();

            // 生成ImageGroup并添加进ScrollView中
            VisualElement imageGroup = visualTree.Instantiate();
            _scrollView.Add(imageGroup);
            VisualElement imageContainer = imageGroup.Q<VisualElement>(ImageContainerName);
            VisualElement propertyContainer = imageGroup.Q<VisualElement>(PropertyContainerName);

            // 创建可序列化物体
            BaseItemObject itemObject = objects.FirstOrDefault() as BaseItemObject;
            SerializedObject serializedItemObject = new SerializedObject(itemObject);

            // 创建物品图标预览图
            Image itemImage = new Image
            {
                sprite = itemObject.sprite,
                style = {alignSelf = new StyleEnum<Align>(Align.Center)}
            };
            imageContainer.Add(itemImage);

            // 获取物体中的可序列化属性
            SerializedProperty serializedProperty = serializedItemObject.GetIterator();
            // 逐个遍历
            serializedProperty.Next(true);
            while (serializedProperty.NextVisible(false))
            {
                PropertyField propertyField = new PropertyField(serializedProperty);
                // 脚本属性栏不允许修改
                propertyField.SetEnabled(serializedProperty.name != "m_Script");
                propertyField.Bind(serializedItemObject);
                // 为Sprite栏添加修改Image的回调
                if (serializedProperty.name == "sprite")
                {
                    propertyField.RegisterValueChangeCallback(evt =>
                    {
                        itemImage.sprite = evt.changedProperty.objectReferenceValue as Sprite;
                    });
                }

                // 判断propertyField应添加至哪个容器
                if (serializedProperty.name == "m_Script" || serializedProperty.name == "sprite")
                {
                    propertyContainer.Add(propertyField);
                }
                else
                {
                    _scrollView.Add(propertyField);
                }
            }

            // 设置已被选择的栏的Style
            for (int i = 0; i < _listViewContainer.childCount; i++)
            {
                _listViewContainer[i].name = itemListView.selectedIndex == i ? ItemChooseListViewLabelName : ItemListViewLabelName;
            }
        };

        // 设置双击资源
        if (_doubleClickSelectObject != null)
        {
            int index = _baseItemObjects.FindIndex(o => o.Equals(_doubleClickSelectObject));
            itemListView.selectedIndex = index;
            _doubleClickSelectObject = null;
        }

        itemListView.Refresh();
    }
}