using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RPG.InputSystyem;
using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using RPG.UI;
using RPG.SaveSystem;
using RPG.TradeSystem;
using UI;
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