using DG.Tweening;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.InputSystyem
{
    public class KeyListenController : BaseUIController
    {
        [SerializeField] private Text _previewKeyText;
        
        private bool _isStartListenKey = false;
        private KeySettingPairView _modifyPairView;
        public void StartListen(KeySettingPairView pairView)
        {
            InputManager.Instance.inputData.CloseAllKeyInput(1);
            _modifyPairView = pairView;
            _previewKeyText.text = string.Empty;
            OptionController.controller.SetAccessible(true);
            Show();
        }

        private void EndListener()
        {
            _isStartListenKey = false;
            _modifyPairView = null;
            OptionController.controller.SetAccessible(false);
            
            Hide();
        }
        
        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            inSequence.Append(rect.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack));
            inSequence.onComplete += delegate { _isStartListenKey = true; };
            inSequence.onRewind += delegate { InputManager.Instance.inputData.OpenAllKeyInput(1); };
            return true;
        }

        private void OnGUI()
        {
            if (_isStartListenKey)
            {
                var newUpdatedCode = Event.current.keyCode;
                if (newUpdatedCode != KeyCode.None && _modifyPairView)
                {
                    // TODO: 实现OffKey的设置
                    // 更改UI
                    _previewKeyText.text = newUpdatedCode.ToString();
                    _modifyPairView.SetMainKeyText(newUpdatedCode.ToString());

                    EndListener();
                }
            }
        }
    }
}