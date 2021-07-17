using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Module;
using RPG.DialogueSystem.Graph;
using RPG.SaveSystem;

namespace RPG.QuestSystem
{
    public class PlayerQuestManager : BaseSingletonWithMono<PlayerQuestManager>, ISaveable
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
        public void AddOnQuestUpdateListener(Action onUpdateAction)
        {
            onQuestUpdate += onUpdateAction;
        }
        public void RemoveOnQuestUpdateListener(Action onUpdateAction)
        {
            onQuestUpdate -= onUpdateAction;
        }

        public void AddQuestObjectiveUpdateListener(Action onObjectiveUpdateAction)
        {
            onQuestObjectiveUpdate += onObjectiveUpdateAction;
        }
        public void RemoveQuestObjectiveUpdateListener(Action onObjectiveUpdateAction)
        {
            onQuestObjectiveUpdate -= onObjectiveUpdateAction;
        }
        public IEnumerable<PlayerQuestStatus> GetQuestStatuses()
        {
            return playerQuestStatuses;
        }
        
        public IEnumerable<PlayerQuestStatus> GetQuestStatuses<T>() where T : QuestSO
        {
            Dictionary<string, QuestSO> tempQuestSODic = GlobalResource.Instance.questDataBaseSO.questSODic;
            foreach (var playerQuestStatus in playerQuestStatuses.Where(playerQuestStatus => tempQuestSODic[playerQuestStatus.QuestUniqueID] is T))
            {
                yield return playerQuestStatus;
            }
        }
        private PlayerQuestStatus GetQuestStatus(QuestSO questSO)
        {
            return GetQuestStatuses()?.FirstOrDefault(status => status.QuestUniqueID == questSO.questUniqueID);
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
            foreach (var playerQuestStatus in playerQuestStatuses.Where(tempPlayerQuestStatus => tempQuestSODic[tempPlayerQuestStatus.QuestUniqueID] == removeQuest))
            {
                playerQuestStatus.PreDestroy();
                playerQuestStatuses.Remove(playerQuestStatus);
                break;
            }
            UpdateQuest();
        }
        
        public void KillQuestTrigger(string entityID)
        {
            // 若玩家有多个击杀同一个目标的任务 都会更新
            foreach (PlayerQuestStatus playerQuestStatus in GetQuestStatuses<KillQuestSO>())
            {
                playerQuestStatus.HandleReactiveQuestOnProgressListener(entityID);
            }
        }

        public bool HasQuest(QuestSO questSO)
        {
            return GetQuestStatus(questSO) != null;
        }

        public bool IsQuestComplete(QuestSO questSO)
        {
            PlayerQuestStatus questStatus = GetQuestStatus(questSO);
            return questStatus is {IsFinish: true} ;
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