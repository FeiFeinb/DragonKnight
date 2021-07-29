using System;
using UnityEngine;

namespace RPG.InputSystyem
{
    public abstract class BaseKey
    {
        public BaseKey(KeyActionType keyActionType)
        {
            _actionType = keyActionType;
        }
        public KeyActionType ActionType => _actionType;

        public bool isEnable => _isEnable;
        public int weight => _weight;
        
        /// <summary>
        /// 键的名称
        /// </summary>
        protected KeyActionType _actionType;
        
        /// <summary>
        /// 是否启用
        /// </summary>
        protected bool _isEnable = false;

        /// <summary>
        /// 设置权重
        /// </summary>
        protected int _weight;

        /// <summary>
        /// 上锁次数计数器
        /// </summary>
        protected int _lockTimer = 1;
        
        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="paramIsEnable">是否启用</param>
        public virtual void SetEnable(bool paramIsEnable)
        {
            _lockTimer = (int)Mathf.Clamp(_lockTimer + (paramIsEnable ? -1 : 1), 0, Mathf.Infinity);
            // 解锁操作 需检测计数器是否为0 上锁操作可以直接锁
            if (paramIsEnable && _lockTimer == 0 || !paramIsEnable)
            {
                _isEnable = paramIsEnable;
            }
        }

        /// <summary>
        /// 设置键的权重
        /// </summary>
        /// <param name="paramWeight">权重</param>
        public void SetWeight(int paramWeight)
        {
            _weight = paramWeight;
        }

        /// <summary>
        /// 处理键的触发
        /// </summary>
        public abstract void HandleKey();

    }
}