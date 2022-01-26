using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Module
{
    public class GlobalResource : BaseSingletonWithMono<GlobalResource>
    {
        [SerializeField] private List<ResourcesSO> _objectInitializations;

        public void LoadGlobalResource()
        {
            // 加载或初始化全局数据
            foreach (var objectInitialization in _objectInitializations)
            {
                objectInitialization.InitAndLoad();
            }  
        }

        public T GetGlobalResource<T>() where T : ResourcesSO
        {
            foreach (var objectInitialization in _objectInitializations)
            {
                if (objectInitialization is T initialization)
                    return initialization;
            }
            return null;
        }
    }
}
