using System.Collections;
using System.Collections.Generic;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueController: BaseUI
    {
        public static string storePath = "UIView/DialogueView";

        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private Button _continueButton;

        private string _nodeUniqueID;
        

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