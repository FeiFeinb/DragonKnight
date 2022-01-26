using System.Collections;
using System.Collections.Generic;
using RPG.Module;
using UnityEngine;
using UnityEditor;
namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New ItemDataBaseSO", menuName = "Inventory System/ItemDataBaseSO")]
    public class ItemDataBaseSO : DataBaseSO
    {
        public string loadPath;
        
        public List<BaseItemObject> itemObjs;
        
        
#if UNITY_EDITOR
        public void GenerateItemID()
        {
            BaseItemObject[] loadSO = Resources.LoadAll<BaseItemObject>(loadPath);
            for (int i = 0; i < loadSO.Length; i++)
            {
                itemObjs.Add(loadSO[i]);
                itemObjs[i].item.id = i;
            }
            EditorUtility.SetDirty(this);
        }
#endif
        public override void InitAndLoad()
        {
        }
    }
}