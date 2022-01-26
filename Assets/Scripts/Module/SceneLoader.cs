using System;
using System.Collections;
using RPG.SaveSystem;
using RPG.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG.Module
{
    public class SceneLoader : BaseSingletonWithMono<SceneLoader>
    {
        public bool isLoading => _isLoading;
        
        private bool _isLoading = false;

        public void Load(string sceneName)
        {
            if (_isLoading)
                return;
            // 启动协程加载场景
            var coroutine = StartCoroutine(LoadSceneAsync(sceneName));
            _isLoading = coroutine != null;
        }
        
        
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            _isLoading = true;

            
            // 加载场景的UI并不属于UI栈中的内容 因此不需要从全局UI栈中读取
            var loadingUI = BaseUI.GetController<SceneLoadingController>();
            loadingUI.Show();
            
            // 等待UI动画展示完毕
            while (!loadingUI.isFinish)
            {
                yield return null;
            }
            
            // 开始加载场景
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            
            // 执行退出当前场景回调 等操作
            CenterEvent.Instance.Raise(GlobalEventID.ExitScene);
            
            WaitForSeconds waitTime = new WaitForSeconds(0.1f);
            while (!operation.isDone)
            {
                // 同步进度条
                loadingUI.SetLoadingProgress(operation.progress);
                if (operation.progress >= 0.9f)
                {
                    // 设置完成进度条
                    loadingUI.SetLoadingProgress(1);
                    // 允许场景加载完执行跳转
                    operation.allowSceneActivation = true;
                }

                yield return waitTime;
            }
            
            // 等待UI动画展示完毕
            while (!loadingUI.IsFinishLoadingAnimation())
            {
                yield return null;
            }
            loadingUI.Hide();
            // TODO: 执行进入新场景的回调
            CenterEvent.Instance.Raise(GlobalEventID.EnterScene);
            _isLoading = false;
        }
    }
}