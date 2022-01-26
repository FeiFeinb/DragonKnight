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
        /// 设置NormalKey 若不存在会自动添加
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="keyCode">键</param>
        public void SetOrAddNormalKey(KeyActionType actionType, KeyCode keyCode)
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
        /// 设置AxisKey 若不存在会自动添加
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="posKey">正向键</param>
        /// <param name="negKey">逆向键</param>
        public void SetOrAddAxisKey(KeyActionType actionType, KeyCode posKey, KeyCode negKey)
        {
            if (!_axisKeysDic.ContainsKey(actionType))
            {
                _axisKeysDic.Add(actionType, new AxisKey(actionType, posKey, negKey));
            }
            else
            {
                _axisKeysDic[actionType].SetKeyCode(posKey, negKey);
            }
        }

        /// <summary>
        /// 获取NormalKey
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <returns>NormalKey</returns>
        public NormalKey GetNormalKey(KeyActionType actionType)
        {
            if (!_normalKeysDic.ContainsKey(actionType))
                throw new Exception($"Input系统在NormalKey中无法找到键{actionType}");
            return _normalKeysDic[actionType];
        }

        /// <summary>
        /// 获取AxisKey
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <returns>AxisKey</returns>
        public AxisKey GetAxisKey(KeyActionType actionType)
        {
            if (!_axisKeysDic.ContainsKey(actionType))
                throw new Exception($"Input系统在AxisKey中无法找到键{actionType}");
            return _axisKeysDic[actionType];
        }

        /// <summary>
        /// 获取NormalKey按下状态
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <returns>键状态</returns>
        public bool GetNormalKeyDown(KeyActionType actionType)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            if (normalKey == null) return false;
            return normalKey.isDown;
        }

        /// <summary>
        /// 获取AxisKey渐变值
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <returns>渐变值</returns>
        public float GetAxisKeyValue(KeyActionType actionType)
        {
            AxisKey axisKey = GetAxisKey(actionType);
            if (axisKey == null) return 0;
            return axisKey.value;
        }

        /// <summary>
        /// 设置NormalKey的启用状态
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="isEnable">是否启用</param>
        public void SetNormalKeyEnable(KeyActionType actionType, bool isEnable)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            if (normalKey == null) return;
            normalKey.SetEnable(isEnable);
        }

        /// <summary>
        /// 设置AxisKey的启用状态
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="isEnable">是否启用</param>
        public void SetAxisKeyEnable(KeyActionType actionType, bool isEnable)
        {
            AxisKey axisKey = GetAxisKey(actionType);
            if (axisKey == null) return;
            axisKey.SetEnable(isEnable);
        }

        /// <summary>
        /// 设置NormalKey权重
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="weight">权重</param>
        public void SetNormalKeyWeights(KeyActionType actionType, int weight)
        {
            NormalKey normalKey = GetNormalKey(actionType);
            normalKey.SetWeight(weight);
        }

        /// <summary>
        /// 设置AxisKey权重
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="weight">权重</param>
        public void SetAxisKeyWeights(KeyActionType actionType, int weight)
        {
            AxisKey axisKey = GetAxisKey(actionType);
            axisKey.SetWeight(weight);
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

            foreach (var axisKey in _axisKeysDic.Where(axisKey => axisKey.Value.weight <= weight))
            {
                axisKey.Value.SetEnable(true);
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

            foreach (var axisKey in _axisKeysDic.Where(axisKey => axisKey.Value.weight <= weight))
            {
                axisKey.Value.SetEnable(false);
            }
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

            foreach (var axisKey in _axisKeysDic)
            {
                axisKey.Value.HandleKey();
            }
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
        /// 获取NormalKey数据 可将传达给Json进行写操作
        /// </summary>
        /// <returns>数据字典</returns>
        public Dictionary<string, InputStrTuple> GetKeyData()
        {
            var dataDic = new Dictionary<string, InputStrTuple>();


            // 常规点按键
            foreach (KeyValuePair<KeyActionType, NormalKey> keyValuePair in _normalKeysDic)
            {
                var tuple = new InputStrTuple(KeyType.NormalKey.ToString(),
                    keyValuePair.Value.mainKeyCode.ToString(), KeyCode.None.ToString());
                dataDic.Add(keyValuePair.Key.ToString(), tuple);
            }

            // Axis键
            foreach (KeyValuePair<KeyActionType, AxisKey> keyValuePair in _axisKeysDic)
            {
                var tuple = new InputStrTuple(KeyType.AxisKey.ToString(),
                    keyValuePair.Value.posKeyCode.ToString(), keyValuePair.Value.negKeyCode.ToString());
                dataDic.Add(keyValuePair.Key.ToString(), tuple);
            }

            return dataDic;
        }

        /// <summary>
        /// 将外界传来的数据字典记录到类中
        /// </summary>
        /// <param name="data">数据字典</param>
        public void LoadKeyData(Dictionary<string, InputStrTuple> data)
        {
            foreach (var pair in data)
            {
                switch (pair.Value.keyTypeStr.ToKeyType())
                {
                    case KeyType.NormalKey:
                        SetOrAddNormalKey(pair.Key.ToKeyActionType(), pair.Value.firstKeyCodeStr.ToKeyCode());
                        break;
                    case KeyType.AxisKey:
                        SetOrAddAxisKey(pair.Key.ToKeyActionType(), pair.Value.firstKeyCodeStr.ToKeyCode(),
                            pair.Value.secondKeyCodeStr.ToKeyCode());
                        break;
                }
            }
        }
    }
}