using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem.Old.Dialogue.Core;
using DialogueSystem.Old.Dialogue.Core.Condition;
using UnityEngine;
using RPG.Module;
using RPG.SaveSystem;
using RPG.DialogueSystem.Graph;
namespace RPG.InventorySystem
{
    public class PlayerInventoryManager : BaseSingletonWithMono<PlayerInventoryManager>, ISaveable, IPredicateEvaluators
    {
        public PlayerInventoryObject inventoryObject;       // 玩家背包
        public EquipmentInventoryObject equipmentObject;    // 装备栏
        
        private void OnTriggerEnter(Collider other)
        {
            // 捡起物品
            var groundItem = other.GetComponent<GroundItem>();
            if (groundItem)
            {
                var result = inventoryObject.AddItem(new ItemData(groundItem.itemObj), 1);
                if (result == null)
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.Log($"背包已满，无法捡起{result.Item1.id}号物品-共{result.Item2}个");
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

        public bool? Evaluator(DialogueConditionType type, ScriptableObject paramSO)
        {
            BaseItemObject itemParamSO = paramSO as BaseItemObject;
            switch (type)
            {
                case DialogueConditionType.HasItem: return inventoryObject.HasItem(itemParamSO.item.id);
            }
            return null;
        }
    }
}