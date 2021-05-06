using RPG.TradeSystem;
using UnityEngine;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class BaseItemObject : ScriptableObject, ISerializationCallbackReceiver, IUseable
    {
        public Sprite sprite;               // 物品图标
        public ItemType itemType;           // 物品类型
        public bool isStackable;            // 是否堆叠
        public int stackAmount;             // 堆叠数量
        public Coin sellPrice;              // 售价
        public ItemData item = new ItemData();              // 物品
        [TextArea(5, 10)] public string description;       // 物品描述

        public virtual void OnAfterDeserialize()
        {
            // 自动更改堆叠值
            stackAmount = isStackable ? stackAmount : 1;
        }

        public virtual void OnBeforeSerialize()
        {
        }
        public virtual void Use(InventorySlot originSlot)
        {
        }
    }
}
