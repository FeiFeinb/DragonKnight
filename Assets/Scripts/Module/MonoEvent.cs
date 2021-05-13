using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Module
{
    public class MonoEvent : BaseSingletonWithMono<MonoEvent>
    {
        private UnityAction updateEvent;
        private void Update()
        {
            if (updateEvent == null)
                return;
            updateEvent.Invoke();
        }
        public void AddUpdateEvent(UnityAction action) { updateEvent += action; }
        public void RemoveUpdateEvent(UnityAction action) { updateEvent -= action; }
    }
}