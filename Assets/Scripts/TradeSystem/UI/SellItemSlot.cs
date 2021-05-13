using RPG.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.TradeSystem
{
    public class SellItemSlot : MonoBehaviour
    {
        public Image image;
        public Text nameText;
        public Text priceText;
        public void InitSlot(BaseItemObject itemObject)
        {
            if (itemObject == null) return;
            // 设置图标
            image.sprite = itemObject.sprite;
            // 设置名称
            string colorStr = ItemTextColor.GetRareColorStr(itemObject.itemType.itemRare);
            nameText.text = ItemTextColor.ColorCodeSplicing(colorStr, itemObject.name);
            // 设置售价
            priceText.text = itemObject.sellPrice.coinStr;
        }
    }
}