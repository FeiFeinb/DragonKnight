using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Animations;

namespace RPG.InputSystyem
{
    [System.Serializable]
    public class InputData
    {
        /// <summary>
        /// NormalKey数列
        /// </summary>
        private Dictionary<KeyActionType, NormalKey> _normalKeysDic = new Dictionary<KeyActionType, NormalKey>();
        
        /// <summary>
        /// AxisKey数列
        /// </summary>
        private Dictionary<KeyActionType, AxisKey> _axisKeysDic = new Dictionary<KeyActionType, AxisKey>();
        
        /// <summary>
        /// 添加NormalKey
        /// </summary>
        /// <param name="actionType">操作名</param>
        /// <param name="keyCode">键</param>
        public void SetNormalKey(KeyActionType actionType, KeyCode keyCode)
        {
            if (!_normalKeysDic.ContainsKey(actionType))
            {
                _normalKeysDic.Add(actionType, new NormalKey(actionType, keyCode));
            }
            else
            {
                _normalKeysDic[actionType].SetKey(keyCode);
            }
        }

        /// <summary>
        /// 获取NormalKey数据 可将传达给Json进行写操作
        /// </summary>
        /// <returns>(name)-(KeyCodeStr) 数据字典</returns>
        public Dictionary<string, string> GetNormalKeyData()
        {
            Dictionary<string, string> dataDic = new Dictionary<string, string>();
            foreach (KeyValuePair<KeyActionType,NormalKey> keyValuePair in _normalKeysDic)
            {
                dataDic.Add(keyValuePair.Key.ToString(), keyValuePair.Value.mainKeyCode.ToString());
            }

            return dataDic;
        }

        /// <summary>
        /// 将外界传来的数据字典记录到类中
        /// </summary>
        /// <param name="data">(name)-(KeyCodeStr) 数据字典</param>
        public void LoadNormalKeyData(Dictionary<string, string> data)
        {
            foreach (KeyValuePair<string,string> keyValuePair in data)
            {
                SetNormalKey(keyValuePair.Key.ToKeyActionType(), keyValuePair.Value.ToKeyCode());
            }
        }
        
        /// <summary>
        /// 获取NormalKey
        /// </summary>
        /// <param name="actionType">操作名</param>
        /// <returns>NormalKey</returns>
        public NormalKey GetNormalKey(KeyActionType actionType)
        {
            if (!_normalKeysDic.ContainsKey(actionType))
                throw new Exception($"Input系统中无法找到键{actionType}");
            return _normalKeysDic[actionType];
        }
        
        /// <summary>
        /// 获取NormalKey按下状态
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <returns>键状态</returns>
        public bool GetNormalKeyDown(KeyActionType actionType)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            if (normalKey == null) return false;
            return normalKey.isDown;
        }
        
        /// <summary>
        /// 设置NormalKey的启用状态
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <param name="isEnable">是否启用</param>
        public void SetNormalKeyEnable(KeyActionType actionType, bool isEnable)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            if (normalKey == null) return;
            normalKey.SetEnable(isEnable);
        }

        public void AddNormalKeyListener(KeyActionType actionType, Action callBack)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            normalKey.AddTriggerListener(callBack);
        }

        public void RemoveNormalKeyListener(KeyActionType actionType, Action callBack)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            normalKey.RemoveTriggerListener(callBack);
        }
        
        /// <summary>
        /// 每帧更新键
        /// </summary>
        public void UpdateKey()
        {
            foreach (var normalKey in _normalKeysDic)
            {
                normalKey.Value.HandleKey();
            }
        }

        
        /// <summary>
        /// 设置NormalKey权重
        /// </summary>
        public void SetNormalKeyWeights(KeyActionType actionType, int weight)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            normalKey.SetWeight(weight);
        }
        
        /// <summary>
        /// 打开某权重及以下的所有键
        /// </summary>
        /// <param name="weight">权重</param>
        public void OpenAllKeyInput(int weight)
        {
            foreach (var normalKey in _normalKeysDic.Where(normalKey => normalKey.Value.weight <= weight))
            {
                normalKey.Value.SetEnable(true);
            }
        }
        
        /// <summary>
        /// 关闭某权重及以下的所有键盘
        /// </summary>
        public void CloseAllKeyInput(int weight)
        {
            foreach (var normalKey in _normalKeysDic.Where(normalKey => normalKey.Value.weight <= weight))
            {
                normalKey.Value.SetEnable(false);
            }
        }
    }
}