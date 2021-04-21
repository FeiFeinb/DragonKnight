using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using RPG.UI;
namespace RPG.InventorySystem
{
    [RequireComponent(typeof(EventTrigger))]
    public abstract class BaseInventoryController : BaseUIController
    {
        [SerializeField] protected Sprite uiMask;       // 空插槽UI

        public override void PreInit()
        {
            InventorySlot[] slots = GetSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].OnAfterUpdate += OnSlotUpdate;
            }
            // 鼠标进出UI背景事件
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnPointerEnterUI(); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnPointerExitUI(); });
        }
        protected void OnSlotUpdate(int slotIndex)
        {
            //  获取到UI所记录的背包的插槽
            var slot = GetSlots()[slotIndex];
            Transform slotUITrans = GetSlotUI(slot).transform;
            // 该插槽有物体
            if (slot.slotData.itemData.id >= 0)
            {
                slotUITrans.GetChild(0).GetComponentInChildren<Image>().sprite = slot.itemObject.sprite;
                slotUITrans.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = slot.slotData.amount == 1 ? "" : slot.slotData.amount.ToString("n0");
            }
            // 该插槽无物体
            else
            {
                slotUITrans.GetChild(0).GetComponentInChildren<Image>().sprite = uiMask;
                slotUITrans.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            // 获取EventTrigger
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            // 新建监听
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            // 添加监听
            trigger.triggers.Add(eventTrigger);
        }
        protected void AddSlotEvent(GameObject obj)
        {
            // 添加监听事件
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnPointerEnterSlot(obj); });  // 鼠标进入插槽
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnPointerExitSlot(obj); });    // 鼠标推出插槽
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnBeginDrag(obj); });            // 开始拖动插槽
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });                      // 拖动插槽途中
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); });                // 结束拖动插槽
            // 右键点击插槽
            AddEvent(obj, EventTriggerType.PointerClick, (baseEventData) =>
            {
                if ((baseEventData as PointerEventData)?.button == PointerEventData.InputButton.Right) { OnRightMouseClick(obj); }
            });
        }
        // 指针进入插槽
        private void OnPointerEnterSlot(GameObject obj)
        {
            MouseItemIcon.controller.SetHoverObj(GetSlot(obj));
            MouseItemTipsController.Instance.OnEnter(GetSlot(obj));
        }
        // 指针离开插槽
        private void OnPointerExitSlot(GameObject obj)
        {
            MouseItemIcon.controller.SetHoverObj(null);
            MouseItemTipsController.Instance.OnExit(GetSlot(obj));
        }
        // 指针进入UI
        private void OnPointerEnterUI()
        {
            MouseItemIcon.controller.SetHoverUI(this);
        }
        // 指针离开UI
        private void OnPointerExitUI()
        {
            MouseItemIcon.controller.SetHoverUI(null);
        }
        private void OnBeginDrag(GameObject obj)
        {
            // 物体可拖动
            MouseItemIcon.controller.CreateDragObject(obj, GetSlot(obj));
        }
        private void OnDrag(GameObject obj)
        {
            // 拖拽过程移动
            MouseItemIcon.controller.MoveDragObject();
        }
        private void OnEndDrag(GameObject obj)
        {
            // 删除拖拽图标
            MouseItemIcon.controller.DestroyDragObject();
            // 移除物品
            if (MouseItemIcon.controller.hoverUI == null)
            {
                GetSlot(obj).ClearSlot();
                return;
            }
            // 拖拽移动
            if (MouseItemIcon.controller.hoverSlot != null)
            {
                BaseInventoryObject.SwapItem(GetSlot(obj), MouseItemIcon.controller.hoverSlot);
            }
        }
        private void OnRightMouseClick(GameObject obj)
        {
            // 右键单击插槽
            InventorySlot slot = GetSlot(obj);
            if (slot.isEmpty) return;
            // 使用该物体
            slot.itemObject.Use(slot);
        }
        public override void Hide()
        {
            // 如果两个界面打开 则禁止关闭
            if (MouseItemIcon.controller.isActive || MouseItemTipsController.Instance.isActive) return;
            base.Hide();
        }
        protected abstract InventorySlot[] GetSlots();
        protected abstract InventorySlot GetSlot(GameObject keyObj);
        protected abstract GameObject GetSlotUI(InventorySlot keySlot);
    }
}