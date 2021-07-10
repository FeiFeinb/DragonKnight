using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RPG.DialogueSystem
{
    /// <summary>
    /// 可对话NPC人物信息
    /// </summary>
    [CreateAssetMenu(fileName = "DialogueNPCInfo", menuName = "Dialogue System/DialogueNPCInfo")]
    public class DialogueCharacterInfoSO : ScriptableObject
    {
        [DisplayOnly] public int ID;                            // NPC ID
        public string CharacterName => _characterName;          // 外部获取
        public Sprite HeadSculpture => _headSculpture;          // 外部获取

        [SerializeField] private string _characterName;         // 名字

        [SerializeField] private Sprite _headSculpture;         // 头像
    }
}