using System;
using System.Collections;
using System.Collections.Generic;
using RPG.UI;
using UnityEngine;

namespace RPG.Module
{
    public class GlobalUIManager : BaseSingletonWithMono<GlobalUIManager>
    {
        [Tooltip("不同UI间切换的等待时间"), SerializeField] private float waitSeconds = 0.1f;
        
        private readonly Stack<BaseUIController> _baseUIStack = new Stack<BaseUIController>();

        public void OpenUI(BaseUIController nextUI)
        {
            BaseUIController topUI = GetTopUIController();
            // 获取栈顶UI 执行退出操作
            if (topUI && topUI == nextUI)
                throw new Exception("UI重复操作");
            // 在Hide动画结束后播放Show
            StartCoroutine(HideAsync(topUI, nextUI));
            // 新UI播放入场动画 压入栈顶
            _baseUIStack.Push(nextUI);
        }

        public void CloseUI(BaseUIController closeUI = null)
        {
            BaseUIController topUI = GetTopUIController();
            if (closeUI && closeUI != topUI)
                throw new Exception("栈顶UI不匹配");
            _baseUIStack.Pop();
            BaseUIController nextUI = GetTopUIController();
            StartCoroutine(HideAsync(topUI, nextUI));
        }

        private BaseUIController GetTopUIController()
        {
            return _baseUIStack.Count > 0 ? _baseUIStack.Peek() : null;
        }

        private IEnumerator HideAsync(BaseUIController preUI, BaseUIController nextUI)
        {
            if (preUI)
            {
                preUI.Hide();
                while (!preUI.isFinish)
                {
                    yield return null;
                }
            }

            if (preUI && nextUI)
            {
                yield return new WaitForSeconds(waitSeconds);
            }
            
            if (nextUI)
            {
                nextUI.Show();
            }
        }
    }
}