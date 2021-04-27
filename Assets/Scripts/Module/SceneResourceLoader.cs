using UnityEngine;
using RPG.InventorySystem;
using RPG.UI;
using RPG.SaveSystem;
using RPG.DialogueSystem;
using RPG.QuestSystem;
namespace RPG.Module
{
    public class SceneResourceLoader : BaseSingletonWithMono<SceneResourceLoader>
    {
        // 进入场景时加载UI资源
        private void Start()
        {
            InventoryController.controller = InitController<InventoryController>(InventoryController.storePath);
            EquipmentController.controller = InitController<EquipmentController>(EquipmentController.storePath);
            MouseItemIcon.controller = InitController<MouseItemIcon>(MouseItemIcon.storePath);
            SavingController.controller = InitController<SavingController>(SavingController.storePath);
            DialogueController.controller = InitController<DialogueController>(DialogueController.storePath);
            QuestSidebarController.controller = InitController<QuestSidebarController>(QuestSidebarController.storePath);
            QuestToolTipsController.controller = InitController<QuestToolTipsController>(QuestToolTipsController.storePath);
            MouseItemTipsController.controller = InitController<MouseItemTipsController>(MouseItemTipsController.storePath);
        }
        private T InitController<T>(string _storePath) where T : BaseUIController
        {
            return UIResourcesManager.Instance.LoadUserInterface(new UIStoreInfo(_storePath)).GetComponent<T>();
        }
    }
}