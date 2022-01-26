using UnityEngine;

namespace RPG.Module
{
    public class SceneResource : BaseSingletonWithMono<SceneResource>
    {
        public void LoadSceneResource()
        {
            // 找到场景中需要加载的数据
            SceneInitializer sceneResCenter = FindObjectOfType(typeof(SceneInitializer)) 
                as SceneInitializer;
            if (sceneResCenter == null) 
                return;
            // 将需要的UI加载到场景中
            sceneResCenter.InitializeSceneData();
        }
    }
}