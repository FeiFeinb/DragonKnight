using System;
using System.Collections;
using RPG.InputSystyem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Module
{
    public class GlobalManager : BaseSingletonWithMono<GlobalManager>
    {
        private IEnumerator Start()
        {
            DontDestroyOnLoad(this);
            
            // 加载配置文件
            InputManager.Instance.LoadPlayerJsonToInputData();
            
            // 加载全局数据
            GlobalResource.Instance.LoadGlobalResource();

            // 注册全局事件
            GlobalEventRegister.Instance.Register();
            
            // 进入场景
            yield return null;
            CenterEvent.Instance.Raise(GlobalEventID.EnterScene);
        }
    }
}