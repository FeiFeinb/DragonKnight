using System;
using UnityEngine;
using RPG.DialogueSystem.Graph;
using RPG.Interact;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueNPC : MonoBehaviour, IInteractable
    {
        public DialogueCharacterInfoSO npcInfo;          // 对话NPC信息
        public DialogueGraphSO dialogueSO;               // 对话内容SO

        public void GetInteractInfo(out InteractType type, out string buttonStr, out Sprite sprite)
        {
            type = InteractType.StartDialogue;
            buttonStr = npcInfo.CharacterName;
            sprite = npcInfo.HeadSculpture;
        }

        public void Interact()
        {
            PlayerDialogueManager.Instance.StartDialogue(this);
        }
    }
}