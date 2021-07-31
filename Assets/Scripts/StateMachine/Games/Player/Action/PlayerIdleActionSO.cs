using RPG.StateMachine;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerIdleActionSO", menuName = "StateMachine/Player/Action/Idle")]
    public class PlayerIdleActionSO : StateActionSO<PlayerIdleAction> {}

    public class PlayerIdleAction : StateAction
    {
        public override void OnUpdate()
        {
            
        }
    }
}