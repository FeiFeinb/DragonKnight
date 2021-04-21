using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.StateMachine
{
    [System.Serializable]
    public class State
    {
        [SerializeField, DisplayOnly] private StateSO originStateSO;    // 状态对应的ScriptableObject
        private StateMachine stateMachine;                              // 状态所处状态机
        private StateTransition[] transitions;                          // 转换关系数组
        private StateAction[] actions;                                  // 状态执行所调动的方法数组

        public State(StateSO _originStateSO, StateMachine _stateMachine)
        {
            originStateSO = _originStateSO;
            stateMachine = _stateMachine;
        }
        public void SetStateAction(StateAction[] _actions)
        {
            actions = _actions;
        }
        public void SetStateTransition(StateTransition[] _transitions)
        {
            transitions = _transitions;
        }
        public State TryTransition()
        {
            // 询问是否需要进行状态转换
            foreach (StateTransition stateTransition in transitions)
            {
                State tempState = stateTransition.TryTransition();
                if (tempState != null)
                {
                    return tempState;
                }
            }
            return null;
        }
        public void OnStateEnter()
        {
            void OnStateEnter(IStateComponent[] components)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    components[i].OnStateEnter();
                }
            }
            OnStateEnter(transitions);
            OnStateEnter(actions);
        }
        public void OnUpdate()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].OnUpdate();
            }
        }
        public void OnStateExit()
        {
            void OnStateExit(IStateComponent[] components)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    components[i].OnStateExit();
                }
            }
            OnStateExit(transitions);
            OnStateExit(actions);
        }
    }
}
