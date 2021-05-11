using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Module;
using RPG.Utility;
namespace RPG.QuestSystem
{
    public class QuestToolTipsView : MonoBehaviour
    {
        [SerializeField] private GameObject questToolTipsObjectivePrefab;       // 提示栏任务目标预制体
        [SerializeField] private GameObject questToolTipsRewardPrefab;          // 提示栏任务奖励预制体
        [SerializeField] private GameObject questToolTipsEquipmentRewardPrefab; // 提示人任务装备奖励预制体
        [SerializeField] private RectTransform questToolTipsObjectiveContainer; // 提示栏任务目标Container
        [SerializeField] private RectTransform questToolTipsRewardContainer;    // 提示栏任务奖励Container
        [SerializeField] private Text questResumeText;
        [SerializeField] private List<QuestObjectiveUI> questObjectiveUIs = new List<QuestObjectiveUI>();
        [SerializeField] private List<QuestToolTipsReward> questToolTipsRewards = new List<QuestToolTipsReward>();
        public void SetQuestResumeText(string _questResumeStr)
        {
            questResumeText.text = _questResumeStr;
        }
        public void SetQuestObjective(int questProgress, int objectiveTarget, string questDescription)
        {
            // 初始化并生成提示栏任务目标
            QuestObjectiveUI tempQuestObjectiveUI = UIResourcesManager.Instance.LoadUserInterface(questToolTipsObjectivePrefab, questToolTipsObjectiveContainer).GetComponent<QuestObjectiveUI>();
            tempQuestObjectiveUI.SetQuestObjectiveSidebar(questProgress, objectiveTarget, questDescription);
            // 添加记录至数列中
            questObjectiveUIs.Add(tempQuestObjectiveUI);
        }
        public void SetQuestReward(QuestReward questReward)
        {
            // 检查有效奖励的数量
            // 设置金币
            if (!questReward.coin.IsZero)
            {
                QuestToolTipsReward tempQuestToolTipsReward = UIResourcesManager.Instance.LoadUserInterface(questToolTipsRewardPrefab, questToolTipsRewardContainer).GetComponent<QuestToolTipsReward>();
                tempQuestToolTipsReward.SetQuestToolTipsReward(questReward.CoinStr);
                // questToolTipsRewards.Add(tempQuestToolTipsReward);
            }
            // 设置经验
            if (questReward.experience > 0)
            {
                QuestToolTipsReward tempQuestToolTipsReward = UIResourcesManager.Instance.LoadUserInterface(questToolTipsRewardPrefab, questToolTipsRewardContainer).GetComponent<QuestToolTipsReward>();
                tempQuestToolTipsReward.SetQuestToolTipsReward(questReward.ExperienceStr);
                // questToolTipsRewards.Add(tempQuestToolTipsReward);
            }
            // 设置装备
            if (questReward.itemObjAmount > 0)
            {
                QuestToolTipsEquipmentReward tempQuestToolTipsEquipmentReward = UIResourcesManager.Instance.LoadUserInterface(questToolTipsEquipmentRewardPrefab, questToolTipsRewardContainer).GetComponent<QuestToolTipsEquipmentReward>();
                tempQuestToolTipsEquipmentReward.SetQuestToolTipsReward(questReward.itemObj, questReward.itemObjAmount);
                // questToolTipsRewards.Add(tempQuestToolTipsEquipmentReward);
            }
        }
        public void Clear()
        {
            questResumeText.text = string.Empty;
            foreach (var questObjectiveUI in questObjectiveUIs)
            {
                Destroy(questObjectiveUI.gameObject);
            }
            questObjectiveUIs.Clear();
            questToolTipsRewardContainer.DestroyChildren();
        }
    }
}
