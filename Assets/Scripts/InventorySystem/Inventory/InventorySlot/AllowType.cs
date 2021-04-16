using UnityEngine;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class AllowType : ISerializationCallbackReceiver
    {
        public bool allIn = true;      // 任何物体都可进入
        public BaseItemType[] baseItemTypes;
        public EquipmentType[] equipmentTypes;
        
        public bool Match(ItemType itemType)
        {
            if (allIn)
            {
                return true;
            }
            for (int i = 0; i < baseItemTypes.Length; i++)
            {
                if (baseItemTypes[i] == itemType.baseItemType)
                {
                    if (baseItemTypes[i] == BaseItemType.Equipment)
                    {
                        for (int j = 0; j < equipmentTypes.Length; j++)
                        {
                            if (equipmentTypes[j] == itemType.equipmentType)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            // 遍历完后仍未匹配
            return false;
        }

        public void OnAfterDeserialize()
        {
            allIn = (equipmentTypes == null || equipmentTypes.Length == 0);
        }

        public void OnBeforeSerialize()
        {

        }
    }
}