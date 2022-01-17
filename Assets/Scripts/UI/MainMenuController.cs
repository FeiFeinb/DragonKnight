using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MainMenuController : BaseUIController
    {
        public static string storePath = "UIView/MainMenuController";
        public static MainMenuController controller;

        [SerializeField] private MainMenuView _mainMenuView;

        public override void PreInit()
        {
            base.PreInit();
            // 初始化各个按钮的功能
        }
    }
    
}
