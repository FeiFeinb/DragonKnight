using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.TradeSystem
{
    public class UIPageFlip : MonoBehaviour
    {
        public int PageValue => pageValue;
        public int MaxPage => maxPage;
        [SerializeField] private Button pageUpButtom;
        [SerializeField] private Button pageDownButton;

        private Action onPageUp;
        private Action onPageDown;

        private Action onFlip;


        [SerializeField] private int pageValue;
        [SerializeField] private int maxPage;

        public void Start()
        {
            pageUpButtom.onClick.AddListener(delegate
            {
                pageValue--;
                onPageUp?.Invoke();
                FlipCheck();
            });
            pageDownButton.onClick.AddListener(delegate
            {
                pageValue++;
                onPageDown?.Invoke();
                FlipCheck();
            });
        }
        public void Reset(int _maxPage)
        {
            pageValue = 1;
            maxPage = _maxPage;
            FlipCheck();
        }

        private void FlipCheck()
        {
            pageUpButtom.transform.parent.parent.gameObject.SetActive(pageValue != 1);
            pageDownButton.transform.parent.parent.gameObject.SetActive(pageValue != maxPage);
        }
        public void AddPageUpListener(Action action)
        {
            onPageUp += action;
        }

        public void RemovePageUpListener(Action action)
        {
            onPageUp -= action;
        }

        public void AddPageDownListener(Action action)
        {
            onPageDown += action;
        }

        public void RemovePageDownListener(Action action)
        {
            onPageDown -= action;
        }
    }
}