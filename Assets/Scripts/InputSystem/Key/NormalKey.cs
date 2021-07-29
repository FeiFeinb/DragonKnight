using System;
using UnityEngine;

namespace RPG.InputSystyem
{
    [System.Serializable]
    public sealed class NormalKey : BaseKey
    {
        public enum HoldType
        {
            Click,
            Press
        }

        public NormalKey(KeyActionType keyActionType, KeyCode setKeyCode) : base(keyActionType)
        {
            SetKey(setKeyCode);
            SetEnable(true);
        }

        public bool isDown => _isDown;

        public KeyCode mainKeyCode => _mainKeyCode;

        /// <summary>
        /// 是否按下
        /// </summary>
        private bool _isDown = false;

        /// <summary>
        /// 对应的主键
        /// </summary>
        private KeyCode _mainKeyCode;

        /// <summary>
        /// 键触发事件
        /// </summary>
        private Action _trigger;

        /// <summary>
        /// 键触发类型
        /// </summary>
        private HoldType _holdType = HoldType.Click;

        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="paramKeyCode">键</param>
        public void SetKey(KeyCode paramKeyCode)
        {
            _mainKeyCode = paramKeyCode;
        }

        /// <summary>
        /// 设置键触发类型
        /// </summary>
        /// <param name="holdType">键触发类型</param>
        public void SetHoldType(HoldType holdType)
        {
            _holdType = holdType;
        }
        
        /// <summary>
        /// 设置此按键是否开启
        /// </summary>
        /// <param name="paramIsEnable">是否开启</param>
        public override void SetEnable(bool paramIsEnable)
        {
            base.SetEnable(paramIsEnable);
            _isDown = false;
        }

        /// <summary>
        /// 添加键触发事件
        /// </summary>
        /// <param name="callBack">键触发事件</param>
        public void AddTriggerListener(Action callBack)
        {
            _trigger += callBack;
        }

        /// <summary>
        /// 移除键触发事件
        /// </summary>
        /// <param name="callBack">键触发事件</param>
        public void RemoveTriggerListener(Action callBack)
        {
            _trigger -= callBack;
        }

        public override void HandleKey()
        {
            switch (_holdType)
            {
                case HoldType.Click:
                    if (_isEnable && Input.GetKeyDown(_mainKeyCode))
                    {
                        _isDown = true;
                        _trigger?.Invoke();
                        return;
                    }

                    _isDown = false;
                    break;
                case HoldType.Press:
                    if (_isEnable && Input.GetKey(_mainKeyCode))
                    {
                        _isDown = true;
                        _trigger?.Invoke();
                        return;
                    }

                    _isDown = false;
                    break;
            }
        }
    }
}