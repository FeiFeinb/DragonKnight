using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.FSM
{
    public class StateMachine : MonoBehaviour
    {
        public State currentState;
        [SerializeField] private TransitionTable transitionTable;
        
    }

}
