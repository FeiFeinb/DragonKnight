using System;
using System.Collections;
using System.Collections.Generic;
using RPG.InputSystyem;
using RPG.UI;
using UnityEngine;

namespace RPG.Module
{
    public class GlobalUIManager : BaseSingletonWithMono<GlobalUIManager>
    {
        public bool isforceDontClose = false;
        public bool isPlayingAnimation = false;
        public bool isPausing = false;
        public int UICount => _baseUIStack.Count;
        [Tooltip("不同UI间切换的等待时间"), SerializeField] private float waitSeconds = 0.1f;
        
        private readonly Stack<BaseUIController> _baseUIStack = new Stack<BaseUIController>();

        public void OpenUI(BaseUIController nextUI)
        {
            BaseUIController topUI = GetTopUIController();
            // 获取栈顶UI 执行退出操作
            if (topUI && topUI == nextUI)
                throw new Exception("UI重复操作");
            if (isPlayingAnimation)
                throw new Exception("请勿用脸滚键盘");
            // 在Hide动画结束后播放Show
            StartCoroutine(HideAsync(topUI, nextUI));
            // 新UI播放入场动画 压入栈顶
            _baseUIStack.Push(nextUI);
            
            if (_baseUIStack.Count == 1 && nextUI == PauseController.controller)
            {
                // 暂停所有键位输入
                isPausing = true;
                InputManager.Instance.inputData.CloseAllKeyInput(0);
            }
        }

        public void CloseUI(BaseUIController closeUI = null)
        {
            BaseUIController topUI = GetTopUIController();
            if (!topUI || (closeUI && closeUI != topUI))
                throw new Exception("栈顶UI不匹配");
            if (isPlayingAnimation)
                throw new Exception("请勿用脸滚键盘");
            
            _baseUIStack.Pop();
            BaseUIController nextUI = GetTopUIController();
            StartCoroutine(HideAsync(topUI, nextUI));
            
            if (_baseUIStack.Count == 0 && topUI == PauseController.controller)
            {
                // 恢复所有输入
                isPausing = false;
                InputManager.Instance.inputData.OpenAllKeyInput(0);
            }
        }

        private BaseUIController GetTopUIController()
        {
            return _baseUIStack.Count > 0 ? _baseUIStack.Peek() : null;
        }

        private BaseUIController GetSecondUIController()
        {
            if (_baseUIStack.Count > 1)
            {
                var cache = _baseUIStack.Pop();
                var result = GetTopUIController();
                _baseUIStack.Push(cache);
                return result;
            }
            return null;
        }
        private IEnumerator HideAsync(BaseUIController preUI, BaseUIController nextUI)
        {
            if (preUI)
            {
                isPlayingAnimation = true;
                preUI.Hide();
                while (!preUI.isFinish)
                {
                    yield return null;
                }

                isPlayingAnimation = false;
            }

            if (preUI && nextUI)
            {
                isPlayingAnimation = true;
                yield return new WaitForSeconds(waitSeconds);
            }
            
            if (nextUI)
            {
                isPlayingAnimation = true;
                nextUI.Show();
                while (!nextUI.isFinish)
                {
                    yield return null;
                }
                isPlayingAnimation = false;
            }
        }
    }
}