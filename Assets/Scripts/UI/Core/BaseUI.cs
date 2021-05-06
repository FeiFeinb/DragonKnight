using UnityEngine;
namespace RPG.UI
{
    public class BaseUI : MonoBehaviour
    {
        public virtual void Show()
        {
            transform?.SetAsLastSibling();
            gameObject?.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject?.SetActive(false);
        }
    }
}