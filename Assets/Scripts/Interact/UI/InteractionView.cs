using System;
using System.Collections.Generic;
using RPG.Module;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inertact
{
    public class InteractionView : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        public GameObject Container => _container;
        
        public void ClearButton()
        {
            _container.transform.DestroyChildren();
        }
    }
}