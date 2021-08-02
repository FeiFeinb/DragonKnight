using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RPG.InputSystyem;
using RPG.Interact;
using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using RPG.UI;
using RPG.SaveSystem;
using RPG.TradeSystem;
using UnityEditor;
using UnityEngine.Events;

public class TestClass : MonoBehaviour
{
    private void Start()
    {
        InputManager.Instance.inputData.SetNormalKeyWeights(KeyActionType.OpenOption, 1);
        InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.OpenOption, delegate
        {
            if (GlobalUIManager.Instance.UICount == 0)
            {
                GlobalUIManager.Instance.OpenUI(PauseController.controller);
            }
            else
            {
                GlobalUIManager.Instance.CloseUI();
            }
        });
        
        InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.OpenInventory, delegate
        {
            // 显示背包界面
            if (InventoryController.controller.isActive)
            {
                InventoryController.controller.Hide();
            }
            else
            {
                InventoryController.controller.Show();
            }
        });

        InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.OpenEquipment, delegate
        {
            // 显示装备界面
            if (EquipmentController.controller.isActive)
            {
                EquipmentController.controller.Hide();
            }
            else
            {
                EquipmentController.controller.Show();
            }
        });
        
        // InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.SceneInteract, InteractManager.Instance.InteractFirst);
        
        // 手动添加AxisKey
        InputManager.Instance.inputData.SetOrAddAxisKey(KeyActionType.MoveHorizontal, KeyCode.D, KeyCode.A);
        InputManager.Instance.inputData.SetOrAddAxisKey(KeyActionType.MoveVertical, KeyCode.W, KeyCode.S);
        InputManager.Instance.inputData.GetNormalKey(KeyActionType.Run).SetHoldType(NormalKey.HoldType.Press);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     if (TradeController.controller.isActive)
        //     {
        //         TradeController.controller.Hide();
        //     }
        //     else
        //     {
        //         TradeController.controller.Show();
        //     }
        // }
    }
}