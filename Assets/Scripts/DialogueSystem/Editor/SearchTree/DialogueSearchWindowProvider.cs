using System.Collections.Generic;
using DialogueSystem.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DialogueSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        public delegate bool SearchWindowOnSelectEntryCallback(SearchTreeEntry SearchTreeEntry, SearchWindowContext context);

        public SearchWindowOnSelectEntryCallback OnSelectEntryCallback;     // 选项点击委托
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> entries = new List<SearchTreeEntry>
            {
                CreateSearchTreeGroupEntry("Create Nodes", 0),
                CreateSearchTreeGroupEntry("Basic Nodes", 1),
                CreateSearchTreeEntry<DialogueGraphStartNode>("Start Node", 2),
                CreateSearchTreeEntry<DialogueGraphEndNode>("End Node", 2),
                CreateSearchTreeGroupEntry("Main Nodes", 1),
                CreateSearchTreeEntry<DialogueGraphChoiceNode>("Choice Node", 2),
                CreateSearchTreeEntry<DialogueGraphConditionNode>("Condition Node", 2),
                CreateSearchTreeEntry<DialogueGraphEventNode>("Event Node", 2),
                CreateSearchTreeEntry<DialogueGraphTalkNode>("Talk Node", 2)
            };
            return entries;
        }

        /// <summary>
        /// 创建搜索树组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="levelIndex">深度索引</param>
        /// <returns>创建的搜索树组</returns>
        private SearchTreeGroupEntry CreateSearchTreeGroupEntry(string groupName, int levelIndex)
        {
            return new SearchTreeGroupEntry(new GUIContent(groupName)) {level = levelIndex};
        }

        /// <summary>
        /// 创建搜索树选项
        /// </summary>
        /// <param name="selectionName">选项名称</param>
        /// <param name="levelIndex">深度索引</param>
        /// <typeparam name="T">创建节点类型</typeparam>
        /// <returns>创建的搜索树选项</returns>
        private SearchTreeEntry CreateSearchTreeEntry<T>(string selectionName, int levelIndex) where T : DialogueGraphBaseNode
        {
            return new SearchTreeEntry(new GUIContent(selectionName)) {level = levelIndex, userData = typeof(T)};
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            if (OnSelectEntryCallback == null) return false;
            return OnSelectEntryCallback(SearchTreeEntry, context);
        }
    }
}