using UnityEngine;
namespace RPG.StateMachine
{
    public interface IStateComponent
    {
        /// <summary>
        /// 进入状态
        /// </summary>
        void OnStateEnter();
        
        /// <summary>
        /// 退出状态
        /// </summary>
        void OnStateExit();
    }
}