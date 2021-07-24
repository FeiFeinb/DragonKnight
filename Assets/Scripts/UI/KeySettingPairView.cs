using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KeySettingPairView : BaseUI
    {
        public Text title;
        public Button mainKeyButton;
        public Button offKeyButton;
        public Text mainKeyText;
        public Text offKeyText;
        
        public void Init(KeySettingView keySettingView)
        {
            mainKeyButton.onClick.AddListener(delegate { keySettingView.SetMainKeyChange(this); }); 
        }

        public void SetMainKeyText(string keyStr)
        {
            mainKeyText.text = keyStr;
        }

        public void SetOffKeyText(string keyStr)
        {
            offKeyText.text = keyStr;
        }
    }
}