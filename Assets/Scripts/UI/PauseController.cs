using System.Collections.Generic;
using DG.Tweening;
using RPG.Module;
using RPG.SaveSystem;
using RPG.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PauseController : BaseUIController
    {
        public static string storePath = "UIView/PauseView";
        public static PauseController controller;

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
            GlobalUIManager.Instance.OpenUI(SavingController.controller);
        }

        public void OpenOptionView()
        {
            GlobalUIManager.Instance.OpenUI(OptionController.controller);
        }
        
        public void ContinueGame()
        {
            GlobalUIManager.Instance.CloseUI(this);
        }
        
        
        
        protected override bool AchieveDoTweenSequence()
        {
            inSequence.Append(_rect.DOAnchorPosX(-_rect.anchoredPosition.x, 0.3f));
            for (int i = 0; i < _buttons.Count; i++)
            {
                RectTransform buttonRect = _buttons[i].transform as RectTransform;
                inSequence.Insert(0.04f * (i + 1), buttonRect.DOAnchorPosX(-buttonRect.anchoredPosition.x, 0.3f));
            }

            inSequence.OnUpdate(() => { _buttons.ForEach(button => button.interactable = false); });

            // 正向显示UI回调
            inSequence.onComplete += () => { _buttons.ForEach(button => button.interactable = true); };
            return true;
        }
    }
}