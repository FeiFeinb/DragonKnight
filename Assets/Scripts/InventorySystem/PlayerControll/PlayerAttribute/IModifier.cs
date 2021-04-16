using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.InventorySystem
{
    public interface IModifier
    {
        /// <summary>
        /// 叠加属性值
        /// </summary>
        /// <param name="addModifiedValue">源属性</param>
        void AddValue(ref int addModifiedValue);

        /// <summary>
        /// 减少属性值
        /// </summary>
        /// <param name="addModifiedValue">源属性</param>
        void SubValue(ref int addModifiedValue);
    }
}