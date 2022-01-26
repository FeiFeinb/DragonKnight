using System;
using System.Collections;
using DG.Tweening;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.InputSystyem
{
    public class KeyListenController : BaseUI
    {
        [SerializeField] private Text _previewKeyText;

        private bool _isStartListenKey = false;

        public void StartListen(Action<KeyCode> modifiedCallBack)
        {
            InputManager.Instance.inputData.CloseAllKeyInput(1);
            InputManager.Instance.CloseMouseInput();
            _previewKeyText.text = string.Empty;
            BaseUI.GetController<OptionController>().SetAccessible(true);
            Show();
            StartCoroutine(ListenKey(modifiedCallBack));
        }

        private void EndListener()
        {
            _isStartListenKey = false;
            BaseUI.GetController<OptionController>().SetAccessible(false);

            Hide();
        }

        private IEnumerator ListenKey(Action<KeyCode> modifiedCallBack)
        {
            while (!Input.anyKeyDown)
            {
                yield return null;
            }
            
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    _previewKeyText.text = keyCode.ToString();
                    modifiedCallBack(keyCode);
                    EndListener();
                }
            }

        }

        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            _inSequence.Append(rect.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack));
            _inSequence.onComplete += delegate { _isStartListenKey = true; };
            _inSequence.onRewind += delegate
            {
                InputManager.Instance.inputData.OpenAllKeyInput(1);
                InputManager.Instance.OpenMouseInput();
            };
            return true;
        }
    }
}