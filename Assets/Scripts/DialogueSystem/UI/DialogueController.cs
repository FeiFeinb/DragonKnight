using System.Collections;
using System.Collections.Generic;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueController : BaseUIController
    {
        public static string storePath = "UIView/DialogueView";
        public static DialogueController controller;

        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private Button _continueButton;

        private string _nodeUniqueID;

        public override void PreInit()
        {
        }

        public void SetDialogueDisplay(DialogueCharacterInfoSO characterInfoSO, string content, string nodeUniqueID)
        {
            _dialogueView.SetSpeakerHead(characterInfoSO.HeadSculpture);
            _dialogueView.SetSpeakerName(characterInfoSO.CharacterName);
            _dialogueView.SetContent(content);
            _nodeUniqueID = nodeUniqueID;
        }

        public void ContinueDialogue()
        {
            if (_nodeUniqueID == string.Empty) return;
            string newStr = _nodeUniqueID;
            _nodeUniqueID = string.Empty;
            PlayerDialogueManager.Instance.ContinueDialogue(newStr);
        }
    }
}