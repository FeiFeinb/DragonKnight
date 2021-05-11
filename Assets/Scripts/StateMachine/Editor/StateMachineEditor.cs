using System;
using UnityEngine;
using UnityEditor;

namespace RPG.StateMachine
{
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineEditor : Editor
    {
        private StateMachine selectMachine;
        
        private void OnEnable()
        {
            selectMachine = target as StateMachine;
            if (selectMachine == null)
            {
                Debug.LogError("Editor Error: Can not translate selectMachine");
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Current StateName:");
            EditorGUILayout.LabelField(selectMachine.CurrentState.StateName);
            EditorGUILayout.EndHorizontal();
        }
    }
}