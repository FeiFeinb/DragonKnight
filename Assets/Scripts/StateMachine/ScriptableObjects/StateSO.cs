using System.Collections.Generic;
using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "New StateSO", menuName = "StateMachine/StateSO")]
    public class StateSO : ScriptableObject
    {
        [SerializeField] private StateActionSO[] stateActionSOs;    // 状态事件数列
        public State GetState(StateMachine stateMachine, Dictionary<ScriptableObject, object> createInstance)
        {
            // 查看是否已经存在缓存中
            if (createInstance.TryGetValue(this, out object obj))
            {
                return obj as State;
            }
            // 缓存中不存在 新建State
            State state = new State(this, stateMachine);
            // 储存在缓存中
            createInstance.Add(this, state);
            return state;
        }
        public StateAction[] GetActions(StateMachine stateMachine, Dictionary<ScriptableObject, object> createInstance)
        {
            // 根据类中储存的状态事件数列构建新的StateAction[]
            StateAction[] stateActions = new StateAction[stateActionSOs.Length];
            for (int i = 0; i < stateActions.Length; i++)
            {
                stateActions[i] = stateActionSOs[i].GetStateAction(stateMachine, createInstance);
            }
            return stateActions;
        }
    }
}