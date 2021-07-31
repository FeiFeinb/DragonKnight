using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
namespace RPG.InventorySystem
{
    public static class InventorySystemExtensionMethod
    {
        /// <summary>
        /// Buff数组数值更新
        /// </summary>
        /// <param name="itemBuff">源数组</param>
        public static void RenerateValues(this ItemBuff[] itemBuff)
        {
            for (int i = 0; i < itemBuff.Length; i++)
            {
                itemBuff[i].RenerateValue();
            }
        }

        public static string ToChinese(this BaseItemType baseItemType)
        {
            switch (baseItemType)
            {
                case BaseItemType.Sundry: return "杂项";
                case BaseItemType.Food: return "食物";
                default: return baseItemType.ToString();
            }
        }
        public static string ToChinese(this EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.Helmet: return "头盔";
                case EquipmentType.NeckLace: return "项链";
                case EquipmentType.BreastPlate: return "胸部";
                case EquipmentType.ArmGuard: return "护手";
                case EquipmentType.WristBand: return "护腕";
                case EquipmentType.Ornaments: return "饰品";
                case EquipmentType.ShinGuards: return "护腿";
                case EquipmentType.Boots: return "护足";
                case EquipmentType.OneHandedSword: return "单手剑";
                case EquipmentType.TwoHandedSword: return "双手剑";
                case EquipmentType.Shield: return "盾牌";
                default: return equipmentType.ToString();
            }
        }

        public static int ToAnimatorInt(this EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.Null: return 0;
                case EquipmentType.OneHandedSword: return 1;
                case EquipmentType.TwoHandedSword: return 2;
                case EquipmentType.Shield: return 3;
                default: return -1;
            }
        }
        public static string ToChinese(this ItemRare itemRare)
        {
            switch (itemRare)
            {
                case ItemRare.Normal: return "普通";
                case ItemRare.Rare: return "稀有";
                case ItemRare.Epic: return "史诗";
                case ItemRare.Legend: return "传说";
                default: return itemRare.ToString();
            }
        }
        public static string ToChinese(this AttributeType itemAttributeType)
        {
            switch (itemAttributeType)
            {
                case AttributeType.Armor: return "护甲";
                case AttributeType.Agility: return "敏捷";
                case AttributeType.Intellect: return "智力";
                case AttributeType.Stamina: return "耐力";
                case AttributeType.Strength: return "力量";
                case AttributeType.Critical: return "暴击";
                case AttributeType.Haste: return "急速";
                case AttributeType.Versatility: return "全能";
                case AttributeType.Mastery: return "精通";
                default: return itemAttributeType.ToString();
            }
        }
    }
}