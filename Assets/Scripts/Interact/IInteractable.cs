using RPG.Interact;
using UnityEngine;

namespace RPG.Interact
{
    public interface IInteractable
    {
        void GetInteractInfo(out InteractType type, out string buttonStr, out Sprite sprite);
        
        void OnInteractButtonClick();
    }
}