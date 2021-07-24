using UnityEngine;

namespace RPG.InputSystyem
{
    public abstract class BaseKey
    {
        public BaseKey(string keyName)
        {
            _name = keyName;
        }
        public string name => _name;

        public bool isEnable => _isEnable;
        
        /// <summary>
        /// 键的名称
        /// </summary>
        protected string _name;
        
        /// <summary>
        /// 是否启用
        /// </summary>
        protected bool _isEnable = false;
        
        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="isEnable">是否启用</param>
        public virtual void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
        }

        /// <summary>
        /// 处理键的触发
        /// </summary>
        public virtual void HandleKey() {}

    }
}