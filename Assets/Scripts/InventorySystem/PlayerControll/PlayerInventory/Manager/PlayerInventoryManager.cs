using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Module;
using RPG.SaveSystem;
using RPG.DialogueSystem.Graph;

namespace RPG.InventorySystem
{
    public class PlayerInventoryManager : BaseSingletonWithMono<PlayerInventoryManager>, ISaveable
    {
        public PlayerInventoryObject inventoryObject; // 玩家背包
        public EquipmentInventoryObject equipmentObject; // 装备栏

        private void OnTriggerEnter(Collider other)
        {
            // 捡起物品
            // TODO: 将从地上捡起改为UI界面中拾取
            var groundItem = other.GetComponent<GroundItem>();
            if (groundItem)
            {
                int itemLeft = inventoryObject.AddWithoutCheck(new ItemData(groundItem.itemObj), 1);
                if (itemLeft == 0)
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.Log($"背包已满 剩下{itemLeft}个无法装下");
                }
            }
        }

        private void OnApplicationQuit()
        {
            // 退出游戏时清空背包
            inventoryObject.Clear();
            equipmentObject.Clear();
        }

        public object CreateState()
        {
            // 储存背包和装备栏的信息的字典
            Dictionary<string, object> saveSlotDic = new Dictionary<string, object>();
            saveSlotDic.Add(inventoryObject.GetHashCode().ToString(), inventoryObject.GetData());
            saveSlotDic.Add(equipmentObject.GetHashCode().ToString(), equipmentObject.GetData());
            return saveSlotDic;
        }

        public void LoadState(object stateInfo)
        {
            // 将stateInfo转为储存背包信息的字典
            if (!(stateInfo is Dictionary<string, object> saveSlotDic))
            {
                Debug.LogError("Cant Load State -- PlayerInventory");
                return;
            }

            // 加载字典中的信息
            inventoryObject.LoadData(saveSlotDic[inventoryObject.GetHashCode().ToString()]);
            equipmentObject.LoadData(saveSlotDic[equipmentObject.GetHashCode().ToString()]);
        }

        public void ResetState()
        {
        }

        public bool HasItem(BaseItemObject baseItemObject)
        {
            return inventoryObject.HasItem(baseItemObject.item.id);
        }

        public BaseInventoryObject GetInventoryObjectFromSlot(InventorySlot slot)
        {
            if (inventoryObject.inventorySlots.Any(tempSlot => tempSlot == slot))
                return inventoryObject;
            if (equipmentObject.equipmentInventorySlot.Any(tempSlot => tempSlot == slot))
                return equipmentObject;
            return null;
        }
    }
}