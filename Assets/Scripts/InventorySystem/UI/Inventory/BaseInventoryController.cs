using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using RPG.UI;
using RPG.Module;
using UnityTemplateProjects.InventorySystem;

namespace RPG.InventorySystem
{
    [RequireComponent(typeof(EventTrigger))]
    public abstract class BaseInventoryController : BaseUI
    {
        protected override void InitInstance()
        {
            base.InitInstance();
            InventorySlot[] slots = GetSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].AddAfterUpdateListener(OnSlotUpdate);
            }
            // 鼠标进出UI背景事件
            GlobalEventRegister.AddLocalEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnPointerEnterUI(); });
            GlobalEventRegister.AddLocalEvent(gameObject, EventTriggerType.PointerExit, delegate { OnPointerExitUI(); });
        }
        
        protected void OnSlotUpdate(int slotIndex)
        {
            //  获取到UI所记录的背包的插槽
            var slot = GetSlots()[slotIndex];
            UIInventorySlot uiInventorySlot = GetSlotUI(slot).GetComponent<UIInventorySlot>();
            // 该插槽有物体
            if (slot.slotData.itemData.id >= 0)
            {
                uiInventorySlot.SetItemSprite(slot.ItemObj.sprite);
                uiInventorySlot.SetItemAmount(slot.slotData.amount == 1 ? string.Empty : slot.slotData.amount.ToString("n0"));
            }
            // 该插槽无物体
            else
            {
                uiInventorySlot.SetItemSprite(null, 0);
                uiInventorySlot.SetItemAmount(string.Empty);
            }
        }

        protected void AddSlotEvent(GameObject obj)
        {
            // 添加监听事件
            GlobalEventRegister.AddLocalEvent(obj, EventTriggerType.PointerEnter, delegate { OnPointerEnterSlot(obj); });  // 鼠标进入插槽
            GlobalEventRegister.AddLocalEvent(obj, EventTriggerType.PointerExit, delegate { OnPointerExitSlot(obj); });    // 鼠标推出插槽
            GlobalEventRegister.AddLocalEvent(obj, EventTriggerType.BeginDrag, delegate { OnBeginDrag(obj); });            // 开始拖动插槽
            GlobalEventRegister.AddLocalEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });                      // 拖动插槽途中
            GlobalEventRegister.AddLocalEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); });                // 结束拖动插槽
            // 右键点击插槽
            GlobalEventRegister.AddLocalEvent(obj, EventTriggerType.PointerClick, (baseEventData) =>
            {
                if ((baseEventData as PointerEventData)?.button == PointerEventData.InputButton.Right) { OnRightMouseClick(obj); }
            });
        }
        // 指针进入插槽
        protected void OnPointerEnterSlot(GameObject obj)
        {
            InventorySlot slot = GetSlot(obj);
            BaseUI.GetController<MouseItemIcon>().SetHoverObj(slot);
            if (!slot.IsEmpty)
            {
                BaseUI.GetController<MouseItemTipsController>().OnEnter(new ItemToolTipsContent(slot.ItemObj, slot.slotData.itemData.itemBuffs));
            }
        }
        // 指针离开插槽
        protected void OnPointerExitSlot(GameObject obj)
        {
            InventorySlot slot = GetSlot(obj);
            BaseUI.GetController<MouseItemIcon>().SetHoverObj(null);
            if (!slot.IsEmpty)
            {
                BaseUI.GetController<MouseItemTipsController>().OnExit();
            }
        }
        // 指针进入UI
        protected void OnPointerEnterUI()
        {
            BaseUI.GetController<MouseItemIcon>().SetHoverUI(this);
        }
        // 指针离开UI
        protected void OnPointerExitUI()
        {
            BaseUI.GetController<MouseItemIcon>().SetHoverUI(null);
        }
        protected void OnBeginDrag(GameObject obj)
        {
            // 物体可拖动
            BaseUI.GetController<MouseItemIcon>().CreateDragObject(obj, GetSlot(obj));
        }
        protected void OnDrag(GameObject obj)
        {
            // 拖拽过程移动
            BaseUI.GetController<MouseItemIcon>().MoveDragObject();
        }
        protected void OnEndDrag(GameObject obj)
        {
            // 删除拖拽图标
            BaseUI.GetController<MouseItemIcon>().DestroyDragObject();
            // 移除物品
            if (BaseUI.GetController<MouseItemIcon>().hoverUI == null)
            {
                InventoryHelper.Instance.ClearSlot(GetSlot(obj));
                return;
            }
            // 拖拽移动
            if (BaseUI.GetController<MouseItemIcon>().hoverSlot != null)
            {
                InventoryHelper.Instance.SwapItem(GetSlot(obj), BaseUI.GetController<MouseItemIcon>().hoverSlot);
            }
        }
        
        protected void OnRightMouseClick(GameObject obj)
        {
            // 右键单击插槽
            InventorySlot slot = GetSlot(obj);
            if (slot.IsEmpty) return;
            // 使用该物体
            slot.ItemObj.Use(slot);
            BaseUI.GetController<MouseItemTipsController>().OnExit();
        }
        
        public override void Hide()
        {
            // 如果两个界面打开 则禁止关闭
            if (BaseUI.GetController<MouseItemIcon>().isActive || BaseUI.GetController<MouseItemTipsController>().isActive) return;
            base.Hide();
        }
        
        protected abstract InventorySlot[] GetSlots();
        protected abstract InventorySlot GetSlot(GameObject keyObj);
        protected abstract GameObject GetSlotUI(InventorySlot keySlot);
    }
}