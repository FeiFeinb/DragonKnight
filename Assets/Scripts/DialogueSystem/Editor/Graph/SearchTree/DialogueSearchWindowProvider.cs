using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        public delegate bool SearchWindowOnSelectEntryCallback(SearchTreeEntry SearchTreeEntry, SearchWindowContext context);

        public SearchWindowOnSelectEntryCallback OnSelectEntryCallback;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> entries = new List<SearchTreeEntry>();
            entries.Add(CreateSearchTreeGroupEntry("Create Nodes", 0));
            entries.Add(CreateSearchTreeGroupEntry("Basic Nodes", 1));
            entries.Add(CreateSearchTreeEntry<DialogueGraphStartNode>("Start Node", 2));
            entries.Add(CreateSearchTreeEntry<DialogueGraphEndNode>("End Node", 2));
            entries.Add(CreateSearchTreeGroupEntry("Main Nodes", 1));
            entries.Add(CreateSearchTreeEntry<DialogueGraphTalkNode>("Talk Node", 2));
            return entries;
        }

        private SearchTreeGroupEntry CreateSearchTreeGroupEntry(string groupName, int levelIndex)
        {
            return new SearchTreeGroupEntry(new GUIContent(groupName)) {level = levelIndex};
        }

        private SearchTreeEntry CreateSearchTreeEntry<T>(string nodeName, int levelIndex) where T : DialogueGraphBaseNode
        {
            return new SearchTreeEntry(new GUIContent(nodeName)) {level = levelIndex, userData = typeof(T)};
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            if (OnSelectEntryCallback == null) return false;
            return OnSelectEntryCallback(SearchTreeEntry, context);
        }
    }
}