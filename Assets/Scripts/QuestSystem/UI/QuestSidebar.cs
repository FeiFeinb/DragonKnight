using UnityEngine;
using UnityEngine.UI;
using RPG.UI;
using System.Collections.Generic;
using RPG.Module;
namespace RPG.QuestSystem
{
    public class QuestSidebar : MonoBehaviour
    {
        [SerializeField] private GameObject questSidebarObjectivePrefab;    // 侧栏任务目标预制体
        [SerializeField] private RectTransform objectiveContainer;          // 侧栏任务目标Container
        [SerializeField] private Button questIndexButton;
        [SerializeField] private Text questIndex;       // 侧栏任务序号
        [SerializeField] private Text questTitle;       // 侧栏任务标题
        private Dictionary<QuestObjective, QuestObjectiveUI> objectiveDic = new Dictionary<QuestObjective, QuestObjectiveUI>();

        private QuestObjectiveUI finishUI;
        
        public void InitQuestSidebarInfo(PlayerQuestStatus questStatus)
        {
            objectiveDic = new Dictionary<QuestObjective, QuestObjectiveUI>();
            // 设置按钮点击事件
            questIndexButton.onClick.AddListener(() =>
            {
                if (!QuestToolTipsController.controller.isActive)
                {
                    QuestToolTipsController.controller.OnEnter(questStatus);
                }
                else
                {
                    QuestToolTipsController.controller.OnExit(questStatus);
                }
            });
            // 设置任务标题
            questTitle.text = questStatus.Title;
            // 设置任务目标
            foreach (QuestObjective questObjective in questStatus.GetObjectives())
            {
                // 生成并初始化侧栏任务目标
                QuestObjectiveUI tempQuestObjectiveUI = UIResourcesManager.Instance.LoadUserInterface(questSidebarObjectivePrefab, objectiveContainer).GetComponent<QuestObjectiveUI>();
                tempQuestObjectiveUI.SetQuestObjectiveSidebar(questStatus.GetProgress(questObjective), questObjective.TargetAmount, questObjective.Description);
                // 添加记录至数列中
                objectiveDic.Add(questObjective, tempQuestObjectiveUI);
            }
            // 判断任务是否完成 完成则清空 不完成则更新进度
            finishUI = UIResourcesManager.Instance.LoadUserInterface(questSidebarObjectivePrefab, objectiveContainer).GetComponent<QuestObjectiveUI>();
            // 生成并初始化侧栏任务目标
            finishUI.SetQuestObjectiveSidebar("已完成");
            finishUI.gameObject.SetActive(false);
        }

        public void Test(PlayerQuestStatus questStatus)
        {
            bool isQuestFinish = questStatus.IsFinish;
            SetCompleteObjectiveActive(!isQuestFinish);
            finishUI.gameObject.SetActive(isQuestFinish);
            if (!isQuestFinish)
            {
                UpdateState(questStatus);
            }
        }
        private void UpdateState(PlayerQuestStatus questStatus)
        {
            SetCompleteObjectiveActive(true);
            // 只更新进度
            foreach (QuestObjective questObjective in questStatus.GetObjectives())
            {
                // 生成并初始化侧栏任务目标
                objectiveDic[questObjective].SetQuestObjectiveSidebar(questStatus.GetProgress(questObjective), questObjective.TargetAmount, questObjective.Description);
            }
        }
        private void SetCompleteObjectiveActive(bool isActive)
        {
            foreach (var objectivePair in objectiveDic)
            {
                objectivePair.Value.gameObject.SetActive(isActive);
            }
        }
    }
}