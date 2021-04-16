using UnityEngine;
namespace RPG.InventorySystem
{
    public enum BaseItemType
    {
        [Tooltip("杂项")] Sundry,
        [Tooltip("食物")] Food,
        [Tooltip("装备")] Equipment
    }
    public enum EquipmentSlotType
    {
        [Tooltip("头部")] Head,
        [Tooltip("颈部")] Neck,
        [Tooltip("胸部")] Chest,
        [Tooltip("手")] Hand,
        [Tooltip("腕部")] Wrist,
        [Tooltip("饰品")] Ornaments,
        [Tooltip("腿")] Leg,
        [Tooltip("脚")] Feet,
        [Tooltip("主手")] MainHand,
        [Tooltip("副手")] OffHand,
    }
    public enum EquipmentType
    {
        [Tooltip("空")] Null,
        [Tooltip("头盔")] Helmet,
        [Tooltip("项链")] NeckLace,
        [Tooltip("胸部")] BreastPlate,
        [Tooltip("护手")] ArmGuard,
        [Tooltip("护腕")] WristBand,
        [Tooltip("饰品")] Ornaments,
        [Tooltip("护腿")] ShinGuards,
        [Tooltip("护足")] Boots,
        [Tooltip("单手剑")] OneHandedSword,
        [Tooltip("双手剑")] TwoHandedSword,
        [Tooltip("盾牌")] Shield
    }
    public enum BodyPosition
    {
        [Tooltip("空")] Null,
        [Tooltip("头部")] Head,
        [Tooltip("胸部")] Chest,
        [Tooltip("左大臂")] LeftUpperArm,
        [Tooltip("右大臂")] RightUpperArm,
        [Tooltip("左小臂")] LeftLowerArm,
        [Tooltip("右小臂")] RightLowerArm,
        [Tooltip("左手")] LeftHand,
        [Tooltip("右手")] RightHand,
        [Tooltip("饰品")] Ornaments,
        [Tooltip("腿部")] Leg,
        [Tooltip("左足")] LeftFoot,
        [Tooltip("右足")] RightFoot
    }
    public enum AttributeType
    {
        [Tooltip("护甲")] Armor,
        [Tooltip("敏捷")] Agility,
        [Tooltip("智力")] Intellect,
        [Tooltip("耐力")] Stamina,
        [Tooltip("力量")] Strength,
        [Tooltip("暴击")] Critical,
        [Tooltip("急速")] Haste,
        [Tooltip("全能")] Versatility,
        [Tooltip("精通")] Mastery,
    }
    public enum ItemRare
    {
        [Tooltip("普通")] Normal,
        [Tooltip("稀有")] Rare,
        [Tooltip("史诗")] Epic,
        [Tooltip("传说")] Legend
    }
}