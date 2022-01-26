using System;
using RPG.InputSystyem;
using RPG.Module;
using RPG.UI;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.InputSystyem
{
    public class KeySettingPairView : BaseUIPanel, ISerializationCallbackReceiver
    {
        public KeyActionType keyActionType => _keyActionType;

        public KeyType keyType => _keyType;
        
        [SerializeField] private KeyActionType _keyActionType;

        [SerializeField] private KeyType _keyType;
        
        [Space]
        
        [SerializeField] private Button _firstKeyButton;
        [SerializeField] private Button _secondKeyButton;
        
        [SerializeField] private Text _firstKeyText;
        [SerializeField] private Text _secondKeyText;

        
        public override void PreInit()
        {
            _firstKeyButton.onClick.AddListener(delegate
            {
                // TODO 设置的是哪个键位的变换
                BaseUI.GetController<OptionController>().keySettingController.ModifyKey(this.SetFirstKeyText);
            });
            // 只有AxisKey才监听第二个键位的修改
            if (_keyType == KeyType.AxisKey)
            {
                // 设置第二个键位的更改
                _secondKeyButton.onClick.AddListener(delegate
                {
                    BaseUI.GetController<OptionController>().keySettingController.ModifyKey(this.SetSecondKeyText);
                });
            }
        }

        public void SetKeyText(KeyType newKeyType, string firstKeyCode, string secondKeyCode)
        {
            _keyType = newKeyType;
            _firstKeyText.text = firstKeyCode;
            _secondKeyText.text = secondKeyCode;
            _secondKeyButton.interactable = newKeyType == KeyType.AxisKey;
        }

        public void SetFirstKeyText(KeyCode firstKeyCode)
        {
            _firstKeyText.text = firstKeyCode.ToString();
        }

        public void SetSecondKeyText(KeyCode secondKeyCode)
        {
            _secondKeyText.text = secondKeyCode.ToString();
        }
        
        public InputStrTuple GetKeyInfo()
        {
            return new InputStrTuple(_keyType.ToString(), _firstKeyText.text, _secondKeyText.text);
        }

        public void OnBeforeSerialize()
        {
            gameObject.name = _keyActionType.ToString();
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}