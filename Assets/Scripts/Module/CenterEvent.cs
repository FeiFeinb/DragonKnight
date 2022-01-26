using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Module
{
    interface IEventInfo { }
    public class EventInfo<T> : IEventInfo
    {
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
        public UnityAction<T> actions;
    }
    public class EventInfo : IEventInfo
    {
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
        public UnityAction actions;
    }
    public class CenterEvent : BaseSingletonWithMono<CenterEvent>
    {
        //储存所有事件
        private Dictionary<GlobalEventID, IEventInfo> eventDir = new Dictionary<GlobalEventID, IEventInfo>();

        public void AddListener<T>(GlobalEventID eventName, UnityAction<T> action)
        {
            //有参版本
            if (eventDir.ContainsKey(eventName))
                (eventDir[eventName] as EventInfo<T>).actions += action;
            else
                eventDir.Add(eventName, new EventInfo<T>(action));
        }
        public void AddListener(GlobalEventID eventName, UnityAction action)
        {
            //无参版本
            if (eventDir.ContainsKey(eventName))
                (eventDir[eventName] as EventInfo).actions += action;
            else
                eventDir.Add(eventName, new EventInfo(action));
        }
        //RemoveListener一般在OnDestroy中调用
        public void RemoveListener<T>(GlobalEventID eventName, UnityAction<T> action)
        {
            //有参版本
            if (eventDir.ContainsKey(eventName))
                (eventDir[eventName] as EventInfo<T>).actions -= action;
        }
        public void RemoveListener(GlobalEventID eventName, UnityAction action)
        {
            //无参版本
            if (eventDir.ContainsKey(eventName))
                (eventDir[eventName] as EventInfo).actions -= action;
        }

        public void Raise<T>(GlobalEventID eventName, T info)
        {
            //如果存在，则依次调用所有事件
            if (eventDir.ContainsKey(eventName))
                (eventDir[eventName] as EventInfo<T>).actions?.Invoke(info);
        }
        public void Raise(GlobalEventID eventName)
        {
            //如果存在，则依次调用所有事件
            if (eventDir.ContainsKey(eventName))
                (eventDir[eventName] as EventInfo).actions?.Invoke();
        }

        public void Clear()
        {
            //跳转场景前手动调用清除不必要的内存占用
            eventDir.Clear();
        }
    }
}