using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerFallActionSO", menuName = "StateMachine/Player/Action/Fall")]
    public class PlayerFallActionSO : StateActionSO<PlayerFallAction>
    {
    }

    public class PlayerFallAction : StateAction
    {
        public override void OnUpdate()
        {
            
        }
    }
}