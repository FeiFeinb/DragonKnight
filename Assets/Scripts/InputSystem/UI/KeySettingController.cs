using System;
using System.Collections.Generic;
using System.Linq;
using RPG.InputSystyem;
using RPG.UI;
using RPG.Utility;
using UnityEngine;

namespace RPG.InputSystyem
{
    public class KeySettingController : BaseUI
    {
        [SerializeField] private KeySettingView _keySettingView;

        [SerializeField] private KeyListenController _keyListenController;

        public void OnEnable()
        {
            // 开启时重新加载UI
            LoadMainKeyChangeFromInputData();
        }

        protected override void InitInstance()
        {
            base.InitInstance();
            _keySettingView.keySettingPairViews.ForEach(keyListenController => keyListenController.PreInit());
            _keyListenController.Init();
        }

        /// <summary>
        /// 修改键位设置
        /// </summary>
        /// <param name="pairView">修改的键对应的KeySettingPairView</param>
        /// <param name="modifiedCallBack">修改完成回调</param>
        public void ModifyKey(Action<KeyCode> modifiedCallBack)
        {
            _keyListenController.StartListen(modifiedCallBack);
        }

        /// <summary>
        /// 从UI中读取暂存的键位设置 保存到InputData
        /// </summary>
        public void WriteMainKeyChangeToInputData()
        {
            foreach (var view in _keySettingView.keySettingPairViews)
            {
                var tuple = view.GetKeyInfo();
                switch (tuple.keyTypeStr.ToKeyType())
                {
                    case KeyType.NormalKey:
                        InputManager.Instance.inputData.SetOrAddNormalKey(view.keyActionType,
                            tuple.firstKeyCodeStr.ToKeyCode());
                        break;
                    case KeyType.AxisKey:
                        InputManager.Instance.inputData.SetOrAddAxisKey(view.keyActionType,
                            tuple.firstKeyCodeStr.ToKeyCode(), tuple.secondKeyCodeStr.ToKeyCode());
                        break;
                }
            }
        }

        /// <summary>
        /// 从InputData中读取数据应用到UI
        /// </summary>
        /// <exception cref="Exception">当Json保存后去修改UI中记录键位的Title时 会出现数据不一致的冲突</exception>
        public void LoadMainKeyChangeFromInputData()
        {
            var data = InputManager.Instance.inputData.GetKeyData();
            foreach (var view in _keySettingView.keySettingPairViews)
            {
                // TODO 根据键位生成UI
                // NormalKey
                // AxisKey
                string actionStr = view.keyActionType.ToString();
                if (!data.ContainsKey(actionStr))
                {
                    Debug.LogError($"无法在Json文件中找到对应的{actionStr}");
                    continue;
                }

                var tuple = data[actionStr];
                view.SetKeyText(tuple.keyTypeStr.ToKeyType(), tuple.firstKeyCodeStr, tuple.secondKeyCodeStr);
            }
        }

        /// <summary>
        /// 捕获容器中的KeySettingPairView并添加至列表
        /// </summary>
        [ContextMenu(nameof(FindAndFillKeySettingPairViewList))]
        private void FindAndFillKeySettingPairViewList()
        {
            _keySettingView.keySettingPairViews =
                _keySettingView.container.GetComponentsInChildren<KeySettingPairView>().ToList();
        }

        /// <summary>
        /// 将UI存储的按键数据反写入到DefaultJson中 在Editor中调用
        /// </summary>
        [ContextMenu(nameof(WriteMainKeyChangeToDefaultJson))]
        private void WriteMainKeyChangeToDefaultJson()
        {
            Dictionary<string, InputStrTuple> data = new Dictionary<string, InputStrTuple>();

            // 将各个UI的信息组成一个Dic 然后赋值给Json
            _keySettingView.keySettingPairViews.ForEach(view =>
            {
                var tuple = view.GetKeyInfo();
                data.Add(view.keyActionType.ToString(),
                    new InputStrTuple(tuple.keyTypeStr, tuple.firstKeyCodeStr, tuple.secondKeyCodeStr));
            });
            InputManager.Instance.WriteDefaultJsonFromInputData(data);
        }
    }
}