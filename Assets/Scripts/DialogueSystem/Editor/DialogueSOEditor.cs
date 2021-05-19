using System;
using UnityEditor;
using UnityEngine;

namespace RPG.DialogueSystem
{
    [CustomEditor(typeof(DialogueSO))]
    public class DialogueSOEditor : Editor
    {
        private DialogueSO selectSO;
        
        private bool showDictionary = true;
        private string statusStr = "节点字典";
        
        private void OnEnable()
        {
            selectSO = target as DialogueSO;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            showDictionary = EditorGUILayout.BeginFoldoutHeaderGroup(showDictionary, statusStr);
            if (showDictionary)
            {
                HorizontalLabel("Key", "Value");
                foreach (var nodePair in selectSO.NodeDic)
                {
                    HorizontalLabel(nodePair.Key, nodePair.Value.Content);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void HorizontalLabel(string leftStr, string rightStr)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(leftStr);
            EditorGUILayout.LabelField(rightStr);
            EditorGUILayout.EndHorizontal();
        }
    }
}