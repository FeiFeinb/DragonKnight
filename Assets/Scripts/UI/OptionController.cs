using DG.Tweening;
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
        
        
        public KeySettingView _keySettingView;

        public override void PreInit()
        {
            base.PreInit();
            _keySettingView.Init();
        }

        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            inSequence.Append(rect.DOAnchorPosY(-rect.anchoredPosition.y, 0.4f).SetEase(Ease.OutBack));
            return true;
        }

        public void OnReturnBack()
        {
            GlobalUIManager.Instance.CloseUI(this);
        }
        
        // TODO: 使用LitJson完成读写
        public void LoadDefaultConfigure() {}
        public void ApplyConfigure() {}
        private void LoadUserConfigure() {}
    }
}
