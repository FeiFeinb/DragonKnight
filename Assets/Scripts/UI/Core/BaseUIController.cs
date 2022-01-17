using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace RPG.UI
{
    public abstract class BaseUIController : BaseUI, IUserInterfacePreInit
    {
        public bool isFinish = true;
        protected Sequence inSequence;

        private bool hasDoTween;
        public virtual bool isActive
        {
            get
            {
                // 外部获取面板活跃状态
                GameObject obj = gameObject;
                return obj && obj.activeSelf;
            }
        }

        public override void Show()
        {
            base.Show();
            if (hasDoTween)
            {
                if (!isFinish) return;
                isFinish = false;
                inSequence.PlayForward();
            }
        }

        public override void Hide()
        {
            if (hasDoTween)
            {
                if (!isFinish) return;
                isFinish = false;
                inSequence.PlayBackwards();
            }
            else
            {
                base.Hide();
            }
        }

        /// <summary>
        /// 在每一次重新载入UI时调用
        /// </summary>
        public virtual void PreInit()
        {
            inSequence = DOTween.Sequence();
            inSequence.SetAutoKill(false);
            inSequence.onComplete += () =>
            {
                isFinish = true;
            };
            // 逆向关闭UI的回调
            inSequence.onRewind += () =>
            {
                isFinish = true;
                base.Hide();
            };
            hasDoTween = AchieveDoTweenSequence();
        }

        protected virtual bool AchieveDoTweenSequence()
        {
            return false;
        }
    }
}