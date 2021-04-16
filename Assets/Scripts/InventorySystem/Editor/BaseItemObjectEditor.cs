// using System.Runtime.InteropServices.WindowsRuntime;
// using UnityEngine;
// using UnityEditor;
// [CustomEditor(typeof(EquipmentItemObject))]
// public class BaseItemObjectEditor : Editor  
// {
//     private SerializedProperty baseItemField;
//     private SerializedProperty equipmentField;
//     private SerializedProperty bodyField;
//     void OnEnable()
//     {
//         baseItemField = serializedObject.FindProperty("itemType.baseItemType");
//         equipmentField = serializedObject.FindProperty("itemType.equipmentType");
//         bodyField = serializedObject.FindProperty("itemType.bodyType");
//     }
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         serializedObject.Update();
//         EditorGUILayout.PropertyField(baseItemField);
//         若为装备类型 则显示equipmentType与bodyType
//         if (baseItemField.enumValueIndex == (int)BaseItemType.Equipment)
//         {
//             EditorGUILayout.PropertyField(equipmentField);
//             EditorGUILayout.PropertyField(bodyField);
//             若该物品能够装备显示
//             serializedObject.ApplyModifiedProperties();
//         }
//     }
// }
