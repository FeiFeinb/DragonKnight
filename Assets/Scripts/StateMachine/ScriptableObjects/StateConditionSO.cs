using System.Collections.Generic;
using UnityEngine;
namespace RPG.StateMachine
{
    public abstract class StateConditionSO : ScriptableObject
    {
        public StateCondition GetStateCondition(StateMachine stateMachine, Dictionary<ScriptableObject, object> createInstance, bool expectedResult)
        {
            // 查看是否已经存在缓存中
            if (createInstance.TryGetValue(this, out object obj))
            {
                return new StateCondition(stateMachine, obj as Condition, expectedResult);
            }
            // 缓存中不存在 新建Condition
            Condition condition = CreateCondition(this);
            condition.Init(stateMachine);
            // 储存在缓存中
            createInstance.Add(this, condition);
            // 利用Condition构建新的StateCondition
            StateCondition stateCondition = new StateCondition(stateMachine, condition, expectedResult);
            return stateCondition;
        }
        protected abstract Condition CreateCondition(StateConditionSO stateConditionSO);
    }
    public abstract class StateConditionSO<T> : StateConditionSO where T : Condition, new()
    {
        protected override Condition CreateCondition(StateConditionSO stateConditionSO)
        {
            Condition condition = new T();
            condition.originStateConditionSO = stateConditionSO;
            return condition;
        }
    }
}