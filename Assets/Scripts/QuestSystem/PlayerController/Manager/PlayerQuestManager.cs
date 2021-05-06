using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Module;
using RPG.DialogueSystem;
using RPG.SaveSystem;
using UnityEngine.TextCore.LowLevel;

namespace RPG.QuestSystem
{
    public class PlayerQuestManager : BaseSingletonWithMono<PlayerQuestManager>, IPredicateEvaluators, ISaveable
    {
        public QuestSO testKillQuest;

        [ContextMenu("AddTestKillQuest")]
        public void AddTestKillQuest()
        {
            AddQuest(testKillQuest);
        }
        [SerializeField] private List<PlayerQuestStatus> playerQuestStatuses = new List<PlayerQuestStatus>();     // 玩家任务数列
        private Action onQuestUpdate;              // 任务更新
        private Action onQuestObjectiveUpdate;     //  任务目标更新
        public void UpdateQuest()
        {
            onQuestUpdate?.Invoke();
        }
        public void UpdateQuestObjective()
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
            Dictionary<string, QuestSO> tempQuestSODic = GlobalResource.Instance.questDataBaseSO.questSODic;
            foreach (var playerQuestStatus in playerQuestStatuses)
            {
                if (tempQuestSODic[playerQuestStatus.QuestUniqueID] is T)
                {
                    yield return playerQuestStatus;
                }
            }
        }
        private PlayerQuestStatus GetQuestStatus(QuestSO questSO)
        {
            foreach (PlayerQuestStatus status in GetQuestStatuses())
            {
                if (status.QuestUniqueID == questSO.questUniqueID)
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
            Dictionary<string, QuestSO> tempQuestSODic = GlobalResource.Instance.questDataBaseSO.questSODic;
            // 查找符合条件的Status
            foreach (PlayerQuestStatus playerQuestStatus in playerQuestStatuses)
            {
                if (tempQuestSODic[playerQuestStatus.QuestUniqueID] != removeQuest) continue;
                playerQuestStatus.PreDestroy();
                playerQuestStatuses.Remove(playerQuestStatus);
                break;
            }
            UpdateQuest();
        }
        
        public void KillQuestTrigger(string _entityID)
        {
            // 若玩家有多个击杀同一个目标的任务 都会更新
            foreach (PlayerQuestStatus playerQuestStatus in GetQuestStatuses<KillQuestSO>())
            {
                playerQuestStatus.HandleReactiveQuestOnProgressListener(_entityID);
            }
        }

        public bool? Evaluator(DialogueConditionType predicate, ScriptableObject paramSO)
        {
            QuestSO questParamSO = paramSO as QuestSO;
            switch (predicate)
            {
                // 任务类型Condition
                case DialogueConditionType.HasQuest:
                    return (GetQuestStatus(questParamSO) != null);
                case DialogueConditionType.CompleteQuest:
                    return GetQuestStatus(questParamSO).IsFinish;
            }
            // 此类型不具备任何Condition
            return null;
        }

        public object CreateState()
        {
            return playerQuestStatuses.Select(playerQuestStatus => playerQuestStatus.GetData()).ToList();
        }

        public void LoadState(object stateInfo)
        {
            if (!(stateInfo is List<object> saveQuestStoreInfos))
            {
                Debug.LogError("Cant Load State -- PlayerQuest");
                return;
            }
            foreach (object saveQuestStoreInfo in saveQuestStoreInfos)
            {
                PlayerQuestStatus emptyPlayerQuestStatus = new PlayerQuestStatus();
                emptyPlayerQuestStatus.LoadData(saveQuestStoreInfo);
                playerQuestStatuses.Add(emptyPlayerQuestStatus);
            }
            UpdateQuest();
            UpdateQuestObjective();
        }

        public void ResetState()
        {
            // 调用预清空方法
            foreach (PlayerQuestStatus playerQuestStatus in playerQuestStatuses)
            {
                playerQuestStatus.PreDestroy();
            }
            // 清空数组
            playerQuestStatuses.Clear();
        }

        private void OnApplicationQuit()
        {
            // 退出游戏前清空监听
            ResetState();
        }
    }

}