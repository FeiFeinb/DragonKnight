using RPG.Module;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class InventorySlotData
    {
        public BaseItemObject itemObject
        {
            get
            {
                if (itemData?.id >= 0)
                {
                    return GlobalResource.Instance.itemDataBase.itemObjs[itemData.id];
                }
                return null;
            }
        }
        public bool isEmpty
        {
            get
            {
                return (itemData.id < 0 && amount == 0);
            }
        }
        public int amount;              // 物品数量
        public ItemData itemData;       // 物品数据
        public InventorySlotData()
        {
            itemData = new ItemData();
            amount = 0;
        }
        public InventorySlotData(ItemData _itemData, int _amount)
        {
            amount = _amount;
            itemData = _itemData;
        }
        public InventorySlotData(InventorySlotData _data)
        {
            amount = _data.amount;
            itemData = _data.itemData;
        }
    }
}