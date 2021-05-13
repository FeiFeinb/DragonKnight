using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace RPG.InventorySystem
{
    public static class ItemTextColor
    {
        public static string GetRareColorStr(ItemRare itemRare)
        {
            switch (itemRare)
            {
                case ItemRare.Normal:
                    return "gray";
                case ItemRare.Rare:
                    return "green";
                case ItemRare.Epic:
                    return "purple";
                case ItemRare.Legend:
                    return "yellow";
                default:
                    return "";
            }
        }
        public static string GetAttributeColorStr(AttributeType itemAttributeType)
        {
            switch (itemAttributeType)
            {
                case AttributeType.Armor: return "white";
                case AttributeType.Agility: return "white";
                case AttributeType.Intellect: return "white";
                case AttributeType.Stamina: return "white";
                case AttributeType.Strength: return "white";
                case AttributeType.Critical: return "green";
                case AttributeType.Haste: return "green";
                case AttributeType.Versatility: return "green";
                case AttributeType.Mastery: return "green";
                default: return "";
            }
        }
        public static string ColorCodeSplicing(string colorStr, string str)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"<color={colorStr}>").Append(str).Append("</color>");
            return strBuilder.ToString();
        }
    }
}
