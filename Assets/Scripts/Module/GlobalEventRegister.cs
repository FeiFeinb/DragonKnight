using System;
using RPG.Config;
using RPG.InputSystyem;
using RPG.InventorySystem;
using RPG.SaveSystem;
using RPG.UI;
using RPG.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RPG.Module
{
    public enum GlobalEventID
    {
        TriggerOptionView,
        TriggerInventoryView,
        TriggerEquipmentView,
        TriggerSaveView,
        
        EnterScene,
        ExitScene,
        
        StartNewGame,
        ExitGame
    }
    
    public class GlobalEventRegister : BaseSingletonWithMono<GlobalEventRegister>
    {
        public void Register()
        {
            CenterEvent.Instance.AddListener(GlobalEventID.EnterScene, delegate
            {
                // 查找摄像机
                InputManager.Instance.SeekOrSetMainCamera();
                // 查找幕布
                UIResourcesLoader.Instance.SeekOrSetMainCanvas();
                // 加载当前场景中的数据
                SceneResource.Instance.LoadSceneResource();
            });
            
            CenterEvent.Instance.AddListener(GlobalEventID.ExitScene, delegate
            {
                UIStackManager.Instance.Clear();
                UIResourcesLoader.Instance.Clear();
            });
            
            // 事件Enum到具体事情的绑定 打开选项菜单以及打开保存菜单
            CenterEvent.Instance.AddListener(GlobalEventID.TriggerOptionView,
                delegate { UIStackManager.Instance.TriggerUI(BaseUI.GetController<OptionController>(), true); });
            CenterEvent.Instance.AddListener(GlobalEventID.TriggerSaveView,
                delegate { UIStackManager.Instance.TriggerUI(BaseUI.GetController<SavingController>(), true); });
            
            // 打开背包以及装备显示
            CenterEvent.Instance.AddListener(GlobalEventID.TriggerInventoryView,
                delegate { UIStackManager.Instance.TriggerUI(BaseUI.GetController<InventoryController>()); });
            CenterEvent.Instance.AddListener(GlobalEventID.TriggerEquipmentView,
                delegate { UIStackManager.Instance.TriggerUI(BaseUI.GetController<EquipmentController>()); });
            
            // 退出游戏和开始新游戏
            CenterEvent.Instance.AddListener(GlobalEventID.ExitGame, QuitGame);
            CenterEvent.Instance.AddListener(GlobalEventID.StartNewGame, StartGame);
            
            // 键盘输入与事件Enum的绑定
            // TODO: 从Json中读取
            InputManager.Instance.inputData.SetNormalKeyWeights(KeyActionType.Exit, 1);
            InputManager.Instance.inputData.AddNormalKeyListener(KeyActionType.Exit, delegate
            {
                CenterEvent.Instance.Raise(GlobalEventID.TriggerOptionView);
            });
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void StartGame()
        {
            // 加载初始场景
            SceneLoader.Instance.Load("MedievalCastleScene");
        }
        
        public static void AddLocalEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            // 获取EventTrigger
            EventTrigger trigger = obj.GetOrAddComponent<EventTrigger>();
            // 新建监听
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            // 添加监听
            trigger.triggers.Add(eventTrigger);
        }
    }
}