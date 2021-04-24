using System.Threading;
using UnityEngine;
namespace RPG.StateMachine
{
    public abstract class StateAction : IStateComponent
    {
        public StateActionSO originStateActionSO;                   // 源StateActionSO

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="stateMachine">当前实体所挂在StateMachine</param>
        public virtual void Init(StateMachine stateMachine) {}
        public virtual void OnStateEnter() {}

        /// <summary>
        /// Update执行
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// FixedUpdate执行
        /// </summary>
        public virtual void OnFixedUpdate() {}

        /// <summary>
        /// LateUpdate执行
        /// </summary>
        public virtual void OnLateUpdate() {}
        public virtual void OnStateExit() {}
    }
}