using UnityEngine;
namespace RPG.StateMachine
{
    [CreateAssetMenu(fileName = "GetKeyDownEConditionSO", menuName = "StateMachine/Condition/GetKeyDownE")]
    public class GetKeyDownEConditionSO : StateConditionSO<GetKeyDownECondition> {}
    public class GetKeyDownECondition : Condition
    {
        public override bool Statement()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.E);
        }
    }
}