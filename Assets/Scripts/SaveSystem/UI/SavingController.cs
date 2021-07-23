using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using RPG.InventorySystem;
using RPG.Module;
using RPG.UI;
using UnityEngine.UI;

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
        [SerializeField] private Button returnButton;
        private SavingSlot[] savingSlots;                       // 存档插槽
        private void OnEnable()
        {
            ChangeSaveSlotState();
        }

        public override void PreInit()
        {
            base.PreInit();
            // 以保存系统支持的最大存档数进行存档插槽的初始化
            savingSlots = new SavingSlot[SaveManager.Instance.maxSaveFileNum];
            for (int i = 0; i < SaveManager.Instance.maxSaveFileNum; i++)
            {
                savingSlots[i] = UIResourcesManager.Instance.LoadUserInterface(saveSlotPrefab, container).GetComponent<SavingSlot>(); 
                savingSlots[i].InitSavingSlot(i);
            }
        }

        protected override bool AchieveDoTweenSequence()
        {
            RectTransform rect = transform as RectTransform;
            inSequence.Append(rect.DOAnchorPosY(-rect.anchoredPosition.y, 0.4f).SetEase(Ease.OutBack));
            return true;
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

        public void OnCencel()
        {
            GlobalUIManager.Instance.CloseUI(this);
        }
    }
}