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
            entries.Add(new SearchTreeEntry(new GUIContent("Create Nodes")));
            entries.Add(new SearchTreeEntry(new GUIContent("Basic Nodes")) { level = 1});
            entries.Add(new SearchTreeEntry(new GUIContent("Start Node")) {level = 2, userData = nameof(DialogueGraphStartNode)});
            entries.Add(new SearchTreeEntry(new GUIContent("End Node")) {level = 2, userData = nameof(DialogueGraphEndNode)});
            return entries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            if (OnSelectEntryCallback == null) return false;
            return OnSelectEntryCallback(SearchTreeEntry, context);
        }
    }
}