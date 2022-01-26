using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Module;

namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New PlayerInventoryObject", menuName = "Inventory System/Inventory/PlayerInventory")]
    public class PlayerInventoryObject : BaseInventoryObject
    {
        public InventorySlot[] inventorySlots;
        
        /// <summary>
        /// 附带检查功能的添加物品 若不能一次性全部放下则不添加
        /// </summary>
        /// <param name="addItem">添加的物品</param>
        /// <param name="amount">数量</param>
        /// <returns>是否添加成功</returns>
        public bool AddWithCheck(ItemData addItem, int amount)
        {
            if (CanPlace(addItem, amount))
            {
                AddItem(addItem, amount);
                HandleCallBack(addItem.id, amount);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 不带检查功能的添加物品 会将能填满的部分全部填满
        /// </summary>
        /// <param name="addItem">添加的物品</param>
        /// <param name="amount">数量</param>
        /// <returns>剩下未能放入背包的物品数量</returns>
        public int AddWithoutCheck(ItemData addItem, int amount)
        {
            var pair = AddItem(addItem, amount);
            if (pair == null)
            {
                // 完全放下了
                HandleCallBack(addItem.id, amount);
                return 0;
            }
            if (pair.Item2 != amount)
            {
                // 放了一点
                HandleCallBack(addItem.id, pair.Item2);
            }
            return pair.Item2;
        }

        public bool RemoveWithCheck(int itemID, int amount)
        {
            int ownedAmount = inventorySlots.Where(slot => slot.slotData.itemData.id == itemID)
                .Sum(slot => slot.slotData.amount);
            if (ownedAmount < amount) return false;
            RemoveItem(itemID, amount);
            HandleCallBack(itemID, -amount);
            return true;
        }

        

        
        /// <summary>
        /// 拥有某物品的数量
        /// </summary>
        /// <param name="itemID">物品ID</param>
        /// <returns>数量</returns>
        public int HasItemAmount(int itemID)
        {
            return inventorySlots.Where(slot => slot.slotData.itemData.id == itemID).Sum(slot => slot.slotData.amount);
        }
        
        /// <summary>
        /// 是否拥有某物品
        /// </summary>
        /// <param name="itemID">物品ID</param>
        /// <returns>结果</returns>
        public bool HasItem(int itemID)
        {
            return inventorySlots.Any(slot => slot.slotData.itemData.id == itemID);
        }

        
        
        private bool CanPlace(ItemData addItem, int amount)
        {
            IEnumerable<InventorySlot> sameItemSlots = GetAvailableSameItemSlots(addItem.id);
            IEnumerable<InventorySlot> emptySlots = GetEmptySlot();
            int maxStackAmount = GlobalResource.Instance.GetGlobalResource<ItemDataBaseSO>().itemObjs[addItem.id].stackAmount;
            int availableAmount =
                sameItemSlots.Sum(slot =>
                    (int) Mathf.Clamp(maxStackAmount - slot.slotData.amount, 0f, Mathf.Infinity)) +
                emptySlots.Sum(slot => maxStackAmount);
            return availableAmount > amount;
        }

        private int RemoveItem(int itemID, int amount)
        {
            // TODO: 优化逻辑
            int removeLeft = amount;
            foreach (InventorySlot slot in inventorySlots.Where(slot => slot.slotData.itemData.id == itemID))
            {
                // 格子上存放的物品数量大于需要移除的数量
                if (slot.slotData.amount > removeLeft)
                {
                    slot.RemoveAmount(removeLeft);
                    return 0;
                }
                // 格子上存放的数量需要全部
                removeLeft -= slot.slotData.amount;
                slot.ClearSlot();
                if (removeLeft == 0) return 0;
            }
            
            // 若全部格子遍历完成后仍未能全部移除 返回剩余的数量
            return removeLeft;
        }
        
        private Tuple<ItemData, int> AddItem(ItemData addItem, int amount)
        {
            // 存在相同物品的插槽
            foreach (InventorySlot sameItemSlot in GetAvailableSameItemSlots(addItem.id))
            {
                // 此格已满
                if (sameItemSlot.slotData.amount == sameItemSlot.ItemObj.stackAmount) continue;
                // 此格子未满 剩余数量为
                int amountLeft = sameItemSlot.ItemObj.stackAmount - sameItemSlot.slotData.amount;
                // 此格子能够一次性放下
                if (amount <= amountLeft)
                {
                    sameItemSlot.AddAmount(amount);
                    return null;
                }

                // 此格子不能够一次性放下
                sameItemSlot.AddAmount(amountLeft);
                amount -= amountLeft;
                return AddItem(addItem, amount);
            }

            // 不存在相同物品的插槽 遍历空插槽
            foreach (InventorySlot emptySlot in GetEmptySlot())
            {
                // 能够一次性放下
                if (amount <= GlobalResource.Instance.GetGlobalResource<ItemDataBaseSO>().itemObjs[addItem.id].stackAmount)
                {
                    emptySlot.UpdateSlot(new InventorySlotData(addItem, amount));
                    return null;
                }

                // 不能一次性放下
                emptySlot.UpdateSlot(new InventorySlotData(addItem,
                    GlobalResource.Instance.GetGlobalResource<ItemDataBaseSO>().itemObjs[addItem.id].stackAmount));
                amount -= GlobalResource.Instance.GetGlobalResource<ItemDataBaseSO>().itemObjs[addItem.id].stackAmount;
            }

            // 无空插槽 或 空插槽使用完了但还没放完
            return new Tuple<ItemData, int>(addItem, amount);
        }


        private IEnumerable<InventorySlot> GetAvailableSameItemSlots(int itemID)
        {
            // 遍历插槽 找出不空不满的插槽 再找出编号匹对的插槽
            return inventorySlots
                .Where(slot => !slot.IsEmpty && slot.slotData.amount !=
                    GlobalResource.Instance.GetGlobalResource<ItemDataBaseSO>().itemObjs[itemID].stackAmount)
                .Where(flexSlot => itemID == flexSlot.slotData.itemData.id);
        }

        private IEnumerable<InventorySlot> GetEmptySlot()
        {
            return inventorySlots.Where(inventorySlot => inventorySlot.IsEmpty);
        }

        protected override IEnumerable<InventorySlot> GetSlot()
        {
            return inventorySlots;
        }
    }
}