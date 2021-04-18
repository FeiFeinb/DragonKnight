using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New TransitionTable", menuName = "FSM/TransitionTable")]
public class TransitionTable : ScriptableObject
{
    [System.Serializable]
    public struct TransitionItem
    {

    }
    [System.Serializable]
    public struct ConditionUsage
    {
        public Result expectedResult;
        public Opetator opetator;
    }

    public enum Result
    {
        True, False
    }
    public enum Opetator
    {
        And, Or
    }
}
