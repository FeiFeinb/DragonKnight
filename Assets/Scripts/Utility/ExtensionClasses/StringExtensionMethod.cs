using System;
using UnityEngine;

namespace RPG.Utility
{
    public static class StringExtensionMethod
    {
        public static KeyCode ToKeyCode(this string keyCodeStr)
        {
            return (KeyCode) Enum.Parse(typeof(KeyCode), keyCodeStr);
        }
    }
}