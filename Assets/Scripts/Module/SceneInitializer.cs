using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Module
{
    public class SceneInitializer : MonoBehaviour
    {
        public List<ResourcesSO> objectInitializations;

        public void InitializeSceneData()
        {
            // 初始化并加载场景中的资源
            foreach (var objectInitialization in objectInitializations)
            {
                objectInitialization.InitAndLoad();
            }  
        }

    }
    
}
