using UnityEngine;
namespace RPG.StateMachine
{
    public abstract class Condition : IStateComponent
    {
        public StateConditionSO originStateConditionSO;             // 源StateConditionSO

        /// <summary>
        /// 状态切换条件判定
        /// </summary>
        /// <returns>是否切换</returns>
        public abstract bool Statement();

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="stateMachine">当前实体所挂在StateMachine</param>
        public virtual void Init(StateMachine stateMachine) {}

        public virtual void OnStateEnter() {}

        public virtual void OnStateExit() {}
    }
    public class StateCondition
    {
        public StateMachine stateMachine;       // 源状态机
        public Condition condition;             // 条件
        public bool expectedResult;             // 期望结果
        public StateCondition(StateMachine _stateMachine, Condition _condition, bool _expectedResult)
        {
            stateMachine = _stateMachine;
            condition = _condition;
            expectedResult = _expectedResult;
        }
        public bool IsMatching()
        {
            // 判断是否匹配
            return expectedResult == condition.Statement();
        }
    }
}