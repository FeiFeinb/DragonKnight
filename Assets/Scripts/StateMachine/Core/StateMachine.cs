using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State currentState;                                     // 当前状态
        [SerializeField] private TransitionTableSO transitionTable;     // 状态转换表
        private void Start()
        {
            // 初始化状态转换表
            transitionTable.InitTransitionTable(this);
            // 将状态转化表中的根节点作为初始状态
            // TODO: 可设置的初始根节点
            TransitionState(transitionTable.rootState);
        }
        private void TransitionState(State targetState)
        {
            currentState?.OnStateExit();
            currentState = targetState;
            currentState?.OnStateEnter();
        }
        private void Update()
        {
            // 每帧检测是否需要切换
            State targetState = currentState.TryTransition();
            if (targetState != null)
            {
                TransitionState(targetState);
            }
            // 每帧执行状态
            currentState.OnUpdate();
        }
        private void FixedUpdate()
        {
            currentState.OnFixedUpdate();
        }
        private void LateUpdate()
        {
            currentState.OnLateUpdate();
        }

    }

}
