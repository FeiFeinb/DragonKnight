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
        [SerializeField] private OverlabSphereCheck _overlabSphereCheck;
        
        private Dictionary<Collider, InteractButton> uiDic = new Dictionary<Collider, InteractButton>();
        private void Start()
        {
            _overlabSphereCheck.onAddCollider += ApproachCollider;
            _overlabSphereCheck.onLossCollider += NonContactCollider;
        }

        private void NonContactCollider(Collider removeCollider)
        {
            if (!removeCollider || !removeCollider.gameObject) return;
            removeCollider.gameObject.TryGetComponent(out IInteractable tempInteractable);
            if (tempInteractable == null) return;
            InteractionController.controller.RemoveButton(uiDic[removeCollider]);
            uiDic.Remove(removeCollider);
        }

        private void ApproachCollider(Collider addCollider)
        {
            addCollider.gameObject.TryGetComponent(out IInteractable tempInteractable);
            if (tempInteractable == null) return;
            tempInteractable.GetInteractInfo(out InteractType type, out string buttonStr, out Sprite sprite);
            uiDic.Add(addCollider, InteractionController.controller.AddButton(type, buttonStr, tempInteractable.Interact, sprite));
        }

        public void AddDialogueChoiceButton(string content, Action callBack)
        {
            InteractionController.controller.AddButton(InteractType.DialogueChoice, content, callBack);
        }
    }
}