using RPG.StateMachine;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerIdleStateActionSO", menuName = "StateMachine/Player/Action/Idle")]
    public class PlayerIdleStateActionSO : StateActionSO<PlayerIdleStateAction> {}

    public class PlayerIdleStateAction : StateAction
    {
        public override void OnUpdate()
        {
            
        }
    }
}