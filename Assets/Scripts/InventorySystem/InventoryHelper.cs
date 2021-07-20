using RPG.InventorySystem;
using RPG.Module;

namespace UnityTemplateProjects.InventorySystem
{
    public class InventoryHelper : BaseSingleton<InventoryHelper>
    {
        public void SwapItem(InventorySlot originSlot, InventorySlot targetSlot)
        {
            // 禁止空物体与物体交换
            if (originSlot.IsEmpty) return;
            // 检测是否能交换
            if (originSlot.TypeMatch(targetSlot.ItemObj) && targetSlot.TypeMatch(originSlot.ItemObj))
            {
                BaseInventoryObject originObject =
                    PlayerInventoryManager.Instance.GetInventoryObjectFromSlot(originSlot);
                BaseInventoryObject targetObject =
                    PlayerInventoryManager.Instance.GetInventoryObjectFromSlot(targetSlot);
                // 只更新物品和数量
                InventorySlotData tempData = new InventorySlotData(originSlot.slotData);
                originSlot.UpdateSlot(targetSlot.slotData);
                originObject.HandleCallBack(tempData.itemData.id, -tempData.amount);
                originObject.HandleCallBack(targetSlot.slotData.itemData.id, targetSlot.slotData.amount);
                targetSlot.UpdateSlot(tempData);
                targetObject.HandleCallBack(originSlot.slotData.itemData.id, -originSlot.slotData.amount);
                targetObject.HandleCallBack(tempData.itemData.id, tempData.amount);
            }
        }

        public void ClearSlot(InventorySlot slot)
        {
            BaseInventoryObject baseInventoryObject = PlayerInventoryManager.Instance.GetInventoryObjectFromSlot(slot);
            int itemID = slot.slotData.itemData.id;
            int amount = slot.slotData.amount;
            slot.ClearSlot();
            baseInventoryObject.HandleCallBack(itemID, -amount);
        }
    }
}