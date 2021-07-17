using System.Collections.Generic;
using RPG.Inertact;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphChoiceNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public string Content;              // 回应内容
        public AudioClip TalkAudioClip;     // 回应音频
        
        public DialogueGraphChoiceNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
        }

        public override bool HandleData(DialogueTreeNode treeNode, GameObject obj)
        {
            // 向交互UI面板添加交互选项
            InteractionController.controller.AddButton(Content, () => PlayerDialogueManager.Instance.ContinueDialogue(UniqueID));
            return false;
        }
        
    }
}