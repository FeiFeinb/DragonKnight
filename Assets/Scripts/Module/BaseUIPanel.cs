using RPG.UI;
using UnityEngine;

namespace RPG.Module
{
    public abstract class BaseUIPanel : MonoBehaviour, IUserInterfacePreInit
    {
        public virtual void PreInit() {}
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}