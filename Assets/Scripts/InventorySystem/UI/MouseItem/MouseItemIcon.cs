using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UI;
namespace RPG.InventorySystem
{
    public class MouseItemIcon : BaseUIController
    {
        public static MouseItemIcon controller;
        public static string storePath = "UIView/MouseItemIconView";

        [HideInInspector] public InventorySlot hoverSlot { get; private set; }     // 所指插槽物件

        [HideInInspector] public BaseInventoryController hoverUI;                  // 所处UI
        public void SetHoverObj(InventorySlot _hoverSlot)
        {
            hoverSlot = _hoverSlot;
        }
        public void SetHoverUI(BaseInventoryController _hoverUI)
        {
            hoverUI = _hoverUI;
        }
        public void CreateDragObject(GameObject obj, InventorySlot slot)
        {
            if (slot.slotData.itemData.id >= 0)
            {
                gameObject.transform.SetAsLastSibling();
                gameObject.SetActive(true);
                // 设置图片
                var objImage = gameObject.GetComponent<Image>();
                objImage.sprite = slot.ItemObj.sprite;
                objImage.raycastTarget = false;
            }
        }
        public void DestroyDragObject()
        {
            gameObject.gameObject.SetActive(false);
        }
        public void MoveDragObject()
        {
            if (gameObject != null)
            {
                gameObject.GetComponent<RectTransform>().position = UnityEngine.Input.mousePosition;
            }
        }
    }
}