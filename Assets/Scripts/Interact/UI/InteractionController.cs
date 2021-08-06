using System;
using System.Collections.Generic;
using RPG.DialogueSystem.Graph;
using RPG.Module;
using RPG.UI;
using RPG.Utility;
using UnityEngine;

namespace RPG.Interact
{
    public class InteractionController : BaseUIController
    {
        public static string storePath = "UIView/InteractionView";
        public static InteractionController controller;

        [SerializeField] private InteractionView _interactionView;
        [SerializeField] private GameObject _interactStartDialogueButtonPrefab;
        [SerializeField] private GameObject _interactDialogueChoicePrefab;
        [SerializeField] private GameObject _interactPickButtonPrefab;

        public override void PreInit()
        {
            base.PreInit();
            _interactionView.ClearButton(true);
        }

        
        
        /// <summary>
        /// 向UI中添加互动按钮
        /// </summary>
        /// <param name="interactType">互动类型</param>
        /// <param name="content">互动按钮显示内容</param>
        /// <param name="callBack">点击按钮回调</param>
        /// <param name="buttonSprite">互动按钮Sprite 为null则默认</param>
        /// <returns>创建的互动按钮</returns>
        public InteractButton AddButton(InteractType interactType, string content, Action callBack,
            Sprite buttonSprite = null)
        {
            InteractButton newButton;
            // 初始化交互按钮
            switch (interactType)
            {
                case InteractType.Keep:
                    newButton = UIResourcesManager.Instance.LoadUserInterface(_interactStartDialogueButtonPrefab, _interactionView.Container).GetComponent<InteractButton>();
                    break;
                case InteractType.DestroySelf:
                    newButton = UIResourcesManager.Instance.LoadUserInterface(_interactPickButtonPrefab, _interactionView.Container).GetComponent<InteractButton>();
                    newButton.AddOnClickListener(delegate { newButton.Destroy(); });
                    break;
                case InteractType.DestroyAll:
                    newButton = UIResourcesManager.Instance.LoadUserInterface(_interactDialogueChoicePrefab, _interactionView.Container).GetComponent<InteractButton>();
                    newButton.AddOnClickListener(delegate { _interactionView.ClearButton(false); });
                    break;
                default:
                    throw new Exception("找不到对应的InteractButton资源");
            }
            // 添加监听
            newButton.SetInformation(buttonSprite, content);
            newButton.AddOnClickListener(callBack);
            return newButton;
        }

        public void RemoveButton(InteractButton button)
        {
            button.Destroy();
        }

        public void HideAllButton()
        {
            _interactionView.Container.SetChildrenActive(false);
        }

        public void ShowAllButton()
        {
            _interactionView.Container.SetChildrenActive(true);
        }
    }
}