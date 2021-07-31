using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New EquipmentInventoryObject", menuName = "Inventory System/Inventory/EquipmentInventory")]
    public class EquipmentInventoryObject : BaseInventoryObject
    {
        public EquipmentInventorySlot[] equipmentInventorySlot;

        public (EquipmentType, EquipmentType) GetCurrentWeaponState()
        {
            EquipmentInventorySlot mainHandSlot =
                equipmentInventorySlot.FirstOrDefault(slot => slot.equipmentSlotType == EquipmentSlotType.MainHand);
            EquipmentInventorySlot offHandSlot =
                equipmentInventorySlot.FirstOrDefault(slot => slot.equipmentSlotType == EquipmentSlotType.OffHand);
            return (mainHandSlot.IsEmpty ? EquipmentType.Null : mainHandSlot.ItemObj.itemType.equipmentType,
                offHandSlot.IsEmpty ? EquipmentType.Null : offHandSlot.ItemObj.itemType.equipmentType);
        }
        
        protected override IEnumerable<InventorySlot> GetSlot()
        {
            return equipmentInventorySlot;
        }
    }
}