using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Config;
using UnityEngine;
using RPG.UI;

namespace RPG.Module
{
    public enum CanvasType
    {
        MainCanvas,
        LastingCanvas
    }


    public class UIResourcesLoader : BaseSingletonWithMono<UIResourcesLoader>
    {
        public Transform mainCanvasTrans;

        public Transform lastingCanvasTrans;

        private Dictionary<string, BaseUI> _cachedDic = new Dictionary<string, BaseUI>();

        public void SeekOrSetMainCanvas(Transform canvasTrans = null)
        {
            if (canvasTrans == null)
            {
                mainCanvasTrans = GameObject.Find("Canvas").transform;
                if (mainCanvasTrans == null)
                {
                    Debug.LogError("Cant Find Canvas In Scene");
                }
            }
            else
            {
                mainCanvasTrans = canvasTrans;
            }
        }

        public T GetUserInterface<T>() where T : BaseUI
        {
            string uiTypeName = typeof(T).Name;
            if (_cachedDic.ContainsKey(uiTypeName))
            {
                return _cachedDic[uiTypeName] as T;
            }

            throw new NullReferenceException($"Cant Get {uiTypeName} in UICacheDictionary");
        }

        public GameObject LoadUserInterface<T>(string storePath, CanvasType canvasType)
        {
            string className = typeof(T).Name;
            if (_cachedDic.ContainsKey(className))
            {
                return _cachedDic[className].gameObject;
            }

            // 加载UI 并自动创建至场景中
            GameObject tempUIRes = ResourcesLoader.Instance.LoadAndInstantiate(storePath,
                canvasType == CanvasType.MainCanvas ? mainCanvasTrans : lastingCanvasTrans);
            // 属性更改
            tempUIRes.name = storePath.Substring(storePath.LastIndexOf("/") + 1);
            // 完成UI的初始化
            tempUIRes.TryGetComponent(out BaseUI baseUI);
            if (baseUI != null)
            {
                _cachedDic.Add(className, baseUI);
                baseUI.Init();
            }
            return tempUIRes;
        }

        public GameObject LoadUserInterface(UILoadConfig loadConfig)
        {
            if (_cachedDic.ContainsKey(loadConfig.className))
                return _cachedDic[loadConfig.className].gameObject;
            
            Transform parent = loadConfig.canvasType == CanvasType.MainCanvas ? mainCanvasTrans : lastingCanvasTrans;
            GameObject tempUIRes = GameObject.Instantiate(loadConfig.loadPrefab, parent);
            // 属性更改
            tempUIRes.name = loadConfig.loadPrefab.name;
            // 完成UI的初始化
            tempUIRes.TryGetComponent(out BaseUI baseUI);
            if (baseUI != null)
            {
                _cachedDic.Add(loadConfig.className, baseUI);
                baseUI.Init();
            }
            return tempUIRes;
        }

        /// <summary>
        /// 自动加载UI至给定的父节点下 并调用PreInit
        /// </summary>
        /// <param name="loadPrefab">UI预制体</param>
        /// <param name="parents">加载完成的GameObject</param>
        /// <returns></returns>
        public GameObject InstantiateUserInterface(GameObject loadPrefab, Transform parents = null)
        {
            // 克隆物件 并自动创建至场景中
            GameObject tempUIRes = GameObject.Instantiate(loadPrefab, parents == null ? mainCanvasTrans : parents);
            // 属性更改
            tempUIRes.name = loadPrefab.name;
            // 完成UI的初始化
            tempUIRes.TryGetComponent<IUserInterfacePreInit>(out IUserInterfacePreInit iPreInit);
            iPreInit?.PreInit();
            return tempUIRes;
        }

        public void Clear()
        {
            _cachedDic.Clear();
        }
    }
}