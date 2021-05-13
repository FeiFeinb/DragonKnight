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
        public int EmptySlotNum => GetEmptySlot().Count();


        public InventorySlot[] inventorySlots;
        
        public Tuple<ItemData, int> AddItem(ItemData addItem, int amount)
        {
            // TODO: 优化AddItem的逻辑
            // 存在相同物品的插槽
            foreach (InventorySlot sameItemSlot in GetSameItemSlots(addItem.id))
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
            // 不存在相同物品的插槽
            // 遍历空插槽
            foreach (InventorySlot emptySlot in GetEmptySlot())
            {
                // 能够一次性放下
                if (amount <= GlobalResource.Instance.itemDataBase.itemObjs[addItem.id].stackAmount)
                {
                    emptySlot.UpdateSlot(new InventorySlotData(addItem, amount));
                    return null;
                }
                // 不能一次性放下
                emptySlot.UpdateSlot(new InventorySlotData(addItem, GlobalResource.Instance.itemDataBase.itemObjs[addItem.id].stackAmount));
                amount -= GlobalResource.Instance.itemDataBase.itemObjs[addItem.id].stackAmount;
            }
            // 无空插槽 或 空插槽使用完了但还没放完 
            return new Tuple<ItemData, int>(addItem, amount);
        }

        public bool HasItem(int itemID)
        {
            return inventorySlots.Any(slot => slot.slotData.itemData.id == itemID);
        }

        private IEnumerable<InventorySlot> GetSameItemSlots(int itemID)
        {
            // 遍历插槽
            // 找出不空不满的插槽 再找出编号匹对的插槽
            return inventorySlots.Where(slot => !slot.IsEmpty && slot.slotData.amount != GlobalResource.Instance.itemDataBase.itemObjs[itemID].stackAmount).Where(flexSlot => itemID == flexSlot.slotData.itemData.id);
        }

        private IEnumerable<InventorySlot> GetEmptySlot()
        {
            return inventorySlots.Where((inventorySlot) => inventorySlot.IsEmpty);
        }

        protected override IEnumerable<InventorySlot> GetSlot()
        {
            return inventorySlots;
        }
    }
}