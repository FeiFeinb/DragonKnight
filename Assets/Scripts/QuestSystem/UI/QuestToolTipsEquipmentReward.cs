using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using RPG.UI;
using UnityEngine.EventSystems;
namespace RPG.QuestSystem
{
    public class QuestToolTipsEquipmentReward : MonoBehaviour
    {
        [SerializeField] private GameObject inventorySlotPrefab;
        public void SetQuestToolTipsReward(BaseItemObject baseItemObject, int amount)
        {
            GameObject inventorySlotObj = UIResourcesLoader.Instance.InstantiateUserInterface(inventorySlotPrefab, transform);
            // 添加鼠标事件
            GlobalEventRegister.AddLocalEvent(inventorySlotObj, EventTriggerType.PointerEnter, delegate {
                OnPointerEnterSlot(baseItemObject);
            });
            GlobalEventRegister.AddLocalEvent(inventorySlotObj, EventTriggerType.PointerExit, delegate {
                OnPointerExitSlot(baseItemObject);
            });
            UIInventorySlot tempUIInventorySlot = inventorySlotObj.GetComponent<UIInventorySlot>();
            // 设置图标
            tempUIInventorySlot.SetItemSprite(baseItemObject.sprite);
            // 物品数量等于1时候不设置数值显示
            tempUIInventorySlot.SetItemAmount(amount > 1 ? amount.ToString() : string.Empty);
        }
        
        // TODO: 更改为由Slot自动注册监听事件
        private void OnPointerEnterSlot(BaseItemObject baseItemObject)
        {
            BaseUI.GetController<MouseItemTipsController>().OnEnter(new ItemToolTipsContent(baseItemObject, baseItemObject.item.itemBuffs));
        }
        
        private void OnPointerExitSlot(BaseItemObject baseItemObject)
        {
            BaseUI.GetController<MouseItemTipsController>().OnExit();
        }
    }
}