using UnityEngine;

namespace RPG.InputSystyem
{
    [System.Serializable]
    public class UnitIntervalKey : IntervalKey
    {
        /// <summary>
        /// 区间范围
        /// </summary>
        private Vector2 _range = new Vector2(0, 1);

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

        public UnitIntervalKey(string keyName) : base(keyName)
        {
        }
    }
}