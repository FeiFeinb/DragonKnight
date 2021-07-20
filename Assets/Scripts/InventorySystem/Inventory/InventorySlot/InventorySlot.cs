using System.Data.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 物品插槽
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        public BaseItemObject ItemObj
        {
            get
            {
                return slotData.ItemObj;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return slotData.IsEmpty;
            }
        }
        [DisplayOnly] public int slotIndex;         // 插槽编号
        public AllowType allowType;                 // 允许防止的类型
        public InventorySlotData slotData;          // 插槽存储数据
        
        private Action<int> _onBeforeUpdate;   // 插槽更新前事件
        private Action<int> _onAfterUpdate;    // 插槽更新后事件

        public void AddBeforeUpdateListener(Action<int> callBack)
        {
            _onBeforeUpdate += callBack;
        }

        public void AddAfterUpdateListener(Action<int> callBack)
        {
            _onAfterUpdate += callBack;
        }

        public void RemoveBeforeUpdateListener(Action<int> callBack)
        {
            _onBeforeUpdate -= callBack;
        }

        public void RemoveAfterUpdateListener(Action<int> callBack)
        {
            _onAfterUpdate -= callBack;
        }
        
        // 添加数量
        public void AddAmount(int amount)
        {
            _onBeforeUpdate?.Invoke(slotIndex);
            slotData.amount += amount;
            _onAfterUpdate?.Invoke(slotIndex);
        }

        public void RemoveAmount(int amount)
        {
            _onBeforeUpdate?.Invoke(slotIndex);
            slotData.amount -= amount;
            _onAfterUpdate?.Invoke(slotIndex);
        }
        
        // 更新插槽值
        public void UpdateSlot(InventorySlotData _slotData)
        {
            _onBeforeUpdate?.Invoke(slotIndex);
            slotData.itemData = _slotData.itemData;
            slotData.amount = _slotData.amount;
            _onAfterUpdate?.Invoke(slotIndex);
        }
        // 清空插槽值
        public void ClearSlot()
        {
            UpdateSlot(new InventorySlotData());
        }
        public bool TypeMatch(BaseItemObject itemObj)
        {
            // itemObj为空 itemObj无记录物体
            if (itemObj == null || itemObj.item.id < 0)
            {
                return true;
            }
            return allowType.Match(itemObj.itemType);
        }
    }
}