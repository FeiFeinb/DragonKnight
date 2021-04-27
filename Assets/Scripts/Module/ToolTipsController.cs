using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using RPG.UI;
namespace RPG.Module
{
    public abstract class ToolTipsController : BaseUIController
    {
        public enum ViewDirection
        {
            UpLeft,         // 左上角
            UpRight,        // 右上角
            DownLeft,       // 左下角
            DownRight       // 右下角
        }
        private RectTransform toolTipsRect;
        public override void PreInit()
        {
            toolTipsRect = gameObject.transform as RectTransform;
        }
        public override void Show()
        {
            // 与关闭状态重新开启时才需要UpdateToolTips
            if (ShouldUpdateToolTips())
            {
                UpdateToolTips(gameObject);
            }
            SetRectTransform();
            gameObject?.SetActive(true);
        }
        public override void Hide()
        {
            gameObject?.SetActive(false);
        }
        public virtual bool ShouldUpdateToolTips()
        {
            return !gameObject.activeSelf;
        }
        public abstract void UpdateToolTips(GameObject _toolTipsObj);

        private void SetRectTransform()
        {
            // 重新设置Pivot
            toolTipsRect.pivot = CalculatePivot(CaculateViewPos());
            // 获取鼠标位置完成位置变换
            toolTipsRect.position = Input.mousePosition;
        }

        private ViewDirection CaculateViewPos()
        {
            // 根据面板大小计算面板朝向
            Vector2 viewSize = toolTipsRect.sizeDelta;
            RectTransform canvanRect = UIResourcesManager.Instance.CanvasTrans as RectTransform;
            float newMousePositionX = Input.mousePosition.x / Screen.width * canvanRect.sizeDelta.x;
            float newMousePositionY = Input.mousePosition.y / Screen.height * canvanRect.sizeDelta.y;
            float horizontalRight = Screen.width - newMousePositionX;
            float verticalUp = Screen.height - newMousePositionY;
            // 左下角显示
            if (newMousePositionX > viewSize.x && newMousePositionY > viewSize.y)
            {
                return ViewDirection.DownLeft;
            }
            // 右下角显示
            else if (horizontalRight > viewSize.x && newMousePositionY > viewSize.x)
            {
                return ViewDirection.DownRight;
            }
            // 右上角显示
            else if (verticalUp > viewSize.x && horizontalRight > viewSize.y)
            {
                return ViewDirection.UpRight;
            }
            // 左上角显示
            else
            {
                return ViewDirection.UpLeft;
            }
        }
        private Vector2 CalculatePivot(ViewDirection viewDirection)
        {
            switch (viewDirection)
            {
                // 左上角
                case ViewDirection.UpLeft:
                    return new Vector2(1, 0);
                // 右上角
                case ViewDirection.UpRight:
                    return new Vector2(0, 0);
                // 左下角
                case ViewDirection.DownLeft:
                    return new Vector2(1, 1);
                // 右下角
                case ViewDirection.DownRight:
                    return new Vector2(0, 1);
                default:
                    return new Vector2();
            }
        }
    }
}