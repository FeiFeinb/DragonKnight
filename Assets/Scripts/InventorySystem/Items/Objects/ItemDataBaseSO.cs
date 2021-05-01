using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.InventorySystem
{
    [CreateAssetMenu(fileName = "New ItemDataBaseSO", menuName = "Inventory System/ItemDataBaseSO")]
    public class ItemDataBaseSO : ScriptableObject
    {
        public string loadPath;
        public BaseItemObject[] itemObjs;

        [ContextMenu("LoadDateBaseItem")]
        public void LoadDateBaseItem()
        {
            BaseItemObject[] loadSO = Resources.LoadAll<BaseItemObject>(loadPath);
            itemObjs = new BaseItemObject[loadSO.Length];
            for (int i = 0; i < loadSO.Length; i++)
            {
                itemObjs[i] = loadSO[i] as BaseItemObject;
            }
            UpdateDateBaseID();
        }
        public void UpdateDateBaseID()
        {
            for (int i = 0; i < itemObjs.Length; i++)
            {
                // 仓库序列化时赋值ID
                itemObjs[i].item.id = i;
            }
        }
    }
}