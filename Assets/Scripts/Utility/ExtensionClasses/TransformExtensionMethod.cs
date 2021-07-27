using UnityEngine;

namespace RPG.Utility
{
    public static class TransformExtensionMethod
    {
        public static void DestroyChildren(this Transform trans, bool isDestroyUnActive)
        {
            foreach (Transform child in trans)
            {
                if (!child.gameObject.activeSelf && !isDestroyUnActive)
                    continue;
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void SetChildrenActive(this Transform trans, bool isActive)
        {
            foreach (Transform child in trans)
            {
                child.gameObject.SetActive(isActive);
            }
        }
    }
}