using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text amountText;
    
    public void SetItemSprite(Sprite itemSprite, float alpha = 1f)
    {
        itemImage.sprite = itemSprite;
        itemImage.color = new Color(1f, 1f, 1f, alpha);
    }
    public void SetItemAmount(string amountStr)
    {
        amountText.text = amountStr.ToString();
    }
}
