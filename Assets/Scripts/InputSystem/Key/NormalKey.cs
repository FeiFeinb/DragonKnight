using UnityEngine;

namespace RPG.InputSystyem
{
    [System.Serializable]
    public sealed class NormalKey : BaseKey
    {
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
        /// 设置主键
        /// </summary>
        /// <param name="paramKeyCode">键</param>
        public void SetKey(KeyCode paramKeyCode)
        {
            _mainKeyCode = paramKeyCode;
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

        public override void HandleKey()
        {
            if (_isEnable && Input.GetKeyDown(_mainKeyCode))
            {
                _isDown = true;
                Debug.Log(ActionType + "触发了");
                _trigger?.Invoke();
            }
        }
    }
}