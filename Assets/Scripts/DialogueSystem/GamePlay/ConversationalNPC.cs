using System;
using UnityEngine;
using RPG.DialogueSystem.Graph;
namespace RPG.DialogueSystem.Graph
{
    public class ConversationalNPC : MonoBehaviour
    {
        public DialogueCharacterInfoSO npcInfo;          // 对话NPC信息
        public DialogueGraphSO dialogueSO;               // 对话内容SO

        public void StartDialogue()
        {
            PlayerDialogueManager.Instance.StartDialogue(this);
        }

        
        public void ResetDialogue()
        {
            
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                StartDialogue();
        }
    }
}