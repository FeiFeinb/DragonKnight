using System.Xml.Serialization;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UI;
namespace RPG.DialogueSystem
{
    public class DialogueController : BaseUIController
    {
        public static string storePath = "UIView/DialogueView";
        public static DialogueController controller;
        [SerializeField] private Image speakerHeadSculpture;            // 对话人物头像
        [SerializeField] private Text speakerName;           // 对话人物名字
        [SerializeField] private Text dialogueText;          // 对话文字
        [SerializeField] private Button nextButton;                     // 下一个按钮
        [SerializeField] private Button quitButton;                     // 退出按钮
        [SerializeField] private Transform playerChoiceContainer;       // 选项按钮容器
        [SerializeField] private GameObject playerChoiceButtonPrefab;   // 玩家选项容器
        public override void PreInit()
        {
            // 完成初始化
            // 添加当对话文件改变时监听
            PlayerDialogueManager.Instance.onDialogueChange += (dialogueSO) =>
            {
                if (dialogueSO == null) Hide();
                else Show();
            };
            // 添加当对话节点改变时监听
            PlayerDialogueManager.Instance.onDialogueNodeChange += UpdateDialogue;
            // 添加点击Next按钮事件
            nextButton.onClick.AddListener(Next);
            // 添加点击Quit按钮事件
            quitButton.onClick.AddListener(CloseDialogue);
        }
        public override void Show()
        {
            // 打开对话UI
            gameObject.SetActive(true);
            UpdateDialogue();
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
        }
        public void CloseDialogue()
        {
            PlayerDialogueManager.Instance.ResetDialogue();
        }
        private void Next()
        {
            // 推进节点进度
            PlayerDialogueManager.Instance.Next();
        }
        private void UpdateDialogue()
        {
            // 判断是否存在对话组件或者对话节点
            if (PlayerDialogueManager.Instance.CurrentDialogueNode == null || PlayerDialogueManager.Instance.IsEmpty)
            {
                // 不存在对话节点，则直接关闭窗口
                CloseDialogue();
                return;
            }
            // 玩家主动或被动对话
            if (PlayerDialogueManager.Instance.IsPlayerChoice || PlayerDialogueManager.Instance.CurrentDialogueNode.Speaker == DialogueSpeaker.Player)
            {
                speakerHeadSculpture.sprite = PlayerDialogueManager.Instance.DialoguePlayerInfo.HeadSculpture;
                speakerName.text = PlayerDialogueManager.Instance.DialoguePlayerInfo.CharacterName;
            }
            else
            {
                speakerHeadSculpture.sprite = PlayerDialogueManager.Instance.NPCHeadSculpture;
                speakerName.text = PlayerDialogueManager.Instance.NPCName;
            }
            // 若非玩家选择环节 则隐藏
            playerChoiceContainer.gameObject.SetActive(PlayerDialogueManager.Instance.IsPlayerChoice);
            // 当非玩家选择环节 且 不是最后一个节点 时才显示nextButton
            nextButton.gameObject.SetActive(!PlayerDialogueManager.Instance.IsPlayerChoice && !PlayerDialogueManager.Instance.IsLastNode);
            if (PlayerDialogueManager.Instance.IsPlayerChoice)
            {
                playerChoiceContainer.DestroyChildren();
                foreach (DialogueNodeSO choice in PlayerDialogueManager.Instance.GetChoice())
                {
                    GameObject choiceObj = GameObject.Instantiate(playerChoiceButtonPrefab, playerChoiceContainer.transform);
                    choiceObj.GetComponentInChildren<Text>().text = choice.Text;
                    choiceObj.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        PlayerDialogueManager.Instance.Choose(choice);
                    }); ;
                    PlayerDialogueManager.Instance.CurrentDialogueNPC.OnDialogueTriggerEvent(choice.EnterEventID, choice?.UniqueID);
                }
            }
            else
            {
                // 更新UI文字显示
                PlayerDialogueManager.Instance.CurrentDialogueNPC.OnDialogueTriggerEvent(PlayerDialogueManager.Instance.CurrentDialogueNode?.EnterEventID, PlayerDialogueManager.Instance.CurrentDialogueNode?.UniqueID);
                dialogueText.text = PlayerDialogueManager.Instance.CurrentDialogueNode?.Text;
            }
        }


    }
}
