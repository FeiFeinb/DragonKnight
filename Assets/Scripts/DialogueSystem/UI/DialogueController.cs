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
        private PlayerConversant playerConversant;                      // 玩家对话管理
        public override void PreInit()
        {
            // 完成初始化
            // TODO: Player标签特化
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            // 添加当对话文件改变时监听
            playerConversant.onDialogueChange += Show;
            // 添加当对话节点改变时监听
            playerConversant.onDialogueNodeChange += UpdateDialogue;
            // 添加点击Next按钮时
            nextButton.onClick.AddListener(Next);
            quitButton.onClick.AddListener(Hide);
        }
        public override void Show()
        {
            // 打开对话UI
            gameObject.SetActive(true);
            UpdateDialogue();
        }
        public override void Hide()
        {
            // 关闭对话UI
            playerConversant.ResetDialogue();
            gameObject.SetActive(false);
        }
        private void Next()
        {
            // 推进节点进度
            playerConversant.Next();
        }
        private void UpdateDialogue()
        {
            // 判断是否存在对话组件或者对话节点
            if (playerConversant.CurrentDialogueNode == null || playerConversant.IsEmpty)
            {
                // 不存在对话节点，则直接关闭窗口
                Hide();
                return;
            }
            // 玩家主动或被动对话
            if (playerConversant.IsPlayerChoice || playerConversant.CurrentDialogueNode.Speaker == DialogueSpeaker.Player)
            {
                speakerHeadSculpture.sprite = playerConversant.DialoguePlayerInfo.HeadSculpture;
                speakerName.text = playerConversant.DialoguePlayerInfo.CharacterName;
            }
            else
            {
                speakerHeadSculpture.sprite = playerConversant.NPCHeadSculpture;
                speakerName.text = playerConversant.NPCName;
            }
            // 若非玩家选择环节 则隐藏
            playerChoiceContainer.gameObject.SetActive(playerConversant.IsPlayerChoice);
            // 当非玩家选择环节 且 不是最后一个节点 时才显示nextButton
            nextButton.gameObject.SetActive(!playerConversant.IsPlayerChoice && !playerConversant.IsLastNode);
            if (playerConversant.IsPlayerChoice)
            {
                playerChoiceContainer.DestroyChildren();
                foreach (DialogueNode choice in playerConversant.GetChoice())
                {
                    GameObject choiceObj = GameObject.Instantiate(playerChoiceButtonPrefab, playerChoiceContainer.transform);
                    choiceObj.GetComponentInChildren<Text>().text = choice.Text;
                    choiceObj.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        playerConversant.Choose(choice);
                    }); ;
                    playerConversant.CurrentDialogueNPC.OnDialogueTriggerEvent(choice.EnterEventID, choice?.UniqueID);
                }
            }
            else
            {
                // 更新UI文字显示
                playerConversant.CurrentDialogueNPC.OnDialogueTriggerEvent(playerConversant.CurrentDialogueNode?.EnterEventID, playerConversant.CurrentDialogueNode?.UniqueID);
                dialogueText.text = playerConversant.CurrentDialogueNode?.Text;
            }
        }


    }
}
