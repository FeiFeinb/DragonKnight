using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.StateMachine
{
    [System.Serializable]
    public class TransitionItem
    {
        public StateSO fromState;               // 从哪来
        public StateSO toState;                 // 到哪去
        public ConditionPair[] conditions;      // 条件对
    }
    [System.Serializable]
    public class ConditionPair
    {
        public bool expectedResult;           // 期望结果
        public StateConditionSO condition;      // 条件
    }
    [CreateAssetMenu(fileName = "New TransitionTable", menuName = "StateMachine/TransitionTable")]
    public class TransitionTableSO : ScriptableObject
    {
        public State rootState => cachedState[0];                   // 获取根节点
        [SerializeField] private TransitionItem[] transitionItems;  // 状态及其转换
        [SerializeField] private List<State> cachedState;                             // 缓存初始化完毕的状态数列
        public void InitTransitionTable(StateMachine stateMachine)
        {
            // 缓存数列与缓存字典
            cachedState = new List<State>();
            Dictionary<ScriptableObject, object> createInstance = new Dictionary<ScriptableObject, object>();
            // 以fromState状态为根据 对TransitionItem数组进行排序
            var fromStateGroups = transitionItems.GroupBy((transition) => transition.fromState);
            foreach (var fromStateGroup in fromStateGroups)
            {
                State fromState = fromStateGroup.Key.GetState(stateMachine, createInstance);
                // 初始化State中的StateAction[]
                fromState.SetStateAction(fromStateGroup.Key.GetActions(stateMachine, createInstance));
                // 初始化State中的StateTransition[]
                fromState.SetStateTransition(GetStateTransition(stateMachine, createInstance, fromStateGroup.ToArray()));
                // 添加至缓存数列
                cachedState.Add(fromState);
            }
        }
        private StateTransition[] GetStateTransition(StateMachine stateMachine, Dictionary<ScriptableObject, object> createInstance, TransitionItem[] transitionItems)
        {
            StateCondition[] ProgressCondition(StateMachine stateMachine, Dictionary<ScriptableObject, object> createInstance, ConditionPair[] conditionPairs)
            {
                List<StateCondition> stateConditions = new List<StateCondition>();
                // 遍历条件对 构建StateCondition数列
                foreach (ConditionPair conditionPair in conditionPairs)
                {
                    stateConditions.Add(conditionPair.condition.GetStateCondition(stateMachine, createInstance, conditionPair.expectedResult));
                }
                return stateConditions.ToArray();
            }

            List<StateTransition> transitions = new List<StateTransition>();
            foreach (TransitionItem transitionItem in transitionItems)
            {
                // TODO: 处理状态机中只有一个状态的情况
                // 遍历存在此fromState的TransitionItem 创建toState
                State toState = transitionItem.toState.GetState(stateMachine, createInstance);
                // 根据StateCondition和toState构建StateTransition
                transitions.Add(new StateTransition(toState, ProgressCondition(stateMachine, createInstance, transitionItem.conditions)));
            }
            return transitions.ToArray();
        }
    }
}
