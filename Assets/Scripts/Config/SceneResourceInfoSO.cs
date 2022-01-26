using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Module;
using RPG.UI;
using Unity.Collections;
using UnityEngine;

namespace RPG.Config
{
    [Serializable]
    public class UILoadConfig
    {
        public GameObject loadPrefab;
        public CanvasType canvasType;
        [DisplayOnly] public string className;
    }
    
    [CreateAssetMenu(fileName = "New SceneResourceInfoSO", menuName = "Config/SceneResourceInfoSO", order = 0)]
    public class SceneResourceInfoSO : ResourcesSO, ISerializationCallbackReceiver
    {
        public List<UILoadConfig> configPairs = new List<UILoadConfig>();
        
        public void OnBeforeSerialize()
        {
            if (configPairs == null)
                return;
            foreach (var loadConfig in configPairs.Where(tempConfig => tempConfig.loadPrefab != null))
            {
                loadConfig.className = loadConfig.loadPrefab.GetComponent<BaseUI>().GetType().Name;
            }
        }

        public void OnAfterDeserialize() {}

        public override void InitAndLoad()
        {
            // 将需要的UI加载到场景中
            foreach (var config in configPairs)
            {
                UIResourcesLoader.Instance.LoadUserInterface(config);
            }
        }
    }
}