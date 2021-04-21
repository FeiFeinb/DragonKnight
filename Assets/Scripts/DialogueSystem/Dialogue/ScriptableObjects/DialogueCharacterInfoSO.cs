using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.DialogueSystem
{

    [CreateAssetMenu(fileName = "DialogueNPCInfo", menuName = "Dialogue System/DialogueNPCInfo")]
    public class DialogueCharacterInfoSO : ScriptableObject
    {
        [DisplayOnly] public int id;
        public string CharacterName => characterName;
        public Sprite HeadSculpture => headSculpture;
        [SerializeField] private string characterName;    // 名字
        [SerializeField] private Sprite headSculpture;    // 头像
    }
}

