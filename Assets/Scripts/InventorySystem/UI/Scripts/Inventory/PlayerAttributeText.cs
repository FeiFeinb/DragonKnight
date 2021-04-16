using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.InventorySystem
{
    public class PlayerAttributeText : MonoBehaviour
    {
        [SerializeField] private Text attributeName;    // 属性名字显示
        [SerializeField] private Text attributeValue;   // 属性数值显示
        public void SetAttributeName(string name)
        {
            attributeName.text = name;
        }
        public void SetAttributeValue(int value)
        {
            attributeValue.text = value.ToString();
        }
    }

}
