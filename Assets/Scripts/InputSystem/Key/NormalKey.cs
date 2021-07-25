using UnityEngine;

namespace RPG.InputSystyem
{
    [System.Serializable]
    public class NormalKey : BaseKey
    {
        public NormalKey(string keyName, KeyCode setKeyCode) : base(keyName)
        {
            SetKey(setKeyCode);
            SetEnable(true);
        }
        public bool isDown => _isDown;

        public bool isDoubleDown => _isDoubleDown;

        public KeyCode keyCode => _keyCode;
        /// <summary>
        /// 键的类型
        /// </summary>
        private KeyTriggerType _triggerType = KeyTriggerType.Click;

        /// <summary>
        /// 是否按下
        /// </summary>
        private bool _isDown = false;

        /// <summary>
        ///  是否双击按下
        /// </summary>
        private bool _isDoubleDown = false;

        /// <summary>
        /// 是否接受双击按下
        /// </summary>
        private bool _isAcceptDoubleDown = false;

        /// <summary>
        /// 双击按下检测间隔
        /// </summary>
        private float _pressInterval = 1.0f;

        /// <summary>
        /// 双击检测间隔计时器
        /// </summary>
        private float _pressIntervalTimer = 0f;

        /// <summary>
        /// 对应的键
        /// </summary>
        private KeyCode _keyCode;
        
        /// <summary>
        /// 设置对应的键
        /// </summary>
        /// <param name="keyCode">键</param>
        public void SetKey(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        /// <summary>
        /// 设置此按键是否开启
        /// </summary>
        /// <param name="isEnable">是否开启</param>
        public override void SetEnable(bool isEnable)
        {
            base.SetEnable(isEnable);
            _isDown = false;
            _isDoubleDown = false;
        }

        public override void HandleKey()
        {
            if (_isEnable && Input.GetKeyDown(_keyCode))
            {
                Debug.Log(name + "触发了");
                _isDown = true;
            }
        }
    }
}