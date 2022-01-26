
using RPG.InputSystyem;
using UnityEngine;
using RPG.Module;


public class TestClass : MonoBehaviour
{
    private void Start()
    {
        // InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.OpenInventory, delegate
        // {
        //     // 显示背包界面
        //     CenterEvent.Instance.Raise(GlobalEventID.TriggerInventoryView);
        // });
        //
        // InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.OpenEquipment, delegate
        // {
        //     // 显示装备界面
        //     CenterEvent.Instance.Raise(GlobalEventID.TriggerEquipmentView);
        // });
        //
        // // InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.SceneInteract, InteractManager.Instance.InteractFirst);
        //
        // // 手动添加AxisKey
        InputManager.Instance.inputData.SetOrAddAxisKey(KeyActionType.MoveHorizontal, KeyCode.D, KeyCode.A);
        // InputManager.Instance.inputData.SetOrAddAxisKey(KeyActionType.MoveVertical, KeyCode.W, KeyCode.S);
        // InputManager.Instance.inputData.GetNormalKey(KeyActionType.Run).SetHoldType(NormalKey.HoldType.Press);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     if (TradeController>().isActive)
        //     {
        //         TradeController>().Hide();
        //     }
        //     else
        //     {
        //         TradeController>().Show();
        //     }
        // }
    }
}