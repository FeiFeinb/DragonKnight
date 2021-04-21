using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.UI
{
    [System.Serializable]
    public class UIStoreInfo
    {
        public string prefabPath;
        public string prefabName { get; private set; }

        public UIStoreInfo(string _prefabPath)
        {
            prefabPath = _prefabPath;
            prefabName = prefabPath.Substring(prefabPath.LastIndexOf("/") + 1);
        }
        public UIStoreInfo(UIStoreInfo _storeInfo)
        {
            prefabPath = _storeInfo.prefabPath;
            prefabName = _storeInfo.prefabName;
        }
    }
}
