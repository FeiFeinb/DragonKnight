using System.Collections.Generic;
using UnityEngine;
namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New EquipmentInventoryObject", menuName = "Inventory System/Inventory/EquipmentInventory")]
    public class EquipmentInventoryObject : BaseInventoryObject
    {
        public EquipmentInventorySlot[] equipmentInventorySlot;

        protected override InventorySlot[] GetSlot()
        {
            return equipmentInventorySlot;
        }
    }
}