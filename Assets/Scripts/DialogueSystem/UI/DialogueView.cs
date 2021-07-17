using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private Image _speakerHead;
        [SerializeField] private Text _name;
        [SerializeField] private Text _content;

        [SerializeField] private GameObject _continueTip;

        public void SetSpeakerHead(Sprite speakerHead)
        {
            _speakerHead.sprite = speakerHead;
        }

        public void SetSpeakerName(string speakerName)
        {
            _name.text = speakerName;
        }

        public void SetContent(string content)
        {
            _content.text = content;
        }
    }
}

