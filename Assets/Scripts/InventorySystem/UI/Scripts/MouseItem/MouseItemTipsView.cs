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
        public void SetTipName(string _tipName)
        {
            nameText.text = _tipName;
        }
        public void SetTipPosition(string _tipPosition)
        {
            positionText.text = _tipPosition;
        }
        public void SetTipAttribute(string _tipAttribute)
        {
            attributeText.enabled = !string.IsNullOrEmpty(_tipAttribute);
            attributeText.text = _tipAttribute;
        }
        public void SetTipRequire(string _tipRequire)
        {
            requireText.text = _tipRequire;
        }
        public void SetTipSellPrice(string _tipSellPrice)
        {
            sellPriceText.text = _tipSellPrice;
        }

        
    }
}