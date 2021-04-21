using System;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.StateMachine
{
    public abstract class StateActionSO : ScriptableObject
    {
        public StateAction GetStateAction(StateMachine stateMachine, Dictionary<ScriptableObject, object> createInstance)
        {
            // 查看是否已经存在缓存中
            if (createInstance.TryGetValue(this, out object obj))
            {
                return obj as StateAction;
            }
            // 缓存中不存在 新建StateAction
            StateAction stateAction = CreateStateAction(this);
            stateAction.Init(stateMachine);
            // 储存在缓存中
            createInstance.Add(this, stateAction);
            return stateAction;
        }
        protected abstract StateAction CreateStateAction(StateActionSO stateActionSO);
    }
    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction CreateStateAction(StateActionSO stateActionSO)
        {
            StateAction stateAction = new T();
            stateAction.originStateActionSO = stateActionSO;
            return stateAction;
        }
    }
}