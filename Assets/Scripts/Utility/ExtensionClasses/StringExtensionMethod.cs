using System;
using RPG.InputSystyem;
using UnityEngine;

namespace RPG.Utility
{
    public static class StringExtensionMethod
    {
        public static KeyCode ToKeyCode(this string keyCodeStr)
        {
            try
            {
                return(KeyCode) Enum.Parse(typeof(KeyCode), keyCodeStr);
            }
            catch (Exception)
            {
                return KeyCode.None;
            }
        }

        public static KeyType ToKeyType(this string keyTypeStr)
        {
            return (KeyType)Enum.Parse(typeof(KeyType), keyTypeStr);
        }
        
        public static KeyActionType ToKeyActionType(this string keyCodeStr)
        {
            return (KeyActionType) Enum.Parse(typeof(KeyActionType), keyCodeStr);
        }
    }
}