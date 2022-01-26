using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Module
{
    public class ResourcesLoader : BaseSingletonWithMono<ResourcesLoader>
    {
        private Dictionary<string, UnityEngine.Object> cacheDic = new Dictionary<string, UnityEngine.Object>();
        
        /// <summary>
        /// 同步加载资源 不会实例化
        /// </summary>
        /// <param name="resPath">资源路径</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>加载完毕的资源</returns>
        public T Load<T>(string resPath) where T : UnityEngine.Object
        {
            return LoadResource<T>(resPath, true, false, null);
        }
        
        /// <summary>
        /// 同步加载GameObject资源 并实例化
        /// </summary>
        /// <param name="resPath">资源路径</param>
        /// <param name="parents">父节点</param>
        /// <returns>加载完毕的GameObject</returns>
        public GameObject LoadAndInstantiate(string resPath, Transform parents)
        {
            return LoadResource<GameObject>(resPath, true, true, parents);
        }
        
        
        
        /// <summary>
        /// 异步加载资源 不会实例化
        /// </summary>
        /// <param name="resPath">资源路径</param>
        /// <param name="callBack">加载完毕回调</param>
        /// <typeparam name="T">资源类型</typeparam>
        public void LoadAsync<T>(string resPath, Action<T> callBack) where T : UnityEngine.Object
        {
            StartCoroutine(LoadResourceAsync<T>(resPath, callBack, true, false, null));
        }
        
        /// <summary>
        /// 异步加载资源 并实例化
        /// </summary>
        /// <param name="resPath">资源路径</param>
        /// <param name="callBack">加载完毕回调</param>
        /// <param name="parents">父节点</param>
        /// <typeparam name="T">资源类型</typeparam>
        public void LoadAsyncAndInstantiate<T>(string resPath, Action<T> callBack, Transform parents) where T : UnityEngine.Object
        {
            StartCoroutine(LoadResourceAsync<T>(resPath, callBack, true, true, parents));
        }
        
        private IEnumerator LoadResourceAsync<T>(string resPath, Action<T> callBack, bool isCache, bool isInstantiate, Transform parents) where T : UnityEngine.Object
        {
            T tempRes = null;
            // 缓存中包含
            if (cacheDic.ContainsKey(resPath))
            {
                tempRes = cacheDic[resPath] as T;
            }
            // 缓存中不包含 需重新加载
            else
            {
                ResourceRequest resRequest = Resources.LoadAsync<T>(resPath);
                // 等待资源加载完成
                while (!resRequest.isDone)
                {
                    yield return null;
                }
                // 资源加载完成
                tempRes = resRequest.asset as T;
                if (isCache)
                {
                    cacheDic.Add(resPath, tempRes);
                }
            }
            if (tempRes == null)
            {
                Debug.LogError("LoadAsync Failed");
            }
            // 如果资源是GameObject 则创建一个实例
            if (tempRes is GameObject && isInstantiate)
            {
                callBack(GameObject.Instantiate(tempRes, parents));
            }
            else
            {
                callBack(tempRes);
            }
        }
        
        private T LoadResource<T>(string resPath, bool isCached, bool isInstantiate, Transform parents) where T : UnityEngine.Object
        {
            T tempRes = null;
            // 缓存中包含
            if (cacheDic.ContainsKey(resPath))
            {
                tempRes = cacheDic[resPath] as T;
            }
            // 缓存中不包含 需重新加载
            else
            {
                tempRes = Resources.Load<T>(resPath);
                if (isCached)
                {
                    cacheDic.Add(resPath, tempRes);
                }
            }
            // 判空
            if (tempRes == null)
            {
                Debug.LogError("Load Failed!");
            }
            // 如果资源是GameObject 则创建一个实例
            return (tempRes is GameObject && isInstantiate) ? GameObject.Instantiate(tempRes, parents) : tempRes;
        }

    }
}