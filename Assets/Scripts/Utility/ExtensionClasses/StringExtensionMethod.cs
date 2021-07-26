using System;
using RPG.InputSystyem;
using UnityEngine;

namespace RPG.Utility
{
    public static class StringExtensionMethod
    {
        public static KeyCode ToKeyCode(this string keyCodeStr)
        {
            return (KeyCode) Enum.Parse(typeof(KeyCode), keyCodeStr);
        }

        public static KeyActionType ToKeyActionType(this string keyCodeStr)
        {
            return (KeyActionType) Enum.Parse(typeof(KeyActionType), keyCodeStr);
        }
    }
}