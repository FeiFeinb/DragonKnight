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
            BaseUI.GetController<OptionController>().SetAccessible(false);

            Hide();
        }

        private IEnumerator ListenKey(Action<KeyCode> modifiedCallBack)
        {
            KeyCode currentKeyCode = InputManager.Instance.GetCurrentKeyDown();
            while (currentKeyCode == KeyCode.None)
            {
                yield return null;
                currentKeyCode = InputManager.Instance.GetCurrentKeyDown();
            }
            
            _previewKeyText.text = currentKeyCode.ToString();
            modifiedCallBack(currentKeyCode);
            EndListener();

        }

        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            _inSequence.Append(rect.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack));
            _inSequence.onRewind += delegate
            {
                InputManager.Instance.inputData.OpenAllKeyInput(1);
                InputManager.Instance.OpenMouseInput();
            };
            return true;
        }
    }
}