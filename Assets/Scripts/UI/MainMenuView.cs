using System;
using RPG.Module;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MainMenuView : BaseUIPanel
    {
        public RectTransform buttonContainer;
        
        public Button settingButton;
        public Button exitButton;
        
        public Button newGameButton;
        public Button loadGameButton;
        
        public override void PreInit()
        {
            settingButton.onClick.AddListener(delegate { CenterEvent.Instance.Raise(GlobalEventID.TriggerOptionView); });
            exitButton.onClick.AddListener(delegate { CenterEvent.Instance.Raise(GlobalEventID.ExitGame); });
            newGameButton.onClick.AddListener(delegate { CenterEvent.Instance.Raise(GlobalEventID.StartNewGame); });
            loadGameButton.onClick.AddListener(delegate { CenterEvent.Instance.Raise(GlobalEventID.TriggerSaveView); });
            
        }
        
        
    }
}