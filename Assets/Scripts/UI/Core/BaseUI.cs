using UnityEngine;
namespace RPG.UI
{
    public class BaseUI : MonoBehaviour
    {
        private Transform _trans;
        public virtual void Show()
        {
            if (!_trans)
            {
                _trans = gameObject.transform;
            }
            _trans.SetAsLastSibling();
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}