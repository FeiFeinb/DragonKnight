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
        /// 返回按钮
        /// </summary>
        public void ReturnBack()
        {
            UIStackManager.Instance.PopUI();
        }
        
        // TODO: 添加对音效配置的加载
        /// <summary>
        /// 加载默认配置按钮
        /// </summary>
        public void LoadDefaultConfigure()
        {
            // 从Json中加载
            InputManager.Instance.LoadDefaultJsonToInputData();
            // 从InputData中读取数据应用到UI
            keySettingController.LoadMainKeyChangeFromInputData();
        }

        /// <summary>
        /// 应用设置按钮
        /// </summary>
        public void ApplyConfigure()
        {
            // 从UI写入到InputData
            keySettingController.WriteMainKeyChangeToInputData();
            // 从InputData中写入到Json
            InputManager.Instance.WritePlayerJsonFromInputData();
        }
    }
}
