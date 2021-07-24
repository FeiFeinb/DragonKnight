using System;
using UnityEngine;
using UnityEngine.Animations;
using Vector2 = System.Numerics.Vector2;

namespace RPG.InputSystyem
{
    [Serializable]
    public class AxisKey : IntervalKey
    {
        /// <summary>
        /// 区间范围
        /// </summary>
        private Vector2 _range = new Vector2(-1, 1);

        /// <summary>
        /// 正向对应键
        /// </summary>
        private KeyCode _posKeyCode;
        
        /// <summary>
        /// 逆向对应键
        /// </summary>
        private KeyCode _negKeyCode;

        /// <summary>
        /// 设置对应的键
        /// </summary>
        /// <param name="posKeyCode">正向对应键</param>
        /// <param name="negKeyCode">逆向对应键</param>
        public void SetKeyCode(KeyCode posKeyCode, KeyCode negKeyCode)
        {
            _posKeyCode = posKeyCode;
            _negKeyCode = negKeyCode;
        }

        /// <summary>
        /// 设置正向对应键
        /// </summary>
        /// <param name="posKeyCode">正向对应键</param>
        public void SetPosKeyCode(KeyCode posKeyCode)
        {
            _posKeyCode = posKeyCode;
        }

        /// <summary>
        /// 设置逆向对应键
        /// </summary>
        /// <param name="negKeyCode">逆向对应键</param>
        public void SetNegKeyCode(KeyCode negKeyCode)
        {
            _negKeyCode = negKeyCode;
        }

        public AxisKey(string keyName) : base(keyName)
        {
        }
    }
}