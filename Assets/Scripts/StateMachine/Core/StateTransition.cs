using UnityEngine;
namespace RPG.StateMachine
{
    public class StateTransition : IStateComponent
    {
        private State targetState;                  // 目标状态
        private StateCondition[] conditions;        // 条件数组
        public StateTransition(State _targetState, StateCondition[] _conditions)
        {
            Init(_targetState, _conditions);
        }
        public void Init(State _targetState, StateCondition[] _conditions)
        {
            targetState = _targetState;
            conditions = _conditions;
        }
        public void OnStateEnter()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].condition.OnStateEnter();
            }
        }

        public void OnStateExit()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].condition.OnStateExit();
            }
        }
        public State TryTransition()
        {
            // StateCondition中的所有条件都满足 才返回true
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].IsMatching())
                {
                    return null;
                }
            }
            return targetState;
        }
    }
}