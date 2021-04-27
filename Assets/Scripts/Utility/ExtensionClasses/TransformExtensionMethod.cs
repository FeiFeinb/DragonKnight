using UnityEngine;

namespace RPG.Utility
{
    public static class TransformExtensionMethod
    {
        public static void DestroyChildren(this Transform trans)
        {
            foreach (Transform child in trans)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}