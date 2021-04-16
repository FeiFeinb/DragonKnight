using UnityEngine;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class ItemType : ISerializationCallbackReceiver
    {
        public BaseItemType baseItemType;         // 基础类型
        public EquipmentType equipmentType;       // 装备类型
        public BodyPosition bodyType;             // 穿着类型
        public ItemRare itemRare;                 // 物品稀有度
        public string GetTypeString()
        {
            if (baseItemType == BaseItemType.Equipment)
            {
                return equipmentType.ToChinese();
            }
            else
            {
                return baseItemType.ToChinese();
            }
        }
        public void OnAfterDeserialize()
        {
            // 默认设置
            equipmentType = (baseItemType == BaseItemType.Equipment ? equipmentType : EquipmentType.Null);
            bodyType = (baseItemType == BaseItemType.Equipment ? bodyType : BodyPosition.Null);
        }

        public void OnBeforeSerialize()
        {
        }
    }
}