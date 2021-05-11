using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New PlayerInventoryObject", menuName = "Inventory System/Inventory/PlayerInventory")]
    public class PlayerInventoryObject : BaseInventoryObject
    {
        public int EmptySlotNum => GetFirstEmptySlot().Length;
        public InventorySlot[] inventorySlots;
        public bool AddItem(ItemData _item, int _amount)
        {
            // TODO: 优化AddItem的逻辑
            InventorySlot[] sameItemSlots = GetSameItemSlots(_item.id);
            // 不存在相同物品的插槽
            if (sameItemSlots.Length == 0)
            {
                Debug.Log("不存在相同物品的插槽");
                InventorySlot[] emptySlot = GetFirstEmptySlot();
                // 背包已满
                if (emptySlot.Length == 0)
                {
                    Debug.Log("背包已满");
                    return false;
                }
                // 背包未满
                else
                {
                    for (int i = 0; i < emptySlot.Length; i++)
                    {
                        Debug.Log("背包未满");
                        // 能够一次性放下
                        if (_amount <= GlobalResource.Instance.itemDataBase.itemObjs[_item.id].stackAmount)
                        {
                            emptySlot[i].UpdateSlot(new InventorySlotData(_item, _amount));
                            return true;
                        }
                        // 不能一次性放下
                        else
                        {
                            emptySlot[i].UpdateSlot(new InventorySlotData(_item, GlobalResource.Instance.itemDataBase.itemObjs[_item.id].stackAmount));
                            _amount -= GlobalResource.Instance.itemDataBase.itemObjs[_item.id].stackAmount;
                            continue;
                        }
                    }
                    // 空插槽使用完了 还没放完
                    Debug.Log("背包已满");
                    return false;
                }
            }
            // 存在相同物品的插槽
            else
            {
                Debug.Log("存在相同物品的插槽");
                for (int i = 0; i < sameItemSlots.Length; i++)
                {
                    // 此格已满
                    if (sameItemSlots[i].slotData.amount == sameItemSlots[i].itemObject.stackAmount)
                    {
                        Debug.Log($"第{i}个格子已满");
                        continue;
                    }
                    //此格子未满
                    else
                    {
                        Debug.Log($"第{i}个格子未满");
                        // 此格剩余数量
                        int amountLeft = sameItemSlots[i].itemObject.stackAmount - sameItemSlots[i].slotData.amount;
                        // 此格子能够一次性放下
                        if (_amount <= amountLeft)
                        {
                            Debug.Log($"第{i}个格子能一次性放下");
                            sameItemSlots[i].AddAmount(_amount);
                            return true;
                        }
                        // 此格子不能够一次性放下
                        else
                        {
                            Debug.Log($"第{i}个格子不能一次性放下");
                            sameItemSlots[i].AddAmount(amountLeft);
                            _amount -= amountLeft;
                            continue;
                        }
                    }
                }
                return AddItem(_item, _amount);
            }
        }

        public bool HasItem(int _itemID)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.slotData.itemData.id == _itemID)
                {
                    return true;
                }
            }
            return false;
        }
        private InventorySlot[] GetSameItemSlots(int itemID)
        {
            List<InventorySlot> slots = new List<InventorySlot>();
            // 遍历插槽
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // 如果该插槽为空 或已满 则跳过本次检查
                if (inventorySlots[i].isEmpty ||
                    inventorySlots[i].slotData.amount == GlobalResource.Instance.itemDataBase.itemObjs[itemID].stackAmount)
                {
                    continue;
                }
                if (itemID == inventorySlots[i].slotData.itemData.id)
                {
                    slots.Add(inventorySlots[i]);
                }
            }
            return slots.ToArray();
        }
        private InventorySlot[] GetFirstEmptySlot()
        {
            List<InventorySlot> emptySlots = new List<InventorySlot>();
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].isEmpty)
                {
                    emptySlots.Add(inventorySlots[i]);
                }
            }
            // 背包已满
            return emptySlots.ToArray();
        }

        protected override InventorySlot[] GetSlot()
        {
            return inventorySlots;
        }
    }
}