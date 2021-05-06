using RPG.InventorySystem;
using UnityEngine;

namespace RPG.TradeSystem
{
    [CreateAssetMenu(fileName = "New BusinessmanInventoryObject", menuName = "Trade System/BusinessmanInventoryObject")]
    public class BusinessmanInventoryObject : ScriptableObject
    {
        public BaseItemObject[] sellObjects;
    }
}