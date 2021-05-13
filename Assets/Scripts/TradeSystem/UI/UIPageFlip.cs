using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.TradeSystem
{
    public class UIPageFlip : MonoBehaviour
    {
        public int PageValue => pageValue;                  // 外部获取
        public int MaxPage => maxPage;                      // 外部获取
        
        [SerializeField] private Button pageUpButtom;       // 上一页按钮
        [SerializeField] private GameObject pageUpObj;      // 上一页按钮物件
        [SerializeField] private Button pageDownButton;     // 下一页按钮
        [SerializeField] private GameObject pageDownObj;    // 下一页按钮
        [SerializeField] private Text pageText;             // 页数文字UI
        
        private Action onPageUp;        // 上一页回调
        private Action onPageDown;      // 下一页回调
        private int pageValue;          // 当前页数
        private int maxPage;            // 总页数

        public void Start()
        {
            // 初始化 创建监听
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
            // 重新设置翻页组件
            pageValue = 1;
            maxPage = _maxPage;
            FlipCheck();
        }

        private void FlipCheck()
        {
            // 设定上下页按钮的可视性
            pageUpObj.SetActive(pageValue != 1);
            pageDownObj.SetActive(pageValue != maxPage);
            pageText.text = $"第{pageValue}页";
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