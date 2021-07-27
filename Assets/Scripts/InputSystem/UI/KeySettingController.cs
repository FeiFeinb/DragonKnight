using System;
using System.Collections.Generic;
using System.Linq;
using RPG.InputSystyem;
using RPG.UI;
using UnityEngine;

namespace RPG.InputSystyem
{
    public class KeySettingController : BaseUIController
    {
        [SerializeField] private KeySettingView _keySettingView;

        [SerializeField] private KeyListenController _keyListenController;
        public void OnEnable()
        {
            // 开启时重新加载UI
            LoadMainKeyChangeFromInputData();
        }

        public override void PreInit()
        {
            base.PreInit();
            _keySettingView.keySettingPairViews.ForEach(keyListenController => keyListenController.Init(this));
            _keyListenController.PreInit();
        }

        /// <summary>
        /// 修改键位设置
        /// </summary>
        /// <param name="pairView"></param>
        public void SetMainKeyChange(KeySettingPairView pairView)
        {
            _keyListenController.StartListen(pairView);
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

        /// <summary>
        /// 捕获容器中的KeySettingPairView并添加至列表
        /// </summary>
        [ContextMenu(nameof(FindAndFillKeySettingPairViewList))]
        private void FindAndFillKeySettingPairViewList()
        {
            _keySettingView.keySettingPairViews = _keySettingView.container.GetComponentsInChildren<KeySettingPairView>().ToList();
        }

        /// <summary>
        /// 将UI存储的按键数据写入到Json中
        /// </summary>
        [ContextMenu(nameof(WriteMainKeyChangeToDefaultJson))]
        private void WriteMainKeyChangeToDefaultJson()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            // 将各个UI的信息组成一个Dic  string-string然后赋值给Json
            _keySettingView.keySettingPairViews.ForEach(view =>
            {
                var pair = view.GetUIData();
                data.Add(pair.Item1, pair.Item2);
            });
            InputManager.Instance.WriteDefaultJsonFromInputData(data);
        }
    }
}