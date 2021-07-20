using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using RPG.Module;
using UnityEngine;
using UnityEditor;

namespace RPG.InventorySystem
{
    public abstract class BaseInventoryObject : ScriptableObject
    {
        protected readonly Dictionary<int, Action<int>> itemAddedCallBackDic = new Dictionary<int, Action<int>>();

        public void RegisterAddItemListener(int itemID, Action<int> callBack)
        {
            if (itemAddedCallBackDic.ContainsKey(itemID))
            {
                itemAddedCallBackDic[itemID] += callBack;
            }
            else
            {
                itemAddedCallBackDic.Add(itemID, callBack);
            }
        }

        public void RemoveAddItemListener(int itemID, Action<int> callBack)
        {
            if (itemAddedCallBackDic.ContainsKey(itemID))
            {
                itemAddedCallBackDic[itemID] -= callBack;
            }
            else
            {
                Debug.LogError("字典中不包含此物品回调");
            }
        }
        
        public void HandleCallBack(int itemId, int amount)
        {
            if (itemId >= 0)
            {
                Debug.Log( $"{GlobalResource.Instance.itemDataBase.itemObjs[itemId].name}的数量变换了{amount}个");
            }
            if (itemAddedCallBackDic.ContainsKey(itemId))
            {
                itemAddedCallBackDic[itemId]?.Invoke(amount);
            }
        }


        [ContextMenu("Clear Inventory")]
        public void Clear()
        {
            foreach (InventorySlot inventorySlot in GetSlot())
            {
                int itemID = inventorySlot.slotData.itemData.id;
                int amount = inventorySlot.slotData.amount;
                inventorySlot.ClearSlot();
                HandleCallBack(itemID, -amount);
            }
        }

        public object GetData()
        {
            InventorySlot[] slots = GetSlot().ToArray();
            InventorySlotData[] slotData = new InventorySlotData[slots.Length];
            for (int i = 0; i < slotData.Length; i++)
            {
                slotData[i] = slots[i].slotData;
            }

            return slotData;
        }

        public void LoadData(object dataObj)
        {
            if (!(dataObj is InventorySlotData[] loadSlotData))
            {
                Debug.LogError("LoadSlotData Is Empty");
                return;
            }

            // TODO: 修复加载的存档的格子数量与当前不一致问题
            InventorySlot[] slots = GetSlot().ToArray();
            for (int i = 0; i < loadSlotData.Length; i++)
            {
                // 只更新物品数据和数量
                // TODO: 是否在加载存档的时候通知外界该格子已更新
                slots[i].UpdateSlot(loadSlotData[i]);
            }
        }
        
        [ContextMenu("InitSlot")]
        public void InitSlot()
        {
            InventorySlot[] slots = GetSlot().ToArray();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].slotIndex = i;
            }
        }

        protected abstract IEnumerable<InventorySlot> GetSlot();
    }
}