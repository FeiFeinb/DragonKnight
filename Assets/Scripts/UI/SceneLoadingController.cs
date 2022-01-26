using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SceneLoadingController : BaseUI
    {
        [SerializeField] private Slider _loadProgress;
        [SerializeField] private CanvasGroup _canvasGroup;

        private TweenerCore<float, float, FloatOptions> core;

        public override void Show()
        {
            base.Show();
            // 开启开启前重置进度条
            _loadProgress.value = 0;
        }

        public override void Hide()
        {
            base.Hide();
            if (core.IsActive())
            {
                core.Kill();
            }
        }

        public void SetLoadingProgress(float progress)
        {
            if (core.IsActive())
            {
                core.Kill();
            }
            core = DOTween.To(() => _loadProgress.value, x => _loadProgress.value = x, progress, 0.5f);
            core.SetAutoKill(true);
            core.Play();
        }


        public bool IsFinishLoadingAnimation()
        {
            return Mathf.Abs(_loadProgress.value - 1) <= 0.01;
        }
        
        protected override bool AchieveDoTweenSequence()
        {
            _inSequence.Append(_canvasGroup.DOFade(1, 0.3f));
            return true;
        }
    }
}
