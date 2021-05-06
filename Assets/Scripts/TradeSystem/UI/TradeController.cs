using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;


using RPG.Module;
using UnityEngine;
using RPG.UI;
using RPG.InventorySystem;
using UnityEngine.EventSystems;

namespace RPG.TradeSystem
{
    public class TradeController : BaseUIController
    {
        public static string storePath = "UIView/TradeView";
        public static TradeController controller;

        private const int maxSlot = 12;
        [SerializeField] private Transform slotContainerTrans;
        [SerializeField] private GameObject commoditySlotPrefab;
        [SerializeField] private BusinessmanInventoryObject commodityObject;
        [SerializeField] private UIPageFlip pageFlip;
    
        // private Dictionary<GameObject, InventorySlot> slotDic = new Dictionary<GameObject, InventorySlot>();   // UI-插槽字典
        // private Dictionary<InventorySlot, GameObject> slotUIDic = new Dictionary<InventorySlot, GameObject>(); // 插槽-UI字典
        [System.Serializable]
        public class TestSlot
        {
            public GameObject slotObj;
            public BaseItemObject itemObj;
        }
        private TestSlot[] slotArray = new TestSlot[maxSlot];

        public override void PreInit()
        {
            pageFlip.Init(3);
            pageFlip.AddPageUpListener(OnPageUp);
            pageFlip.AddPageDownListener(OnPageDown);
            slotArray = new TestSlot[12];
            for (int i = 0; i < maxSlot; i++)
            {
                slotArray[i] = new TestSlot
                {
                    slotObj = UIResourcesManager.Instance.LoadUserInterface(commoditySlotPrefab, slotContainerTrans),
                    itemObj = null
                };
            }

        }

        public override void Show()
        {
            base.Show();
            
            // 先取前maxSlot个
            for (int i = 0; i < commodityObject.sellObjects.Length; i++)
            {
                slotArray[i].itemObj = commodityObject.sellObjects[i];
                slotArray[i].slotObj.GetComponent<CommoditySlot>().InitSlot(slotArray[i].itemObj);
                EventTriggerManager.Instance.AddEvent(slotArray[i].slotObj, EventTriggerType.PointerEnter,
                    delegate { OnPointerEnterSlot(slotArray[i].itemObj); });
                EventTriggerManager.Instance.AddEvent(slotArray[i].slotObj, EventTriggerType.PointerExit,
                    delegate { OnPointerExitSlot(slotArray[i].itemObj); });
            }
        }

        private void OnPointerEnterSlot(BaseItemObject baseItemObject)
        {
            MouseItemTipsController.controller.OnEnter(new ItemToolTipsContent(baseItemObject, baseItemObject.item.itemBuffs));
        }
        private void OnPointerExitSlot(BaseItemObject baseItemObject)
        {
            MouseItemTipsController.controller.OnEnter(new ItemToolTipsContent(baseItemObject, baseItemObject.item.itemBuffs));
        }
        private void OnPageUp()
        {
        
        }

        private void OnPageDown()
        {
        
        }
    }
}

