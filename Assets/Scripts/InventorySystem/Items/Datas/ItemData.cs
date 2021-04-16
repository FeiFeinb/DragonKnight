using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class ItemData
    {
        [DisplayOnly] public int id;          // 物品Id
        public ItemBuff[] itemBuffs;
        public ItemData()
        {
            id = -1;
            itemBuffs = new ItemBuff[0];
        }
        public ItemData(ItemData itemData)
        {
            InitItemData(itemData);
        }
        public ItemData(BaseItemObject itemObj)
        {
            InitItemData(itemObj.item);
        }
        public void InitItemData(ItemData itemData)
        {
            id = itemData.id;
            itemBuffs = new ItemBuff[itemData.itemBuffs.Length];
            for (int i = 0; i < itemData.itemBuffs.Length; i++)
            {
                // 重新生成buff属性
                itemBuffs[i] = new ItemBuff(itemData.itemBuffs[i]);
            }
        }
    }
}
