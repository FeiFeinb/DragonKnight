using System;
using System.Collections.Generic;
using RPG.Interact;
using RPG.Module;
using RPG.Utility;
using UnityEngine;

namespace RPG.Interact
{
    public class InteractManager : BaseSingletonWithMono<InteractManager>
    {
        [SerializeField] private UpdateOverlapSphereCheck updateOverlapSphereCheck;
        
        private Dictionary<IInteractable, InteractButton> uiDic = new Dictionary<IInteractable, InteractButton>();
        private void Start()
        {
            updateOverlapSphereCheck.onAddCollider += ApproachCollider;
            updateOverlapSphereCheck.onLossCollider += NonContactCollider;
        }

        private void NonContactCollider(Collider removeCollider)
        {
            // 若物品已被删除 则返回
            if (!removeCollider || !removeCollider.gameObject) return;
            removeCollider.gameObject.TryGetComponent(out IInteractable tempInteractable);
            if (tempInteractable == null) return;
            InteractionController.controller.RemoveButton(uiDic[tempInteractable]);
            uiDic.Remove(tempInteractable);
            // _buttonsList.Remove(tempInteractable);
        }

        private void ApproachCollider(Collider addCollider)
        {
            addCollider.gameObject.TryGetComponent(out IInteractable tempInteractable);
            if (tempInteractable == null) return;
            tempInteractable.GetInteractInfo(out InteractType type, out string buttonStr, out Sprite sprite);
            InteractButton addButton = InteractionController.controller.AddButton(type, buttonStr, () =>
            {
                // uiDic.Remove(addCollider);
                // _buttonsList.Remove(tempInteractable);
                tempInteractable.OnInteractButtonClick();
            }, sprite);
            uiDic.Add(tempInteractable, addButton);
            // _buttonsList.Add(tempInteractable);
        }

        public void AddDialogueChoiceButton(string content, Action callBack)
        {
            InteractionController.controller.AddButton(InteractType.DestroyAll, content, callBack);
        }

        // public void InteractFirst()
        // {
        //     if (_buttonsList.Count > 0)
        //     {
        //         _buttonsList[0].OnInteractButtonClick();
        //         _buttonsList.RemoveAt(0);
        //     }
        // }
    }
}