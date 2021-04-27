using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.QuestSystem
{
    public class QuestToolTipsController : ToolTipsController
    {
        private QuestToolTipsView questToolTipsView;
        private PlayerQuestStatus currentQuest;
        public static string storePath = "UIView/QuestToolTipsView";   // 路径
        public static QuestToolTipsController controller;

        public override void UpdateToolTips(GameObject _toolTipsObj)
        {
            if (questToolTipsView == null)
            {
                questToolTipsView = _toolTipsObj.GetComponent<QuestToolTipsView>();
            }
            // 开始前先清空提示面板
            questToolTipsView.Clear();
            // 设置提示栏任务简述
            questToolTipsView.SetQuestResumeText(currentQuest.quest.questResume);
            // 设置提示栏任务目标
            foreach (QuestObjective questObjective in currentQuest.quest.GetObjectives())
            {
                questToolTipsView.SetQuestObjective(currentQuest.GetProgress(questObjective), questObjective.ObjectiveTarget, questObjective.ObjectiveDescription);
            }
            // 设置提示栏任务奖励
            questToolTipsView.SetQuestReward(currentQuest.quest.questReward);
        }

        public void OnEnter(PlayerQuestStatus _questStatus)
        {
            currentQuest = _questStatus;
            Show();
        }
        public void OnExit(PlayerQuestStatus _questStatus)
        {
            currentQuest = null;
            Hide();
        }
    }
}
