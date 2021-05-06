using RPG.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.TradeSystem
{
    public class CommoditySlot : MonoBehaviour
    {
        public Image itemImage;
        public Text itemName;
        public Text itemPrice;
        public void InitSlot(BaseItemObject itemObject)
        {
            if (itemObject == null) return;
            itemImage.sprite = itemObject.sprite;
            itemName.text = itemObject.name;
            itemPrice.text = itemObject.sellPrice.coinStr;
        }
    }
}