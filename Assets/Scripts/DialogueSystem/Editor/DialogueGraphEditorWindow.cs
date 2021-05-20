using System;
using System.Collections;
using System.Collections.Generic;
using RPG.DialogueSystem;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
public class DialogueGraphEditorWindow : EditorWindow
{
    [MenuItem("Window/DialogueGraph")]
    private static void ShowDialogueGraphWindow()
    {
        GetWindow<DialogueGraphEditorWindow>(false, "DialogueGraph");
    }

    private void OnEnable()
    {
        this.rootVisualElement.Add(new DialogueGraphView()
        {
            style = { flexGrow = 1 }
        });
    }
}
