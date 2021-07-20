using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.InventorySystem
{
    public class PlayerEquipmentManager : BaseSingletonWithMono<PlayerEquipmentManager>
    {
        [SerializeField] private Transform loadObj;
        [SerializeField] private Transform mainHandTrans;           // 主手武器位置
        [SerializeField] private Transform offHandTrans;            // 副手武器位置
        [SerializeField] private Transform bonesTrans;              // 骨骼位置
        private Dictionary<BodyPosition, Transform> bodyTypeDic = new Dictionary<BodyPosition, Transform>();                // 记录每件装备对应的位置
        public Dictionary<BodyPosition, EquipmentBindInfo> originInfo = new Dictionary<BodyPosition, EquipmentBindInfo>();  // 角色初始外观
        private EquipmentCombiner equipmentCombiner;                 // 绑定类
        private GameObject mainHandWeapon;                           // 主手武器
        private GameObject offHandWeapon;                            // 副手武器
        private void Start()
        {
            // 保存角色初始状态
            InitData();
            // 新建绑定类
            // 同时设定绑定的装备外观集合以及骨骼
            equipmentCombiner = new EquipmentCombiner(loadObj, bonesTrans);
            // 为每个装备格子添加监听事件
            EquipmentInventorySlot[] slots = PlayerInventoryManager.Instance.equipmentObject.equipmentInventorySlot;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].AddBeforeUpdateListener(OnTakeOff);
                slots[i].AddAfterUpdateListener(OnWear);
            }
        }
        private void OnTakeOff(int slotIndex)
        {
            EquipmentInventorySlot[] slots = PlayerInventoryManager.Instance.equipmentObject.equipmentInventorySlot;
            if (!slots[slotIndex].IsEmpty)
            {
                // 脱下装备
                var type = slots[slotIndex].ItemObj.itemType;
                // 主手武器
                if (slots[slotIndex].equipmentSlotType == EquipmentSlotType.MainHand)
                {
                    Destroy(mainHandWeapon);
                }
                // 副手武器
                else if (slots[slotIndex].equipmentSlotType == EquipmentSlotType.OffHand)
                {
                    Destroy(offHandWeapon);
                }
                // 判断字典中是否有记录此装备类型
                else if (bodyTypeDic.ContainsKey(type.bodyType) && originInfo.ContainsKey(type.bodyType))
                {
                    // 解绑
                    equipmentCombiner.Combine(bodyTypeDic[type.bodyType].transform, originInfo[type.bodyType]);
                }
            }
        }
        private void OnWear(int slotIndex)
        {
            EquipmentInventorySlot slot = PlayerInventoryManager.Instance.equipmentObject.equipmentInventorySlot[slotIndex];
            if (!slot.IsEmpty)
            {
                // 穿上装备
                var equipmentItemObject = slot.ItemObj as EquipmentItemObject;
                ItemType type = slot.ItemObj.itemType;
                // 主手武器
                if (slot.equipmentSlotType == EquipmentSlotType.MainHand)
                {
                    mainHandWeapon = GameObject.Instantiate(equipmentItemObject.equipmentPrefab, mainHandTrans);
                }
                // 副手武器
                else if (slot.equipmentSlotType == EquipmentSlotType.OffHand)
                {
                    offHandWeapon = GameObject.Instantiate(equipmentItemObject.equipmentPrefab, offHandTrans);
                }
                // 判断此装备是否支持显示
                else if (type.bodyType != BodyPosition.Null)
                {
                    // 判断其装备预制体是否为空
                    if (equipmentItemObject.equipmentPrefab == null)
                    {
                        Debug.LogError("The EquipmentPrefab is Empty");
                        return;
                    }
                    // 绑定
                    int prefabHash = equipmentItemObject.equipmentPrefab.name.GetHashCode();
                    // 判断字典中是否记录此装备类型
                    if (bodyTypeDic.ContainsKey(type.bodyType))
                    {
                        equipmentCombiner.Combine(bodyTypeDic[type.bodyType], equipmentCombiner.bindInfoDic[prefabHash]);
                    }
                }
            }
        }
        [ContextMenu("InitData")]
        private void InitData()
        {
            // 清空两字典 防止数据重复
            bodyTypeDic = new Dictionary<BodyPosition, Transform>();
            originInfo = new Dictionary<BodyPosition, EquipmentBindInfo>();
            // 对子物体进行遍历 填充字典
            foreach (Transform child in transform)
            {
                BodyPosition type = child.GetComponent<BodyTypeInfo>().bodyType;
                child.gameObject.name = type.ToString();
                bodyTypeDic.Add(type, child);
                // 保存原有外观
                originInfo.Add(type, new EquipmentBindInfo(child.GetComponent<SkinnedMeshRenderer>()));
            }
        }
    }
}