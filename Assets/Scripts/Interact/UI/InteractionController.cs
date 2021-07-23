using System;
using System.Collections.Generic;
using RPG.DialogueSystem.Graph;
using RPG.Module;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inertact
{
    public class InteractionController : BaseUIController
    {
        public static string storePath = "UIView/InteractionView";
        public static InteractionController controller;

        [SerializeField] private InteractionView _interactionView;
        [SerializeField] private GameObject _interactButtonPrefab;

        public override void PreInit()
        {
            base.PreInit();
            _interactionView.ClearButton();
        }


        public void AddButton(string content, Action callBack)
        {
            InteractButton newButton = UIResourcesManager.Instance
                .LoadUserInterface(_interactButtonPrefab.gameObject, _interactionView.Container.transform)
                .GetComponent<InteractButton>();
            // 初始化交互按钮
            newButton.Init(content);
            // 点击一次后即刻删除
            newButton.AddOnClickListener(delegate { _interactionView.ClearButton(); });
            // 添加监听
            newButton.AddOnClickListener(callBack);
        }
    }
}