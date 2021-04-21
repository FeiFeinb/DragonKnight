using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.SaveSystem
{
    public interface ISaveable
    {
        /// <summary>
        /// 创建保存信息
        /// </summary>
        /// <returns>保存信息</returns>
        object CreateState();

        /// <summary>
        /// 读取保存信息
        /// </summary>
        /// <param name="stateInfo">保存信息</param>
        void LoadState(object stateInfo);
        
        /// <summary>
        /// 重设保存信息
        /// </summary>
        void ResetState();
    }
}
