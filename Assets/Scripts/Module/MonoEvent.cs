using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Module
{
    public class MonoEvent : BaseSingletonWithMono<MonoEvent>
    {
        private UnityAction UpdateEvent;
        private void Update()
        {
            if (UpdateEvent == null)
                return;
            UpdateEvent.Invoke();
        }
        public void AddUpdateEvent(UnityAction _action) { UpdateEvent += _action; }
        public void RemoveUpdateEvent(UnityAction _action) { UpdateEvent -= _action; }
    }
}