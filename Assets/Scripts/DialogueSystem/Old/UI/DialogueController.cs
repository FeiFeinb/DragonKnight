using DialogueSystem.Old.Dialogue.Core;
using DialogueSystem.Old.Dialogue.ScriptableObjects;
using DialogueSystem.Old.PlayerControl;
using RPG.UI;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem.Old.UI
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
            OldPlayerDialogueManager.Instance.onDialogueChange += (dialogueSO) =>
            {
                if (dialogueSO == null) Hide();
                else
                {
                    Show();
                    UpdateDialogue();
                }
            };
            // 添加当对话节点改变时监听
            OldPlayerDialogueManager.Instance.onDialogueNodeChange += UpdateDialogue;
            // 添加点击Next按钮事件
            nextButton.onClick.AddListener(Next);
            // 添加点击Quit按钮事件
            quitButton.onClick.AddListener(CloseDialogue);
        }

        private void Next()
        {
            // 推进节点进度
            OldPlayerDialogueManager.Instance.Next();
        }
        public void CloseDialogue()
        {
            // 关闭对话框
            OldPlayerDialogueManager.Instance.ResetDialogue();
        }
        private void UpdateDialogue()
        {
            // 判断是否存在对话组件或者对话节点
            if (OldPlayerDialogueManager.Instance.CurrentDialogueNode == null || OldPlayerDialogueManager.Instance.IsEmpty)
            {
                // 不存在对话节点，则直接关闭窗口
                CloseDialogue();
                return;
            }
            // 玩家主动或被动对话
            if (OldPlayerDialogueManager.Instance.IsPlayerChoice || OldPlayerDialogueManager.Instance.CurrentDialogueNode.Speaker == DialogueSpeaker.Player)
            {
                speakerHeadSculpture.sprite = OldPlayerDialogueManager.Instance.DialoguePlayerInfo.HeadSculpture;
                speakerName.text = OldPlayerDialogueManager.Instance.DialoguePlayerInfo.CharacterName;
            }
            else
            {
                speakerHeadSculpture.sprite = OldPlayerDialogueManager.Instance.NPCHeadSculpture;
                speakerName.text = OldPlayerDialogueManager.Instance.NPCName;
            }
            // 若非玩家选择环节 则隐藏
            playerChoiceContainer.gameObject.SetActive(OldPlayerDialogueManager.Instance.IsPlayerChoice);
            // 当非玩家选择环节 且 不是最后一个节点 时才显示nextButton
            nextButton.gameObject.SetActive(!OldPlayerDialogueManager.Instance.IsPlayerChoice && !OldPlayerDialogueManager.Instance.IsLastNode);
            if (OldPlayerDialogueManager.Instance.IsPlayerChoice)
            {
                playerChoiceContainer.DestroyChildren();
                foreach (DialogueNodeSO choice in OldPlayerDialogueManager.Instance.GetChoice())
                {
                    GameObject choiceObj = GameObject.Instantiate(playerChoiceButtonPrefab, playerChoiceContainer.transform);
                    choiceObj.GetComponentInChildren<Text>().text = choice.Content;
                    choiceObj.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        OldPlayerDialogueManager.Instance.Choose(choice);
                    }); ;
                    OldPlayerDialogueManager.Instance.CurrentDialogueNPC.TryTriggerDialogueEvent(choice.EnterEventID, choice?.UniqueID);
                }
            }
            else
            {
                // 更新UI文字显示
                OldPlayerDialogueManager.Instance.CurrentDialogueNPC.TryTriggerDialogueEvent(OldPlayerDialogueManager.Instance.CurrentDialogueNode?.EnterEventID, OldPlayerDialogueManager.Instance.CurrentDialogueNode?.UniqueID);
                dialogueText.text = OldPlayerDialogueManager.Instance.CurrentDialogueNode?.Content;
            }
        }


    }
}
