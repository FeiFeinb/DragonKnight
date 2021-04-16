using UnityEngine;
namespace RPG.InventorySystem
{

    public class UIEquipmentSlotType : MonoBehaviour
    {
        public EquipmentSlotType Type
        {
            get
            {
                return type;
            }
            set
            {
                // 设置类型
                type = value;
                // 设置名称
                gameObject.name = type.ToString();
            }
        }
        [SerializeField] private EquipmentSlotType type;
    }
}