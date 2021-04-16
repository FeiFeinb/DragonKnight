using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
namespace RPG.InventorySystem
{
    public class InventoryController : BaseInventoryController
    {
        public static string storePath = "UIView/InventoryView";
        public static InventoryController controller;

        public PlayerInventoryObject playerInventoryObject;     // 显示哪个背包
        public Dictionary<GameObject, InventorySlot> slotDic = new Dictionary<GameObject, InventorySlot>();   // 字典
        public Dictionary<InventorySlot, GameObject> slotUIDic = new Dictionary<InventorySlot, GameObject>(); // 字典

        [SerializeField] private GameObject inventorySlotPrefab;        // 物品插槽预制体
        [SerializeField] private GameObject gridLayOutObj;              // 排列物件

        public override void PreInit()
        {
            base.PreInit();
            // 将对应的GameObject与对应的InventorySlot绑定
            for (int i = 0; i < playerInventoryObject.inventorySlots.Length; i++)
            {
                // 生成空物品栏
                var itemObject = GameObject.Instantiate(inventorySlotPrefab, gridLayOutObj.transform);
                slotDic.Add(itemObject, playerInventoryObject.inventorySlots[i]);
                slotUIDic.Add(playerInventoryObject.inventorySlots[i], itemObject);
                AddSlotEvent(itemObject);
                // 开启之后做一次插槽的更新
                // OnSlotUpdate(i);
            }
        }


        protected override InventorySlot[] GetSlots()
        {
            return playerInventoryObject.inventorySlots;
        }

        protected override InventorySlot GetSlot(GameObject keyObj)
        {
            return slotDic[keyObj];
        }

        protected override GameObject GetSlotUI(InventorySlot keySlot)
        {
            return slotUIDic[keySlot];
        }
    }
}