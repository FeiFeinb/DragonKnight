using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class PlayerAttribute
    {
        public AttributeType itemAttributeType;     // 属性类型
        public ModifiedInt modifiedInt;             // 数值管理
        public Action<int> modifiedCallBack;        // 变化回调
        // 初始化未在Inspector窗口中初始化的变量
        public void InitAndStart()
        {
            modifiedInt.InitModifiedInt();
            modifiedInt.AddModifiedCallBack(OnAttributeUpdate);
        }
        // 属性变化时调用
        private void OnAttributeUpdate()
        {
            PlayerAttributeManager.Instance.AttributeModified(this);
            // 调用回调
            modifiedCallBack?.Invoke(modifiedInt.ModifiedValue);
        }
        public void AddUpdateCallBack(Action<int> callBack)
        {
            modifiedCallBack += callBack;
        }
        public void RemoveUpdateCallBack(Action<int> callBack)
        {
            modifiedCallBack -= callBack;
        }
    }
}