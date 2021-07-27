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
        [SerializeField] private GameObject questSidebarElementPrefab;             // 侧栏任务UI预制体
        [SerializeField] private RectTransform questSidebarContainer;       // 侧栏任务UI Container
        private Dictionary<PlayerQuestStatus, QuestSidebar> statusDic = new Dictionary<PlayerQuestStatus, QuestSidebar>();
        public override void PreInit()
        {
            base.PreInit();
            PlayerQuestManager.Instance.AddOnQuestUpdateListener(RegenerateQuestSidebars);
            PlayerQuestManager.Instance.AddQuestObjectiveUpdateListener(ReDrawQuestState);
            RegenerateQuestSidebars();
        }

        private void RegenerateQuestSidebars()
        {
            // 清空任务列表
            Clear();
            // 从玩家任务列表中加载任务
            foreach (PlayerQuestStatus questStatus in PlayerQuestManager.Instance.GetQuestStatuses())
            {
                // 遍历任务列表并生成侧栏任务UI
                QuestSidebar tempQuestSidebar = UIResourcesManager.Instance.LoadUserInterface(questSidebarElementPrefab, questSidebarContainer).GetComponent<QuestSidebar>();
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
                statusDic[questStatus].Test(questStatus);
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

