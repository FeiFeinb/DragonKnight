using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
namespace RPG.Module
{
    public class UIResourcesManager : BaseSingletonWithMono<UIResourcesManager>
    {
        public Transform CanvasTrans => canvasTrans;    // 外部获取
        private Transform canvasTrans;                  // 场景中Canvas
        private void Awake()
        {
            canvasTrans = GameObject.FindObjectOfType<Canvas>().transform;
        }
        /// <summary>
        /// 自动加载UI至场景Canvas下 并调用PreInit
        /// </summary>
        /// <param name="storeInfo">UI存储信息</param>
        /// <returns>加载完成的GameObject</returns>
        public GameObject LoadUserInterface(UIStoreInfo storeInfo)
        {
            // 加载UI 并自动创建至场景中
            GameObject tempUIRes = ResourcesManager.Instance.LoadAndInstantiate(storeInfo.prefabPath, canvasTrans);
            // 属性更改
            tempUIRes.name = storeInfo.prefabName;
            // 完成UI的初始化
            tempUIRes.TryGetComponent<IUserInterfacePreInit>(out IUserInterfacePreInit iPreInit);
            iPreInit?.PreInit();
            return tempUIRes;
        }

        /// <summary>
        /// 自动加载UI至场景Canvas下 并调用PreInit
        /// </summary>
        /// <param name="loadPrefab">UI预制体</param>
        /// <returns>加载完成的GameObject</returns>
        public GameObject LoadUserInterface(GameObject loadPrefab)
        {
            // 克隆物件 并自动创建至场景中
            GameObject tempUIRes = GameObject.Instantiate(loadPrefab, canvasTrans);
            // 属性更改
            tempUIRes.name = loadPrefab.name;
            // 完成UI的初始化
            tempUIRes.TryGetComponent<IUserInterfacePreInit>(out IUserInterfacePreInit iPreInit);
            iPreInit?.PreInit();
            return tempUIRes;
        }
        
        /// <summary>
        /// 自动加载UI至给定的父节点下 并调用PreInit
        /// </summary>
        /// <param name="loadPrefab">UI预制体</param>
        /// <param name="parents">加载完成的GameObject</param>
        /// <returns></returns>
        public GameObject LoadUserInterface(GameObject loadPrefab, Transform parents)
        {
            // 克隆物件 并自动创建至场景中
            GameObject tempUIRes = GameObject.Instantiate(loadPrefab, parents);
            // 属性更改
            tempUIRes.name = loadPrefab.name;
            // 完成UI的初始化
            tempUIRes.TryGetComponent<IUserInterfacePreInit>(out IUserInterfacePreInit iPreInit);
            iPreInit?.PreInit();
            return tempUIRes;
        }
    }
}


