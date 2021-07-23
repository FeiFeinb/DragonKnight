using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.TradeSystem;
using UnityEngine.UI;
using RPG.Module;
namespace RPG.InventorySystem
{
    public class InventoryController : BaseInventoryController
    {
        public static string storePath = "UIView/InventoryView";
        public static InventoryController controller;
        
        public PlayerInventoryObject playerInventoryObject;     // 显示哪个背包
        
        [SerializeField] private GameObject inventorySlotPrefab;        // 物品插槽预制体
        [SerializeField] private Transform slotContainerTrans;          // 排列物品格子容器
        [SerializeField] private Text coinText;                         // 货币Text
        private Dictionary<GameObject, InventorySlot> slotDic = new Dictionary<GameObject, InventorySlot>();   // UI-插槽字典
        private Dictionary<InventorySlot, GameObject> slotUIDic = new Dictionary<InventorySlot, GameObject>(); // 插槽-UI字典


        public override void PreInit()
        {
            base.PreInit();
            // 将对应的GameObject与对应的InventorySlot绑定
            foreach (InventorySlot slot in playerInventoryObject.inventorySlots)
            {
                // 生成空物品栏
                var itemObject = UIResourcesManager.Instance.LoadUserInterface(inventorySlotPrefab, slotContainerTrans);
                slotDic.Add(itemObject, slot);
                slotUIDic.Add(slot, itemObject);
                AddSlotEvent(itemObject);
            }
            // 添加货币变更监听
            PlayerTradeManager.Instance.AddOnCoinUpdateListener(UpdateCoinText);
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

        private void UpdateCoinText(string coinStr)
        {
            coinText.text = coinStr;
        }
    }
}