using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
using RPG.SaveSystem;
namespace RPG.InventorySystem
{
    public class PlayerAttributeManager : BaseSingletonWithMono<PlayerAttributeManager>, ISaveable
    {
        public PlayerAttribute[] PlayerAttributes => playerAttributes;
        [SerializeField] private PlayerAttribute[] playerAttributes;      // 角色属性
        private void Start()
        {
            // 为每个装备格子添加监听事件
            EquipmentInventorySlot[] slots = PlayerInventoryManager.Instance.equipmentObject.equipmentInventorySlot;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].AddBeforeUpdateListener(OnBeforeUpdateEquipmentSlot);
                slots[i].AddAfterUpdateListener(OnAfterUpdateEquipmentSlot);
            }
            // 属性变化时监听
            for (int i = 0; i < playerAttributes.Length; i++)
            {
                playerAttributes[i].InitAndStart();
            }
        }
        public void AttributeModified(PlayerAttribute playerAttribute)
        {
            Debug.Log(string.Concat(playerAttribute.itemAttributeType.ToChinese(), "现在为", playerAttribute.modifiedInt.ModifiedValue));
        }
        private void OnBeforeUpdateEquipmentSlot(int slotIndex)
        {
            EquipmentInventorySlot[] slots = PlayerInventoryManager.Instance.equipmentObject.equipmentInventorySlot;
            // 物体离开插槽
            if (!slots[slotIndex].IsEmpty)
            {
                Debug.Log($"{slots[slotIndex].ItemObj.name}离开插槽");
                var tempTuples = ModifiedIntTraverse(slots[slotIndex]);
                foreach (var tempTuple in tempTuples)
                {
                    tempTuple?.Item1.RemoveModifier(tempTuple?.Item2);
                }
            }
        }

        private void OnAfterUpdateEquipmentSlot(int slotIndex)
        {
            EquipmentInventorySlot[] slots = PlayerInventoryManager.Instance.equipmentObject.equipmentInventorySlot;
            // 物体进入插槽
            if (!slots[slotIndex].IsEmpty)
            {
                Debug.Log($"{slots[slotIndex].ItemObj.name}进入插槽");
                var tempTuples = ModifiedIntTraverse(slots[slotIndex]);
                foreach (var tempTuple in tempTuples)
                {
                    tempTuple?.Item1.AddModifier(tempTuple?.Item2);
                }
            }
        }
        private Tuple<ModifiedInt, IModifier>[] ModifiedIntTraverse(InventorySlot inventorySlot)
        {
            if (!inventorySlot.IsEmpty)
            {
                var itemBuffs = inventorySlot.slotData.itemData.itemBuffs;
                List<Tuple<ModifiedInt, IModifier>> tuples = new List<Tuple<ModifiedInt, IModifier>>();
                foreach (ItemBuff itemBuff in itemBuffs)
                {
                    foreach (PlayerAttribute playerAttribute in playerAttributes)
                    {
                        if (playerAttribute.itemAttributeType == itemBuff.itemAttributeType)
                        {
                            tuples.Add(new Tuple<ModifiedInt, IModifier>(playerAttribute.modifiedInt, itemBuff));
                        }
                    }
                }
                return tuples.ToArray();
            }
            return null;
        }

        public object CreateState()
        {
            return null;
        }

        public void LoadState(object stateInfo)
        {
        }

        public void ResetState()
        {
            Debug.Log("PlayerAttributeManager ResetState");
            for (int i = 0; i < playerAttributes.Length; i++)
            {
                // FIXME: 在读取存档前先设置baseValue 让Init能够重置到一个正确的值
                playerAttributes[i].modifiedInt.InitModifiedInt();
            }
        }
    }
}