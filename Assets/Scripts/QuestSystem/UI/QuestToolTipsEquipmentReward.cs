using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using UnityEngine.EventSystems;
namespace RPG.QuestSystem
{
    public class QuestToolTipsEquipmentReward : MonoBehaviour
    {
        [SerializeField] private GameObject inventorySlotPrefab;
        public void SetQuestToolTipsReward(BaseItemObject baseItemObject, int amount)
        {
            GameObject inventorySlotObj = UIResourcesManager.Instance.LoadUserInterface(inventorySlotPrefab, transform);
            // 添加鼠标事件
            EventTriggerManager.Instance.AddEvent(inventorySlotObj, EventTriggerType.PointerEnter, delegate {
                OnPointerEnterSlot(baseItemObject);
            });
            EventTriggerManager.Instance.AddEvent(inventorySlotObj, EventTriggerType.PointerExit, delegate {
                OnPointerExitSlot(baseItemObject);
            });
            UIInventorySlot tempUIInventorySlot = inventorySlotObj.GetComponent<UIInventorySlot>();
            // 设置图标
            tempUIInventorySlot.SetItemSprite(baseItemObject.sprite);
            // 物品数量等于1时候不设置数值显示
            tempUIInventorySlot.SetItemAmount(amount > 1 ? amount.ToString() : string.Empty);
        }
        private void OnPointerEnterSlot(BaseItemObject baseItemObject)
        {
            MouseItemTipsController.controller.OnEnter(new ItemToolTipsContent(baseItemObject, baseItemObject.item.itemBuffs));
        }
        private void OnPointerExitSlot(BaseItemObject baseItemObject)
        {
            MouseItemTipsController.controller.OnExit(new ItemToolTipsContent(baseItemObject, baseItemObject.item.itemBuffs));

        }
    }
}