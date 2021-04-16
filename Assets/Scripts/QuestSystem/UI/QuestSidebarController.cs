using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.Module;
namespace RPG.QuestSystem
{
    public class QuestSidebarController : BaseUIController
    {
        public static string storePath = "UIView/QuestSidebarView";
        public static QuestSidebarController controller;
        [SerializeField] private GameObject questSidebarPrefab;             // 侧栏任务UI预制体
        [SerializeField] private RectTransform questSidebarContainer;       // 侧栏任务UI Container
        private Dictionary<PlayerQuestStatus, QuestSidebar> statusDic = new Dictionary<PlayerQuestStatus, QuestSidebar>();
        public override void PreInit()
        {
            PlayerQuestManager.Instance.AddOnQuestUpdateListener(RenerateQuestSidebars);
            PlayerQuestManager.Instance.AddQuestObjectiveUpdateListener(ReDrawQuestState);
            RenerateQuestSidebars();
        }
        private void RenerateQuestSidebars()
        {
            // 清空任务列表
            Clear();
            // 从玩家任务列表中加载任务
            foreach (PlayerQuestStatus questStatus in PlayerQuestManager.Instance.GetQuestStatuses())
            {
                // 遍历任务列表并生成侧栏任务UI
                QuestSidebar tempQuestSidebar = UIResourcesManager.Instance.LoadUserInterface(questSidebarPrefab, questSidebarContainer).GetComponent<QuestSidebar>();
                // 记录到数列中
                tempQuestSidebar.InitQuestSidebarInfo(questStatus);
                statusDic.Add(questStatus, tempQuestSidebar);
            }
        }
        private void ReDrawQuestState()
        {
            // 遍历已记录的任务列表
            foreach (PlayerQuestStatus questStatus in PlayerQuestManager.Instance.GetQuestStatuses())
            {
                // 已完成的任务
                if (questStatus.IsFinish)
                {
                    statusDic[questStatus].SetFinishState(questStatus);
                }
                // 未完成的任务
                else
                {
                    statusDic[questStatus].UpdateState(questStatus);
                }
            }
        }
        private void Clear()
        {
            foreach (var statisPair in statusDic)
            {
                Destroy(statisPair.Value.gameObject);
            }
            statusDic.Clear();
        }
    }
}

