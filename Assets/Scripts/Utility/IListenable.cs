using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility
{
    public interface IListenable<T>
    {
        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="action">带参数事件</param>
        void AddListener(Action<T> action);
        
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="action">带参数事件</param>
        void RemoveListener(Action<T> action);
    }

    public interface IListenable
    {
        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="action">无参事件</param>
        void AddListener(Action action);
        
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="action">无参事件</param>
        void RemoveListener(Action action);
    }
}