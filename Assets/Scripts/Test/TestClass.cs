using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     // 显示背包界面
        //     if (InventoryController.controller.isActive)
        //     {
        //         InventoryController.controller.Hide();
        //     }
        //     else
        //     {
        //         InventoryController.controller.Show();
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     // 显示装备界面
        //     if (EquipmentController.controller.isActive)
        //     {
        //         EquipmentController.controller.Hide();
        //     }
        //     else
        //     {
        //         EquipmentController.controller.Show();
        //     }
        // }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GlobalUIManager.Instance.UICount == 0)
            {
                GlobalUIManager.Instance.OpenUI(PauseController.controller);
            }
            else if (!GlobalUIManager.Instance.isforceDontClose)
            { 
                GlobalUIManager.Instance.CloseUI();
            }
        }
        
        
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

