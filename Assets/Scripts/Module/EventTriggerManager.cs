using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using RPG.Utility;
namespace RPG.Module
{
    public class EventTriggerManager : BaseSingletonWithMono<EventTriggerManager>
    {
        public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            // 获取EventTrigger
            EventTrigger trigger = obj.GetOrAddComponent<EventTrigger>();
            // 新建监听
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            // 添加监听
            trigger.triggers.Add(eventTrigger);
        }
    }

}

