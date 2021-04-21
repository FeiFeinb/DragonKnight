using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "New GuardRunStateActionSO", menuName = "FSM/GuardRunStateActionSO")]
    public class GuardRunStateActionSO : StateActionSO<GuardRunStateAction> {}
    public class GuardRunStateAction : StateAction
    {
        public override void OnUpdate()
        {
            Debug.Log("Run");
        }
    }
}