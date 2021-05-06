using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using RPG.UI;
using RPG.Module;
namespace RPG.SaveSystem
{
    public class SavingController : BaseUIController
    {
        public static string storePath = "UIView/SavingView";   // 路径
        public static SavingController controller;
        
        [Tooltip("UI无存档颜色")]public Color defaultColor;
        [Tooltip("UI有存档颜色")]public Color fillColor;
        [SerializeField] private Transform container;           // 插槽生成容器
        [SerializeField] private GameObject saveSlotPrefab;     // 插槽预制体
        private SavingSlot[] savingSlots;                       // 存档插槽
        private void OnEnable()
        {
            ChangeSaveSlotState();
        }
        public override void PreInit()
        {
            // 以保存系统支持的最大存档数进行存档插槽的初始化
            savingSlots = new SavingSlot[SaveManager.Instance.maxSaveFileNum];
            for (int i = 0; i < SaveManager.Instance.maxSaveFileNum; i++)
            {
                savingSlots[i] = UIResourcesManager.Instance.LoadUserInterface(saveSlotPrefab, container).GetComponent<SavingSlot>(); 
                savingSlots[i].InitSavingSlot(i);
            }
        }
        public void ChangeSaveSlotState()
        {
            var _saveDics = SaveManager.Instance.LoadSaveDics();
            for (int i = 0; i < _saveDics.Length; i++)
            {
                // 该插槽具有存档
                if (_saveDics[i] != null && _saveDics[i].ContainsKey("saveTime"))
                {
                    savingSlots[i].SetSlotFilled(_saveDics[i]["saveTime"] as string);
                }
                // 该插槽无存档
                else
                {
                    savingSlots[i].SetSlotEmpty();
                }
            }
        }
        public void OnSave(int saveSlotIndex)
        {
            SaveManager.Instance.Save(saveSlotIndex, (timeStr) =>
            {
                savingSlots[saveSlotIndex].SetSlotFilled(timeStr);
            });
        }
        public void OnDelete(int saveSlotIndex)
        {
            SaveManager.Instance.Delete(saveSlotIndex, delegate
            {
                savingSlots[saveSlotIndex].SetSlotEmpty();
            });
        }
        public void OnLoad(int saveSlotIndex)
        {
            SaveManager.Instance.Load(saveSlotIndex);
        }
    }
}