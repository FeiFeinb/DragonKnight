using System;
using System.Collections.Generic;
using RPG.InputSystyem;
using RPG.UI;
using UnityEngine;

namespace UI
{
    public class KeySettingView : BaseUI
    {
        [SerializeField] private Transform _content;
        [SerializeField] private KeyListenController keyListenController;
        
        public KeySettingPairView openInventoryKeyPair;

        public void Init()
        {
            // 读取Json 初始化按键
            InputManager.Instance.inputData.AddNormalKey(openInventoryKeyPair.title.text, KeyCode.B);
            
            openInventoryKeyPair.Init(this);
            
            keyListenController.PreInit();
        }
        
        public void SetMainKeyChange(KeySettingPairView pairView)
        {
            // TODO: 开始之后不允许用户点击其他地方
            keyListenController.StartListen(pairView);
        }
        
    }
}