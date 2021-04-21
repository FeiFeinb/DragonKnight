using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
using RPG.DialogueSystem;
namespace RPG.QuestSystem
{
    public class PlayerQuestManager : BaseSingletonWithMono<PlayerQuestManager>, IPredicateEvaluators
    {
        [SerializeField] private List<PlayerQuestStatus> playerQuestStatuses = new List<PlayerQuestStatus>();     // 玩家任务数列
        private Action onQuestUpdate;              // 任务更新
        private Action onQuestObjectiveUpdate;     //  任务目标更新
        public void UpdateQuest()
        {
            onQuestUpdate?.Invoke();
        }
        public void QuestObjectiveUpdate()
        {
            onQuestObjectiveUpdate?.Invoke();
        }
        public void AddOnQuestUpdateListener(Action _onUpdate)
        {
            onQuestUpdate += _onUpdate;
        }
        public void RemoveOnQuestUpdateListener(Action _onUpdate)
        {
            onQuestUpdate -= _onUpdate;
        }

        public void AddQuestObjectiveUpdateListener(Action _onObjectiveUpdate)
        {
            onQuestObjectiveUpdate += _onObjectiveUpdate;
        }
        public void RemoveQuestObjectiveUpdateListener(Action _onObjectiveUpdate)
        {
            onQuestObjectiveUpdate -= _onObjectiveUpdate;
        }
        public IEnumerable<PlayerQuestStatus> GetQuestStatuses()
        {
            foreach (var playerQuestStatus in playerQuestStatuses)
            {
                yield return playerQuestStatus;
            }
        }
        public IEnumerable<PlayerQuestStatus> GetQuestStatuses<T>() where T : QuestSO
        {
            foreach (var playerQuestStatus in playerQuestStatuses)
            {
                if (playerQuestStatus.quest is T)
                {
                    yield return playerQuestStatus;
                }
            }
        }
        public PlayerQuestStatus GetQuestStatus(string questTitle)
        {
            foreach (PlayerQuestStatus status in playerQuestStatuses)
            {
                if (status.quest.questTitle == questTitle)
                {
                    return status;
                }
            }
            return null;
        }
        public void AddQuest(QuestSO addQuest)
        {
            playerQuestStatuses.Add(new PlayerQuestStatus(addQuest));
            UpdateQuest();
        }
        public void RemoveQuest(QuestSO removeQuest)
        {
            // 查找符合条件的Status
            for (int i = 0; i < playerQuestStatuses.Count; i++)
            {
                if (playerQuestStatuses[i].quest == removeQuest)
                {
                    // 移除
                    playerQuestStatuses.RemoveAt(i);
                    break;
                }
            }
            UpdateQuest();
        }
        public void KillQuestTrigger(string _entityID)
        {
            // 若玩家有多个击杀同一个目标的任务 都会更新
            foreach (PlayerQuestStatus playerQuestStatus in GetQuestStatuses<KillQuestSO>())
            {
                playerQuestStatus.KillQuestProgress(_entityID);
            }
        }

        public bool? Evaluator(DialogueConditionType predicate, string parameters)
        {
            switch (predicate)
            {
                // 任务类型Condition
                // TODO: 更改Condition检测QuestTitle
                case DialogueConditionType.HasQuest:
                    return (GetQuestStatus(parameters) != null);
                case DialogueConditionType.CompleteQuest:
                    return GetQuestStatus(parameters).IsFinish;
            }
            // 此类型不具备任何Condition
            return null;
        }
    }

}