using UnityEngine;

namespace RPG.DialogueSystem
{
    public static class DialogueSystemExtensionMethod
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