using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 对话节点数据类
    /// </summary>
    [System.Serializable]
    public class DialogueGraphTalkNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        [TextArea(1, 10)]
        public string Content;                              // 对话内容
        public AudioClip TalkAudioClip;                     // 对话音频
        public InterlocutorType Interlocutor;               // 对话方
        public DialogueCharacterInfoSO CharacterInfoSO;     // 对话角色信息

        public DialogueGraphTalkNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
        }

        public override bool HandleData(DialogueTreeNode treeNode, GameObject obj)
        {
            // 设置UI显示
            DialogueController.controller.SetDialogueDisplay(CharacterInfoSO, Content, UniqueID);
            return false;
        }
    }
}