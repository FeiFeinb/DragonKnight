using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class BackGroundSizeFitter : MonoBehaviour
    {
        [SerializeField] private float widthMotify;             // 横向加值
        [SerializeField] private float heightMotify;            // 纵向加值
        [SerializeField] private RectTransform originRect;      // 本身坐标
        public void SetSize(RectTransform targetObj)
        {
            float targetWidth = targetObj.sizeDelta.x;
            float targetHeight = targetObj.sizeDelta.y;
            originRect.sizeDelta = new Vector2(targetWidth + widthMotify * 2, targetHeight + heightMotify * 2);
        }
    }
}