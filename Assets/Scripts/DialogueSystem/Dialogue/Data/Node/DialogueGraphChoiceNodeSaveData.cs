using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem
{
    [System.Serializable]
    public class DialogueGraphChoiceNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public string Content;              // 回应内容
        public AudioClip TalkAudioClip;     // 回应音频
        
        public DialogueGraphChoiceNodeSaveData(string uniqueID, Rect rectPos, List<DialogueGraphPortSaveData> inputPortsData, List<DialogueGraphPortSaveData> outputPortsData) : base(uniqueID, rectPos, inputPortsData, outputPortsData)
        {
        }
    }
}