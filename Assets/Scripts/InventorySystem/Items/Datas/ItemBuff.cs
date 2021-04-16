using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.InventorySystem
{
    [System.Serializable]
    public class ItemBuff : IModifier
    {
        public AttributeType itemAttributeType;                 // 物品属性
        [DisplayOnly, Tooltip("属性值")] public int value;      // 属性值
        [SerializeField] private int minValue;                  // 最小随机值
        [SerializeField] private int maxValue;                  // 最大随机值
        public ItemBuff(ItemBuff _itemBuff)
        {
            itemAttributeType = _itemBuff.itemAttributeType;
            minValue = _itemBuff.minValue;
            maxValue = _itemBuff.maxValue;
            value = _itemBuff.value;
        }

        public void AddValue(ref int addModifiedValue)
        {
            // 属性增加
            addModifiedValue += value;
        }
        public void SubValue(ref int addModifiedValue)
        {
            // 属性减少
            addModifiedValue -= value;
        }

        public void RenerateValue()
        {
            value = UnityEngine.Random.Range(minValue, maxValue);
        }

    }
}
