using RPG.InputSystyem;
using UnityEngine;

namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "PlayerHasHVInputSO", menuName = "StateMachine/Player/Condition/HasHVInput")]
    public class PlayerHasHVInputSO : StateConditionSO<PlayerHasHVInput> {}

    public class PlayerHasHVInput : Condition
    {
        public override bool Statement()
        {
            float horizontal = InputManager.Instance.inputData.GetAxisKeyValue(KeyActionType.MoveHorizontal);
            float vertical = InputManager.Instance.inputData.GetAxisKeyValue(KeyActionType.MoveVertical);
            return Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0;
        }
    }
}