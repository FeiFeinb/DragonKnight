using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.QuestSystem
{
    public class QuestToolTipsController : ToolTipsController
    {
        public static string storePath = "UIView/QuestToolTipsView";   // 路径
        public static QuestToolTipsController controller;
        private QuestToolTipsView questToolTipsView;
        private PlayerQuestStatus currentQuest;

        public override void UpdateToolTips(GameObject _toolTipsObj)
        {
            if (questToolTipsView == null)
            {
                questToolTipsView = _toolTipsObj.GetComponent<QuestToolTipsView>();
            }
            // 开始前先清空提示面板
            questToolTipsView.Clear();
            if (!GlobalResource.Instance.questDataBaseSO.questSODic.TryGetValue(currentQuest.QuestUniqueID, out QuestSO questSO))
            {
                Debug.LogError("Cant Find QuestSO");
            }
            // 设置提示栏任务简述
            questToolTipsView.SetQuestResumeText(questSO.Resume);
            // 设置提示栏任务目标
            foreach (QuestObjective questObjective in questSO.GetObjectives())
            {
                questToolTipsView.SetQuestObjective(currentQuest.GetProgress(questObjective), questObjective.TargetAmount, questObjective.Description);
            }
            // 设置提示栏任务奖励
            questToolTipsView.SetQuestReward(questSO.Reward);
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
