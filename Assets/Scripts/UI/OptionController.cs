using DG.Tweening;
using RPG.InputSystyem;
using RPG.Module;
using RPG.UI;
using UnityEngine;

namespace UI
{
    public class OptionController : BaseUIController
    {
        public static string storePath = "UIView/OptionView";
        public static OptionController controller;

        [SerializeField] private OptionView _optionView;
        
        [SerializeField] private KeySettingController _keySettingController;

        public override void PreInit()
        {
            base.PreInit();
            _keySettingController.PreInit();
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

        public void ReturnBack()
        {
            GlobalUIManager.Instance.CloseUI(this);
        }
        
        // TODO: 添加对音效的操作
        public void LoadDefaultConfigure()
        {
            // 从Json中加载
            InputManager.Instance.LoadDefaultJsonToInputData();
            // 从InputData中读取数据应用到UI
            _keySettingController.LoadMainKeyChangeFromInputData();
        }

        public void ApplyConfigure()
        {
            // 从UI写入到InputData
            _keySettingController.WriteMainKeyChangeToInputData();
            // 保存到Json
            InputManager.Instance.WritePlayerJsonFromInputData();
        }
    }
}
