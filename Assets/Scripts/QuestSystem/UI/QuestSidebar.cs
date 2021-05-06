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
            if (!GlobalResource.Instance.questDataBaseSO.questSODic.TryGetValue(questStatus.QuestUniqueID, out QuestSO questSO))
            {
                Debug.LogError("Cant Find Quest");
            }
            // 设置任务标题
            questTitle.text = questSO.questTitle;
            // 设置任务目标
            foreach (QuestObjective questObjective in questSO.GetObjectives())
            {
                // 生成并初始化侧栏任务目标
                QuestObjectiveUI tempQuestObjectiveUI = UIResourcesManager.Instance.LoadUserInterface(questSidebarObjectivePrefab, objectiveContainer).GetComponent<QuestObjectiveUI>();
                tempQuestObjectiveUI.SetQuestObjectiveSidebar(questStatus.GetProgress(questObjective), questObjective.Target, questObjective.Description);
                // 添加记录至数列中
                objectiveDic.Add(questObjective, tempQuestObjectiveUI);
            }
        }
        public void SetFinishState(PlayerQuestStatus questStatus)
        {
            // 清空所有任务目标UI
            Clear();
            // 判断任务是否完成 完成则清空 不完成则更新进度
            QuestObjectiveUI tempQuestObjectiveUI = UIResourcesManager.Instance.LoadUserInterface(questSidebarObjectivePrefab, objectiveContainer).GetComponent<QuestObjectiveUI>();
            // 生成并初始化侧栏任务目标
            tempQuestObjectiveUI.SetQuestObjectiveSidebar("已完成");
            // 添加记录至数列中
            objectiveDic.Add(new QuestObjective(), tempQuestObjectiveUI);
        }
        public void UpdateState(PlayerQuestStatus questStatus)
        {
            if (!GlobalResource.Instance.questDataBaseSO.questSODic.TryGetValue(questStatus.QuestUniqueID, out QuestSO questSO))
            {
                Debug.LogError("Cant Find Quest");
            }
            // 只更新进度
            foreach (QuestObjective questObjective in questSO.GetObjectives())
            {
                // 生成并初始化侧栏任务目标
                objectiveDic[questObjective].SetQuestObjectiveSidebar(questStatus.GetProgress(questObjective), questObjective.Target, questObjective.Description);
            }
        }
        public void Clear()
        {
            foreach (var objectivePair in objectiveDic)
            {
                Destroy(objectivePair.Value.gameObject);
            }
            objectiveDic.Clear();
        }
    }
}