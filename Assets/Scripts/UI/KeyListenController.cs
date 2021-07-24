using System;
using System.Collections;
using DG.Tweening;
using RPG.InputSystyem;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class KeyListenController : BaseUIController
    {
        [SerializeField] private Text _previewKeyText;
        private bool _isStartListenKey = false;
        private KeySettingPairView _modifyPairView;
        public void StartListen(KeySettingPairView pairView)
        {
            _modifyPairView = pairView;
            _previewKeyText.text = string.Empty;
            Show();
        }
        
        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            inSequence.Append(rect.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack));
            inSequence.onComplete += delegate { _isStartListenKey = true; };
            return true;
        }

        private void OnGUI()
        {
            if (_isStartListenKey)
            {
                var code = Event.current.keyCode;
                if (code != KeyCode.None && _modifyPairView)
                {
                    // TODO: 实现OffKey的设置
                    InputManager.Instance.inputData.SetNormalKey(_modifyPairView.title.text, code);
                    
                    _previewKeyText.text = code.ToString();
                    _modifyPairView.SetMainKeyText(code.ToString());
                    
                    _isStartListenKey = false;
                    _modifyPairView = null;
                    Hide();
                }
            }
        }
    }
}