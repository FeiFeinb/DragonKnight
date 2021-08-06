using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using RPG.Interact;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace RPG.InventorySystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver, IInteractable
    {
        public BaseItemObject itemObj;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            // 使物体Sprite与itemObj对应
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (itemObj == null)
            {
                spriteRenderer.sprite = null;
                gameObject.name = "Null Item";
            }
            else
            {
                spriteRenderer.sprite = itemObj.sprite;
                gameObject.name = itemObj.name;
            }

            EditorUtility.SetDirty(spriteRenderer);
#endif
        }

        private void LateUpdate()
        {
            // 使图标始终朝向相机
            transform.LookAt(mainCamera.transform);
        }

        public void GetInteractInfo(out InteractType type, out string buttonStr, out Sprite sprite)
        {
            type = InteractType.DestroySelf;
            buttonStr = itemObj.name;
            sprite = itemObj.sprite;
        }

        public void OnInteractButtonClick()
        {
            int itemLeft = PlayerInventoryManager.Instance.inventoryObject.AddWithoutCheck(new ItemData(itemObj), 1);
            if (itemLeft == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"背包已满 剩下{itemLeft}个无法装下");
            }
        }
    }
}