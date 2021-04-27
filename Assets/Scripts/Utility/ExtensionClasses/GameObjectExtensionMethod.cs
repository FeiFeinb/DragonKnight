using UnityEngine;

namespace RPG.Utility
{
    public static class GameObjectExtensionMethod
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            if (obj.TryGetComponent<T>(out T retComponent))
            {
                return retComponent;
            }
            return obj.AddComponent<T>();
        }
    }
}