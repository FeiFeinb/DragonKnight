using UnityEditor;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphTalkNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public string _content;                               // 对话内容
        public AudioClip _audioClip;                          // 对话音频
        public InterlocutorType _interlocutorType;            // 对话方
        public DialogueCharacterInfoSO _characterInfoSO;      // 对话角色信息

        public DialogueGraphTalkNodeSaveData(string guid, string title, Rect rectPos) : base(guid, title, rectPos)
        {
        }
    }
}