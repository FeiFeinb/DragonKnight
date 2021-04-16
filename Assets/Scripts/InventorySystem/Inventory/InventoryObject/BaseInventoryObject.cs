using System.ComponentModel;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.InventorySystem
{
    public abstract class BaseInventoryObject : ScriptableObject
    {
        public int backpackCapacity;                    // 背包容量上限

        public static void SwapItem(InventorySlot originSlot, InventorySlot targetSlot)
        {
            // 禁止空物体与物体交换
            if (originSlot.isEmpty && !targetSlot.isEmpty)
            {
                return;
            }
            // 检测是否能交换
            if (originSlot.TypeMatch(targetSlot.itemObject) && targetSlot.TypeMatch(originSlot.itemObject))
            {
                // 只更新物品和数量
                InventorySlotData tempData = new InventorySlotData(originSlot.slotData);
                originSlot.UpdateSlot(targetSlot.slotData);
                targetSlot.UpdateSlot(tempData);
            }
        }

        public void RemoveItem(ItemData item)
        {
            InventorySlot[] slots = GetSlot();
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].slotData.itemData == item)
                {
                    slots[i].ClearSlot();
                }
            }
        }

        [ContextMenu("Clear Inventory")]
        public void Clear()
        {
            InventorySlot[] slots = GetSlot();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].ClearSlot();
            }
        }
        [ContextMenu("InitSlot")]
        public void InitSlot()
        {
            InventorySlot[] slots = GetSlot();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].slotIndex = i;
            }
        }
        public void LoadDatas(InventorySlotData[] loadSlotDatas)
        {
            if (loadSlotDatas == null)
            {
                Debug.LogError("LoadSlotDatas Is Empty");
                return;
            }
            // TODO: 修复加载的存档的格子数量与当前不一致问题
            InventorySlot[] slots = GetSlot();
            for (int i = 0; i < loadSlotDatas.Length; i++)
            {
                // 只更新物品数据和数量
                slots[i].UpdateSlot(loadSlotDatas[i]);
            }
        }

        protected abstract InventorySlot[] GetSlot();
        public virtual InventorySlotData[] GetDatas()
        {
            InventorySlot[] slots = GetSlot();
            InventorySlotData[] slotDatas = new InventorySlotData[slots.Length];
            for (int i = 0; i < slotDatas.Length; i++)
            {
                slotDatas[i] = slots[i].slotData;
            }
            return slotDatas;
        }
    }
}