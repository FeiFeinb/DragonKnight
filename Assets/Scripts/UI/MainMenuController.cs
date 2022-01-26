using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MainMenuController : BaseUI
    {
        [SerializeField] private MainMenuView _mainMenuView;

        protected override void InitInstance()
        {
            // 初始化各个按钮的功能
            _mainMenuView.PreInit();
        }
        
        protected override bool InitControlObj()
        {
            _controlUITrans = _mainMenuView.buttonContainer.transform;
            _controlUIGameObj = _mainMenuView.buttonContainer.gameObject;
            return true;
        }

        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = _mainMenuView.buttonContainer;
            _inSequence.Append(rect.DOAnchorPosY(-rect.anchoredPosition.y, 0.2f).SetEase(Ease.Linear));
            return true;
        }
        
    }
    
}
