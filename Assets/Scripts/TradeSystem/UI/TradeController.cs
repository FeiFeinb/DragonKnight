using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using RPG.Module;
using UnityEngine;
using RPG.UI;
using RPG.InventorySystem;
using UnityEngine.EventSystems;

namespace RPG.TradeSystem
{
    public class TradeController : BaseUIController
    {
        [System.Serializable]
        public class SellItemSlotData
        {
            public GameObject slotObj;
            public BaseItemObject itemObj;
        }

        public static string storePath = "UIView/TradeView";
        public static TradeController controller;

        private const int maxSlot = 12; // 单页商店最大插槽数

        [SerializeField] private Transform slotContainerTrans; // UI生成父节点
        [SerializeField] private GameObject sellItemSlotPrefab; // 物品销售插槽
        [SerializeField] private BusinessmanInventoryObject commodityObject; // 商人背包
        [SerializeField] private UIPageFlip pageFlip; // 翻页组件

        private SellItemSlotData[] sellDatas; // 商店插槽数据数组

        public override void PreInit()
        {
            base.PreInit();
            // 添加监听
            pageFlip.AddPageUpListener(UpdateTradeView);
            pageFlip.AddPageDownListener(UpdateTradeView);
            // 创建空插槽数据类
            sellDatas = new SellItemSlotData[maxSlot];
            for (int i = 0; i < maxSlot; i++)
            {
                // 初始化插槽
                sellDatas[i] = new SellItemSlotData
                {
                    slotObj = UIResourcesManager.Instance.LoadUserInterface(sellItemSlotPrefab, slotContainerTrans),
                    itemObj = null
                };
                // 由于闭包特性 需要转变为临时变量
                SellItemSlotData sellData = sellDatas[i];
                // 添加鼠标进入监听
                EventTriggerManager.Instance.AddEvent(sellDatas[i].slotObj, EventTriggerType.PointerEnter,
                    delegate { OnPointerEnterSlot(sellData); });
                EventTriggerManager.Instance.AddEvent(sellDatas[i].slotObj, EventTriggerType.PointerExit,
                    delegate { OnPointerExitSlot(sellData); });
                EventTriggerManager.Instance.AddEvent(sellDatas[i].slotObj, EventTriggerType.PointerClick,
                    (baseEventData) =>
                    {
                        if ((baseEventData as PointerEventData)?.button == PointerEventData.InputButton.Right)
                        {
                            OnPointerRightClickSlot(sellData);
                        }
                    });
            }
        }


        public override void Show()
        {
            base.Show();
            // 每次打开页面都会从第一页开始显示
            pageFlip.Reset((commodityObject.sellObjects.Length - 1) / maxSlot + 1);
            UpdateTradeView();
        }

        private void OnPointerEnterSlot(SellItemSlotData sellData)
        {
            BaseItemObject itemObj = sellData.itemObj;
            if (itemObj == null) return;
            MouseItemTipsController.controller.OnEnter(new ItemToolTipsContent(itemObj, itemObj.item.itemBuffs));
        }

        private void OnPointerExitSlot(SellItemSlotData sellData)
        {
            BaseItemObject itemObj = sellData.itemObj;
            if (itemObj == null) return;
            MouseItemTipsController.controller.OnExit();
        }

        private void OnPointerRightClickSlot(SellItemSlotData sellData)
        {
            // 购买物品
            PlayerTradeManager.Instance.BuyItem(sellData.itemObj);
        }

        private void UpdateTradeView()
        {
            // 获取第一页应显示的插槽数量
            int showNum = pageFlip.PageValue < pageFlip.MaxPage
                ? maxSlot
                : commodityObject.sellObjects.Length - maxSlot * (pageFlip.PageValue - 1);
            int startIndex = (pageFlip.PageValue - 1) * maxSlot;
            for (int i = 0; i < sellDatas.Length; i++)
            {
                // 对要显示的插槽赋值
                if (i < showNum)
                {
                    BaseItemObject itemObj = commodityObject.sellObjects[i + startIndex];
                    sellDatas[i].itemObj = itemObj;
                    sellDatas[i].slotObj.gameObject.SetActive(true);
                    sellDatas[i].slotObj.GetComponent<SellItemSlot>().InitSlot(itemObj);
                }
                // 空插槽则隐藏
                else
                {
                    sellDatas[i].slotObj.gameObject.SetActive(false);
                }
            }
        }
    }
}