using System;
using System.Collections;
using System.Collections.Generic;
using RPG.InputSystyem;
using RPG.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Module
{
    public class UIStackManager : BaseSingletonWithMono<UIStackManager>
    {
        public bool isPlayingAnimation = false;
        
        public int UICount => _baseUIStack.Count;
        
        private Stack<Tuple<BaseUI, bool>> _baseUIStack = new Stack<Tuple<BaseUI, bool>>();

        public void TriggerUI(BaseUI nextUI, bool isHidePreUI = false)
        {
            // 如果当前UI在栈顶 则关闭该UI
            Tuple<BaseUI, bool> topUI = GetTopUIController();
            if (topUI != null && topUI.Item1 == nextUI)
            {
                PopUI();
            }
            // 如果当前UI不在栈顶 则开启该UI
            else if (topUI == null || topUI.Item1 != nextUI)
            {
                PushUI(nextUI, isHidePreUI);
            }
        }

        public void PushUI(BaseUI openUI, bool isHidePreUI = false)
        {
            Tuple<BaseUI, bool> topUI = GetTopUIController();
            // 避免UI重复入栈
            if (isPlayingAnimation)
                return;
            // 如果栈中有元素 则 栈顶元素不能是新入栈的UI
            if (topUI != null && openUI == topUI.Item1)
                return;
            StartCoroutine(PushUIAsync(isHidePreUI ? topUI?.Item1 : null, openUI));
            _baseUIStack.Push(new Tuple<BaseUI, bool>(openUI, isHidePreUI));
        }

        public void PopUI()
        {
            Tuple<BaseUI, bool> topUI = GetTopUIController();
            // 检查当前是否正在播放UI 或 栈是否为空
            if (isPlayingAnimation || topUI == null)
                return;
            _baseUIStack.Pop();
            Tuple<BaseUI, bool> newTopUI = GetTopUIController();
            StartCoroutine(PopUIAsync(topUI.Item1, topUI.Item2 ? newTopUI?.Item1 : null));
        }
        
        private IEnumerator PushUIAsync(BaseUI exitUI, BaseUI enterUI)
        {
            if (exitUI)
            {
                isPlayingAnimation = true;
                exitUI.Hide();
                while (!exitUI.isFinish)
                {
                    yield return null;
                }
                isPlayingAnimation = false;
            }
        
            isPlayingAnimation = true;
            enterUI.OnUIPushIntoStack();
            enterUI.Show();
            while (!enterUI.isFinish)
            {
                yield return null;
            }
            isPlayingAnimation = false;
        }
        
        private IEnumerator PopUIAsync(BaseUI poppedUI, BaseUI newTopUI)
        {
            isPlayingAnimation = true;
            poppedUI.Hide();
            while (!poppedUI.isFinish)
            {
                yield return null;
            }
            poppedUI.OnUIPopFromStack();
            isPlayingAnimation = false;
            
            if (newTopUI)
            {
                isPlayingAnimation = true;
                newTopUI.Show();
                while (!newTopUI.isFinish)
                {
                    yield return null;
                }
                isPlayingAnimation = false;
            }
        }

        public void Clear()
        {
            // 清空UI栈中的数据 并执行相应UI的回调 不会执行UI的动画
            int stKSize = _baseUIStack.Count;
            for (int i = 0; i < stKSize; i++)
            {
                BaseUI currentTopUI = _baseUIStack.Peek().Item1;
                _baseUIStack.Pop();
                currentTopUI.OnUIPopFromStack();
                Destroy(currentTopUI.gameObject);
            }
        }
        
        private Tuple<BaseUI, bool> GetTopUIController()
        {
            return _baseUIStack.Count > 0 ? _baseUIStack.Peek() : null;
        }
    }
}