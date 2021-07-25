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
        private Dictionary<string, NormalKey> _normalKeysDic = new Dictionary<string, NormalKey>();

        /// <summary>
        /// UnitIntervalKey数列
        /// </summary>
        private Dictionary<string, UnitIntervalKey> _unitIntervalKeysDic = new Dictionary<string, UnitIntervalKey>();

        /// <summary>
        /// AxisKey数列
        /// </summary>
        private Dictionary<string, AxisKey> _axisKeysDic = new Dictionary<string, AxisKey>();
        
        /// <summary>
        /// 添加NormalKey操作
        /// </summary>
        /// <param name="name">操作名</param>
        /// <param name="keyCode">键</param>
        public void AddNormalKey(string name, KeyCode keyCode)
        {
            if (!_normalKeysDic.ContainsKey(name))
            {
                _normalKeysDic.Add(name, new NormalKey(name, keyCode));
            }
            else
            {
                SetNormalKey(name, keyCode);
            }
        }

        public Dictionary<string, string> GetNormalKeyData()
        {
            Dictionary<string, string> dataDic = new Dictionary<string, string>();
            foreach (KeyValuePair<string,NormalKey> keyValuePair in _normalKeysDic)
            {
                dataDic.Add(keyValuePair.Key, keyValuePair.Value.keyCode.ToString());
            }

            return dataDic;
        }

        public void LoadNormalKeyData(Dictionary<string, string> data)
        {
            foreach (KeyValuePair<string,string> keyValuePair in data)
            {
                string keyName = keyValuePair.Key;
                AddNormalKey(keyName, keyValuePair.Value.ToKeyCode());
            }
        }
        
        
        /// <summary>
        /// 获取NormalKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <returns>NormalKey</returns>
        public NormalKey GetNormalKey(string keyName)
        {
            if (!_normalKeysDic.ContainsKey(keyName))
                throw new Exception($"Input系统中无法找到键{keyName}");
            return _normalKeysDic[keyName];
        }

        /// <summary>
        /// 获取ValueKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <returns>UnitIntervalKey</returns>
        public UnitIntervalKey GetUnitIntervalKey(string keyName)
        {
            if (!_unitIntervalKeysDic.ContainsKey(keyName))
                throw new Exception($"Input系统中无法找到键{keyName}");
            return _unitIntervalKeysDic[keyName];
        }

        /// <summary>
        /// 获取AxisKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <returns>AxisKey</returns>
        public AxisKey GetAxisKey(string keyName)
        {
            if (!_axisKeysDic.ContainsKey(keyName))
                throw new Exception($"Input系统中无法找到键{keyName}");
            return _axisKeysDic[keyName];
        }

        /// <summary>
        /// 设置NormalKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <param name="keyCode">键</param>
        public void SetNormalKey(string keyName, KeyCode keyCode)
        {
            NormalKey normalKey = GetNormalKey(keyName);
            normalKey?.SetKey(keyCode);
        }

        /// <summary>
        /// 设置ValueKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <param name="keyCode">键</param>
        public void SetUnitIntervalKey(string keyName, KeyCode keyCode)
        {
            UnitIntervalKey unitIntervalKey = GetUnitIntervalKey(keyName);
            unitIntervalKey?.SetKey(keyCode);
        }

        /// <summary>
        /// 设置AxisKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <param name="posKeyCode">正向键</param>
        /// <param name="negKeyCode">逆向键</param>
        public void SetAxisKey(string keyName, KeyCode posKeyCode, KeyCode negKeyCode)
        {
            AxisKey axisKey = GetAxisKey(keyName);
            axisKey?.SetKeyCode(posKeyCode, negKeyCode);
        }

        /// <summary>
        /// 设置AxisKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <param name="posKeyCode">正向键</param>
        public void SetAxisPosKey(string keyName, KeyCode posKeyCode)
        {
            AxisKey axisKey = GetAxisKey(keyName);
            axisKey?.SetPosKeyCode(posKeyCode);
        }

        /// <summary>
        /// 设置AxisKey
        /// </summary>
        /// <param name="keyName">操作名</param>
        /// <param name="negKeyCode">逆向键</param>
        public void SetAxisNegKey(string keyName, KeyCode negKeyCode)
        {
            AxisKey axisKey = GetAxisKey(keyName);
            axisKey?.SetNegKeyCode(negKeyCode);
        }

        public bool GetNormalKeyDown(string keyName)
        {
            NormalKey normalKey = GetNormalKey(keyName);
            if (normalKey == null) return false;
            return normalKey.isDown;
        }

        public bool GetNormalKeyDoubleDown(string keyName)
        {
            NormalKey normalKey = GetNormalKey(keyName);
            if (normalKey == null) return false;
            return normalKey.isDoubleDown;
        }

        public float GetUnitIntervalKeyValue(string keyName)
        {
            UnitIntervalKey unitIntervalKey = GetUnitIntervalKey(keyName);
            if (unitIntervalKey == null) return 0;
            return unitIntervalKey.value;
        }

        public float GetAxisKeyValue(string keyName)
        {
            AxisKey axisKey = GetAxisKey(keyName);
            if (axisKey == null) return 0;
            return axisKey.value;
        }

        public void SetNormalKeyEnable(string keyName, bool isEnable)
        {
            NormalKey normalKey = GetNormalKey(keyName);
            if (normalKey == null) return;
            normalKey.SetEnable(isEnable);
        }

        public void SetUnitIntervalKeyEnable(string keyName, bool isEnable)
        {
            UnitIntervalKey unitIntervalKey = GetUnitIntervalKey(keyName);
            if (unitIntervalKey == null) return;
            unitIntervalKey.SetEnable(isEnable);
        }

        public void SetAxisKeyEnable(string keyName, bool isEnable)
        {
            AxisKey axisKey = GetAxisKey(keyName);
            if (axisKey == null) return;
            axisKey.SetEnable(isEnable);
        }

        public void UpdateKey()
        {
            foreach (var normalKey in _normalKeysDic)
            {
                normalKey.Value.HandleKey();
            }
        }
    }
}