using DG.Tweening;
using RPG.Audio;
using RPG.InputSystyem;
using RPG.Module;
using RPG.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.UI
{
    public class OptionController : BaseUI
    {
        public static string storePath = "UIView/OptionView";

        [SerializeField] private OptionView _optionView;
        
        public AudioSettingController audioSettingController;
        public KeySettingController keySettingController;
        public MakerController makerController;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectColor;
        
        private BaseUI _currentSettingController;

        protected override void InitInstance()
        {
            base.InitInstance();
            audioSettingController.Init();
            keySettingController.Init();
            makerController.Init();

            _optionView.audioSettingViewButton.onClick.AddListener(SwitchToAudioSettingView);
            _optionView.keySettingViewButton.onClick.AddListener(SwitchToKeySettingView);
            _optionView.makerViewButton.onClick.AddListener(SwitchToMakerView);

            SwitchToAudioSettingView();
        }

        public void SetAccessible(bool isShelter)
        {
            _optionView.shelterImage.gameObject.SetActive(isShelter);
        }
        
        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            _inSequence.Append(rect.DOAnchorPosY(-rect.anchoredPosition.y, 0.4f).SetEase(Ease.OutBack));
            return true;
        }

        private void SwitchToAudioSettingView()
        {
            if (_currentSettingController)
                _currentSettingController.Hide();
            _currentSettingController = audioSettingController;
            
            _optionView.audioSettingViewButton.image.color = _selectColor;
            _optionView.keySettingViewButton.image.color = _normalColor;
            _optionView.makerViewButton.image.color = _normalColor;
            
            _optionView.defaultButton.gameObject.SetActive(false);
            _optionView.applyButton.gameObject.SetActive(false);
            _currentSettingController.Show();
        }
        
        private void SwitchToKeySettingView()
        {
            if (_currentSettingController)
                _currentSettingController.Hide();
            _currentSettingController = keySettingController;
            
            _optionView.audioSettingViewButton.image.color = _normalColor;
            _optionView.keySettingViewButton.image.color = _selectColor;
            _optionView.makerViewButton.image.color = _normalColor;
            
            _optionView.defaultButton.gameObject.SetActive(true);
            _optionView.applyButton.gameObject.SetActive(true);
            _currentSettingController.Show();
        }
        
        private void SwitchToMakerView()
        {
            if (_currentSettingController)
                _currentSettingController.Hide();
            _currentSettingController = makerController;
            
            _optionView.audioSettingViewButton.image.color = _normalColor;
            _optionView.keySettingViewButton.image.color = _normalColor;
            _optionView.makerViewButton.image.color = _selectColor;
            
            _optionView.defaultButton.gameObject.SetActive(false);
            _optionView.applyButton.gameObject.SetActive(false);
            _currentSettingController.Show();
        }

        
        /// <summary>
        /// ????????????
        /// </summary>
        public void ReturnBack()
        {
            UIStackManager.Instance.PopUI();
        }
        
        // TODO: ??????????????????????????????
        /// <summary>
        /// ????????????????????????
        /// </summary>
        public void LoadDefaultConfigure()
        {
            // ???Json?????????
            InputManager.Instance.LoadDefaultJsonToInputData();
            // ???InputData????????????????????????UI
            keySettingController.LoadMainKeyChangeFromInputData();
        }

        /// <summary>
        /// ??????????????????
        /// </summary>
        public void ApplyConfigure()
        {
            // ???UI?????????InputData
            keySettingController.WriteMainKeyChangeToInputData();
            // ???InputData????????????Json
            InputManager.Instance.WritePlayerJsonFromInputData();
        }
    }
}
