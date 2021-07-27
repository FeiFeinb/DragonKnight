using DG.Tweening;
using RPG.Audio;
using RPG.InputSystyem;
using RPG.Module;
using RPG.UI;
using UnityEngine;

namespace RPG.UI
{
    public class OptionController : BaseUIController
    {
        public static string storePath = "UIView/OptionView";
        public static OptionController controller;

        [SerializeField] private OptionView _optionView;
        
        [SerializeField] private AudioSettingController _audioSettingController;
        [SerializeField] private KeySettingController _keySettingController;
        [SerializeField] private MakerController _makerController;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectColor;
        private BaseUI _currentSettingView;
        
        public override void PreInit()
        {
            base.PreInit();
            _audioSettingController.PreInit();
            _keySettingController.PreInit();
            _makerController.PreInit();

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
            inSequence.Append(rect.DOAnchorPosY(-rect.anchoredPosition.y, 0.4f).SetEase(Ease.OutBack));
            return true;
        }

        private void SwitchToAudioSettingView()
        {
            if (_currentSettingView)
                _currentSettingView.Hide();
            _currentSettingView = _audioSettingController;
            
            _optionView.audioSettingViewButton.image.color = _selectColor;
            _optionView.keySettingViewButton.image.color = _normalColor;
            _optionView.makerViewButton.image.color = _normalColor;
            
            _optionView.defaultButton.gameObject.SetActive(false);
            _optionView.applyButton.gameObject.SetActive(false);
            _currentSettingView.Show();
        }
        
        private void SwitchToKeySettingView()
        {
            if (_currentSettingView)
                _currentSettingView.Hide();
            _currentSettingView = _keySettingController;
            
            _optionView.audioSettingViewButton.image.color = _normalColor;
            _optionView.keySettingViewButton.image.color = _selectColor;
            _optionView.makerViewButton.image.color = _normalColor;
            
            _optionView.defaultButton.gameObject.SetActive(true);
            _optionView.applyButton.gameObject.SetActive(true);
            _currentSettingView.Show();
        }
        
        private void SwitchToMakerView()
        {
            if (_currentSettingView)
                _currentSettingView.Hide();
            _currentSettingView = _makerController;
            
            _optionView.audioSettingViewButton.image.color = _normalColor;
            _optionView.keySettingViewButton.image.color = _normalColor;
            _optionView.makerViewButton.image.color = _selectColor;
            
            _optionView.defaultButton.gameObject.SetActive(false);
            _optionView.applyButton.gameObject.SetActive(false);
            _currentSettingView.Show();
        }

        
        /// <summary>
        /// 返回按钮
        /// </summary>
        public void ReturnBack()
        {
            GlobalUIManager.Instance.CloseUI(this);
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
            _keySettingController.LoadMainKeyChangeFromInputData();
        }

        /// <summary>
        /// 应用设置按钮
        /// </summary>
        public void ApplyConfigure()
        {
            // 从UI写入到InputData
            _keySettingController.WriteMainKeyChangeToInputData();
            // 保存到Json
            InputManager.Instance.WritePlayerJsonFromInputData();
        }
    }
}
