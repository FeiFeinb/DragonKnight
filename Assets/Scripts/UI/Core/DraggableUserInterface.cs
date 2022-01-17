using UnityEngine;
using UnityEngine.EventSystems;
namespace RPG.UI
{
    public class DraggableUserInterface : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform targetTrans;

        private Vector2 originOffset;
        public void OnDrag(PointerEventData eventData)
        {
            targetTrans.position = eventData.position - originOffset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // 记录位置偏移
            originOffset = eventData.position - (Vector2)targetTrans.position;
            // 将拖动目标的UI设为最顶层
            targetTrans.SetAsLastSibling();
        }
    }
}