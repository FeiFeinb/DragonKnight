using UnityEngine;
using RPG.DialogueSystem;
using RPG.InventorySystem;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class DialogueQuestObjective : QuestObjective
    {
        public DialogueNodeSO DialogueNode => dialogueNode;
        public string EventID => eventID;
        public DialogueCharacterInfoSO CharacterInfo => characterInfo;
        [Tooltip("对话角色信息"), SerializeField] private DialogueCharacterInfoSO characterInfo;
        [Tooltip("触发完成任务的对话结点"), SerializeField] private DialogueNodeSO dialogueNode;
        [Tooltip("事件ID"), SerializeField] private string eventID;
    }
}