using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text amountText;
    
    public void SetItemSprite(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }
    public void SetItemAmount(string amountStr)
    {
        amountText.text = amountStr.ToString();
    }
}
