using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.InventorySystem
{
    public class PlayerAttributeTextManager : MonoBehaviour
    {
        [SerializeField] private PlayerAttributeText[] attributeTexts;  // Text数组
        public void InitPlayerAttributeTextManager()
        {
            PlayerAttribute[] attributes = PlayerAttributeManager.Instance.PlayerAttributes;
            for (int i = 0; i < attributes.Length; i++)
            {
                // 先设定初始值
                // 问题: 当属性中baseValue赋值时（第一次属性值更新）.下方的监听回调还尚未被添加
                // 故需要手动设定其值
                attributeTexts[i].SetAttributeName(attributes[i].itemAttributeType.ToChinese());
                attributeTexts[i].SetAttributeValue(attributes[i].modifiedInt.ModifiedValue);
                // 添加监听回调
                attributes[i].AddUpdateCallBack(attributeTexts[i].SetAttributeValue);
            }
        }
    }

}
