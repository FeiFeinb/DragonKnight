using UnityEngine;
namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New FoodItemObject", menuName = "Inventory System/ItemObject/FoodItemObject")]
    public class FoodItemObject : BaseItemObject, ISerializationCallbackReceiver
    {
        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            itemType.baseItemType = BaseItemType.Food;
        }

        public override void OnBeforeSerialize()
        {
        }
        // TODO: 添加食物增益效果
        public override void Use(InventorySlot originSlot)
        {
            Debug.Log("吃食物");
        }
    }
}