using System;
using System.Collections.Generic;
using RPG.Module;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interact
{
    public class InteractionView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        public Transform Container => _container;
        
        public void ClearButton(bool isDestroyUnActive)
        {
            _container.DestroyChildren(isDestroyUnActive);
        }
    }
}