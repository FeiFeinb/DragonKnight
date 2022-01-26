using System;
using System.Collections.Generic;
using DG.Tweening;
using RPG.Module;
using UnityEngine;
namespace RPG.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        [DisplayOnly] public bool isFinish = true;

        protected Sequence _inSequence;
        
        /// <summary>
        /// UI进出栈所控制的Transform
        /// </summary>
        protected Transform _controlUITrans;

        /// <summary>
        /// UI进出栈所控制的GameObject
        /// </summary>
        protected GameObject _controlUIGameObj;

        /// <summary>
        /// 当UI开启显示时的回调
        /// </summary>
        protected Action _onUIShow;

        /// <summary>
        /// 当UI关闭显示时的回调 此时UI可能还处在UI栈中
        /// </summary>
        protected Action _onUIHide;
        
        private bool _hasDoTween;
        
        public static T GetController<T>() where T : BaseUI
        {
            return UIResourcesLoader.Instance.GetUserInterface<T>();
        }
        
        /// <summary>
        /// 获取UI当前是否存在或是否显示
        /// </summary>
        public virtual bool isActive
        {
            get
            {
                // 外部获取面板活跃状态
                return _controlUIGameObj && _controlUIGameObj.activeSelf;
            }
        }

        protected void SetUIActive(bool newIsActive)
        {
            if (newIsActive)
            {
                _controlUITrans.SetAsLastSibling();
            }
            _controlUIGameObj.SetActive(newIsActive);
        }
        
        public virtual void Show()
        {
            SetUIActive(true);
            if (_hasDoTween)
            {
                if (!isFinish) return;
                isFinish = false;
                _inSequence.PlayForward();
            }
            _onUIShow?.Invoke();
        }

        public virtual void Hide()
        {
            if (_hasDoTween)
            {
                if (!isFinish) return;
                isFinish = false;
                _inSequence.PlayBackwards();
            }
            else
            {
                SetUIActive(false);
            }
            _onUIHide?.Invoke();
        }

        protected virtual IEnumerable<BaseUIPanel> GetView()
        {
            return null;
        }

        public void Init()
        {
            bool isDefaultShow = InitControlObj();
            
            _inSequence = DOTween.Sequence();
            _inSequence.SetAutoKill(false);
            _inSequence.onComplete += () =>
            {
                isFinish = true;
            };
            // 逆向关闭UI的回调
            _inSequence.onRewind += () =>
            {
                isFinish = true;
                SetUIActive(false);
            };
            _hasDoTween = AchieveDoTweenSequence();
            
            var views = GetView();
            if (views != null)
            {
                foreach (var baseUIPanel in views)
                {
                    if (baseUIPanel != null)
                    {
                        baseUIPanel.PreInit();
                    }
                }
            }
            
            InitInstance();
            
            // 如果默认要求显示 则显示
            if (isDefaultShow)
            {
                Show();
            }
            // 如果不要求显示 则回到不显示的位置
            else
            {
                SetUIActive(false);
            }
        }
        
        /// <summary>
        /// 在每一次重新载入UI时调用
        /// </summary>
        protected virtual void InitInstance()
        {
            
        }
        
        /// <summary>
        /// 重写时不需要调用基类的方法 初始化UI动画
        /// </summary>
        /// <returns>是否处理UI动画</returns>
        protected virtual bool AchieveDoTweenSequence()
        {
            return false;
        }

        /// <summary>
        /// 初始化所控制的Transform和GameObject 返回值代表UI被加载时默认是否SetActive
        /// </summary>
        /// <returns>是否SetActive true</returns>
        protected virtual bool InitControlObj()
        {
            _controlUITrans = transform;
            _controlUIGameObj = gameObject;
            return false;
        }

        /// <summary>
        /// 当UI压入UI栈中的回调
        /// </summary>
        public virtual void OnUIPushIntoStack() {}
        
        /// <summary>
        /// 当UI弹出UI栈中的回调
        /// </summary>
        public virtual void OnUIPopFromStack() {}
    }
}