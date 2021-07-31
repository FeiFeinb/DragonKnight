using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
namespace RPG.InventorySystem
{
    public class EquipmentController : BaseInventoryController
    {
        public static string storePath = "UIView/EquipmentView";
        public static EquipmentController controller;
        public EquipmentInventoryObject equipmentInventoryObject;           // 显示哪个背包
        public Dictionary<GameObject, EquipmentInventorySlot> slotDic = new Dictionary<GameObject, EquipmentInventorySlot>();      // 字典
        public Dictionary<EquipmentInventorySlot, GameObject> slotUIDIc = new Dictionary<EquipmentInventorySlot, GameObject>();    // 字典
        [SerializeField] private PlayerAttributeTextManager playerAttributeTextManager;
        [SerializeField] private UIEquipmentSlotType[] slotTypes;
        public override void PreInit()
        {
            base.PreInit();
            // 初始化文本
            playerAttributeTextManager.InitPlayerAttributeTextManager();
            // 将对应的GameObject与对应的InventorySlot绑定
            EquipmentInventorySlot[] slot = equipmentInventoryObject.equipmentInventorySlot;
            // i 代表背包中的第i个物品
            for (int i = 0; i < slot.Length; i++)
            {
                var obj = slotTypes[i].gameObject;
                slotDic.Add(obj, slot[i]);
                slotUIDIc.Add(slot[i], obj);
                AddSlotEvent(obj);
                // 开启之后做一次插槽的更新
                // OnSlotUpdate(i);
            }
        }


        [ContextMenu("ReSortSlotType")]
        public void ReSortSlotType()
        {
            // 根据EquipmentInventoryObject中的顺序重新设置slotGameObject顺序
            var slot = equipmentInventoryObject.equipmentInventorySlot;
            // 如果slotGameObject数量与EquipmentInventoryObject中的装备插槽数不匹配
            if (slot.Length != slotTypes.Length)
            {
                Debug.LogError("Equipment Inventory Slots Amount Does Not Match");
                return;
            }
            for (int i = 0; i < slot.Length; i++)
            {
                slotTypes[i].Type = slot[i].equipmentSlotType;
            }
        }
        protected override InventorySlot[] GetSlots()
        {
            return equipmentInventoryObject.equipmentInventorySlot;
        }

        protected override InventorySlot GetSlot(GameObject keyObj)
        {
            return slotDic[keyObj];
        }
        public InventorySlot GetTypeSlot(EquipmentType _equipmentType)
        {
            foreach (var keyObj in slotDic)
            {
                foreach (var equipmentType in keyObj.Value.allowType.equipmentTypes)
                {
                    if (equipmentType == _equipmentType)
                    {
                        return keyObj.Value;
                    }
                }
            }
            return null;
        }

        protected override GameObject GetSlotUI(InventorySlot keySlot)
        {
            return slotUIDIc[keySlot as EquipmentInventorySlot];
        }
    }
}