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
            image.sprite = itemObject.sprite;
            nameText.text = itemObject.name;
            priceText.text = itemObject.sellPrice.coinStr;
        }
    }
}