using System.Collections.Generic;
using DG.Tweening;
using RPG.InputSystyem;
using RPG.Module;
using RPG.SaveSystem;
using RPG.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PauseController : BaseUI
    {
        public static string storePath = "UIView/PauseView";

        [SerializeField] private List<Button> _buttons = new List<Button>();

        [SerializeField] private RectTransform _rect;


        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OpenSaveAndLoadView()
        {
            UIStackManager.Instance.PushUI(BaseUI.GetController<SavingController>());
        }

        public void OpenOptionView()
        {
            UIStackManager.Instance.PushUI(BaseUI.GetController<OptionController>());
        }
        
        public void ContinueGame()
        {
            UIStackManager.Instance.PopUI();
        }

        public override void OnUIPushIntoStack()
        {
            base.OnUIPushIntoStack();
            // 暂停所有键位输入
            InputManager.Instance.inputData.CloseAllKeyInput(0);
            InputManager.Instance.CloseMouseInput();
        }

        public override void OnUIPopFromStack()
        {
            base.OnUIPopFromStack();
            InputManager.Instance.inputData.CloseAllKeyInput(0);
            InputManager.Instance.OpenMouseInput();
        }

        protected override bool AchieveDoTweenSequence()
        {
            _inSequence.Append(_rect.DOAnchorPosX(-_rect.anchoredPosition.x, 0.3f));
            for (int i = 0; i < _buttons.Count; i++)
            {
                RectTransform buttonRect = _buttons[i].transform as RectTransform;
                _inSequence.Insert(0.04f * (i + 1), buttonRect.DOAnchorPosX(-buttonRect.anchoredPosition.x, 0.3f));
            }

            // 在UI动画播放过程中不允许UI交互
            _inSequence.OnUpdate(() => { _buttons.ForEach(button => button.interactable = false); });

            // 正向显示UI回调
            _inSequence.onComplete += () => { _buttons.ForEach(button => button.interactable = true); };
            return true;
        }
    }
}