using RPG.UI;
using UnityEngine;
using UnityTemplateProjects.InventorySystem;

namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New EquipmentItemObject", menuName = "Inventory System/ItemObject/EquipmentItemObject")]
    public class EquipmentItemObject : BaseItemObject
    {
        public GameObject equipmentPrefab;  // 物品实体模型
        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            itemType.baseItemType = BaseItemType.Equipment;
        }

        public override void OnBeforeSerialize()
        {
        }
        public override void Use(InventorySlot originSlot)
        {
            if (originSlot is EquipmentInventorySlot)
            {
                Debug.Log("物品已经装备，使用物品本身");
            }
            else
            {
                Debug.Log("物品未被装备，将其装备");
                InventorySlot targetSlot = BaseUI.GetController<EquipmentController>().GetTypeSlot(itemType.equipmentType);
                InventoryHelper.Instance.SwapItem(originSlot, targetSlot);
            }
        }
    }
}