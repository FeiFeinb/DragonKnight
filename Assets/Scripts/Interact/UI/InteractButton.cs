using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.Interact
{
    public class InteractButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _content;
        [SerializeField] private Button _button;

        public void SetInformation(Sprite sprite, string content)
        {
            if (sprite) _icon.sprite = sprite;
            _content.text = content;
        }

        public void AddOnClickListener(Action onCLickCallBack)
        {
            _button.onClick.AddListener(onCLickCallBack.Invoke);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}