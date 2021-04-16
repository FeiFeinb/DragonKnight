using UnityEngine;
using RPG.DialogueSystem;
using RPG.InventorySystem;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class DialogueQuestObjective : QuestObjective
    {
        public string DialogueUniqueID => dialogueUniqueID;
        public string EventID => eventID;
        public DialogueCharacterInfo CharacterInfo => characterInfo;
        [SerializeField] private string dialogueUniqueID;               // 触发完成任务的对话结点ID
        [SerializeField] private string eventID;                        // 事件ID
        [SerializeField] private DialogueCharacterInfo characterInfo;   // 对话角色信息
    }
}