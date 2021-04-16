using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// Inspector窗口只读变量Attribute
/// </summary>
public class DisplayOnlyAttribute : PropertyAttribute
{

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DisplayOnlyAttribute))]
public class DisplayOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif
