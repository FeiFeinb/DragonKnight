using RPG.InputSystyem;
using RPG.UI;
using RPG.Utility;
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
        
        public void Init(KeySettingController keySettingController)
        {
            mainKeyButton.onClick.AddListener(delegate { keySettingController.SetMainKeyChange(this); }); 
        }

        public void SetMainKeyText(string keyStr)
        {
            mainKeyText.text = keyStr;
        }

        public void SetOffKeyText(string keyStr)
        {
            offKeyText.text = keyStr;
        }

        /// <summary>
        /// 将UI上记录的键位信息储存到InputData中
        /// </summary>
        public void WriteUIDataToInputData()
        {
            // Add or Set ?
            InputManager.Instance.inputData.AddNormalKey(title.text, mainKeyText.text.ToKeyCode());
        }
    }
}