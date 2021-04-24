using System.Collections.Generic;
using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using RPG.UI;
using RPG.SaveSystem;
public class TestClass : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        }
        if (Input.GetKeyDown(KeyCode.R))
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
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SavingController.controller.isActive)
            {
                SavingController.controller.Hide();
            }
            else
            {
                SavingController.controller.Show();
            }
        }
    }

}
