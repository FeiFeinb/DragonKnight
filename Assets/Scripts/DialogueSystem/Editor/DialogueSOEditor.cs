using System;
using UnityEditor;

namespace  RPG.DialogueSystem
{
    [CustomEditor(typeof(DialogueSO))]
    public class DialogueSOEditor : Editor
    {
        private DialogueSO _selectSO;

        private bool _showDictionary = true;
        private string _statusStr = "节点字典";

        private void OnEnable()
        {
            _selectSO = target as DialogueSO;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // 开启可折叠区域
            _showDictionary = EditorGUILayout.BeginFoldoutHeaderGroup(_showDictionary, _statusStr);
            // 若打开折叠
            if (_showDictionary)
            {
                HorizontalLabel("Key", "Value");
                // 遍历字典 显示Key与Value
                foreach (var nodePair in _selectSO.NodeDic)
                {
                    HorizontalLabel(nodePair.Key, nodePair.Value.Content);
                }
            }
            // 结束可折叠区域
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
