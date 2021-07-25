using System;
using System.Collections.Generic;
using RPG.InputSystyem;
using RPG.UI;
using UnityEngine;

namespace UI
{
    public class KeySettingController : BaseUIController
    {
        [SerializeField] private KeySettingView _keySettingView;
        
        [SerializeField] private KeyListenController keyListenController;

        public void OnEnable()
        {
            // 开启时重新加载UI
            LoadMainKeyChangeFromInputData();
        }

        public override void PreInit()
        {
            base.PreInit();
            _keySettingView.keySettingPairViews.ForEach(keyListenController => keyListenController.Init(this));
            keyListenController.PreInit();
        }

        /// <summary>
        /// 修改键位设置
        /// </summary>
        /// <param name="pairView"></param>
        public void SetMainKeyChange(KeySettingPairView pairView)
        {
            keyListenController.StartListen(pairView);
        }

        /// <summary>
        /// 从UI中读取暂存的键位设置 保存到InputData
        /// </summary>
        public void WriteMainKeyChangeToInputData()
        {
            _keySettingView.keySettingPairViews.ForEach(view =>
            {
                view.WriteUIDataToInputData();
            });
        }
        
        /// <summary>
        /// 从InputData中读取数据应用到UI
        /// </summary>
        /// <exception cref="Exception">当Json保存后去修改UI中记录键位的Title时 会出现数据不一致的冲突</exception>
        public void LoadMainKeyChangeFromInputData()
        {
            Dictionary<string, string> data = InputManager.Instance.inputData.GetNormalKeyData(); 
            _keySettingView.keySettingPairViews.ForEach(view =>
            {
                string title = view.title.text;
                if (!data.ContainsKey(title))
                    throw new Exception("UI中记录的键位Title与Json中记录的不一致");
                view.SetMainKeyText(data[title]);
            });
        }
    }
}