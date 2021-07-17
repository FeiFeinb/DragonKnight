using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.Inertact
{
    public class InteractButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _content;
        [SerializeField] private Button _button;

        public void Init(string content)
        {
            // TODO: 将Image赋值更改为Enum赋值 由此类去查找对应的Image自动赋值
            // _icon = icon;
            _content.text = content;
        }

        public void AddOnClickListener(Action onCLickCallBack)
        {
            _button.onClick.AddListener(onCLickCallBack.Invoke);
        }
    }
}