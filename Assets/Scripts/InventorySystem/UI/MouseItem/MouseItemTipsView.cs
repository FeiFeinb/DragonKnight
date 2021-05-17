using System.Text;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UI;
namespace RPG.InventorySystem
{
    public class MouseItemTipsView : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text positionText;
        [SerializeField] private Text attributeText;
        [SerializeField] private Text requireText;
        [SerializeField] private Text sellPriceText;
        public void SetTipName(string tipName)
        {
            nameText.text = tipName;
        }
        public void SetTipPosition(string tipPosition)
        {
            positionText.text = tipPosition;
        }
        public void SetTipAttribute(string tipAttribute)
        {
            attributeText.enabled = !string.IsNullOrEmpty(tipAttribute);
            attributeText.text = tipAttribute;
        }
        public void SetTipRequire(string tipRequire)
        {
            requireText.text = tipRequire;
        }
        public void SetTipSellPrice(string tipSellPrice)
        {
            sellPriceText.text = tipSellPrice;
        }

        
    }
}