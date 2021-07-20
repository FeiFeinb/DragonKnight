using UnityEngine;
using RPG.DialogueSystem.Graph;
using RPG.InventorySystem;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class DialogueQuestObjective : QuestObjective, ISerializationCallbackReceiver
    {
        // public DialogueNodeSO DialogueNode => dialogueNode;
        // public DialogueCharacterInfoSO CharacterInfo => characterInfo;
        // [Tooltip("对话角色信息"), SerializeField] private DialogueCharacterInfoSO characterInfo;
        // [Tooltip("触发完成任务的对话结点"), SerializeField] private DialogueNodeSO dialogueNode;
        public DialogueGraphSO DialogueSO => _dialogueSO;
        [Tooltip("对话SO"), SerializeField] private DialogueGraphSO _dialogueSO;
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            targetAmount = 1;
        }
    }
}