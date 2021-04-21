using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "New GuardWalkStateActionSO", menuName = "FSM/GuardWalkStateActionSO")]
    public class GuardWalkStateActionSO : StateActionSO<GuardWalkStateAction> {}
    public class GuardWalkStateAction : StateAction
    {
        public override void OnUpdate()
        {
            Debug.Log("Walk");
        }
    }
}