using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Module;
using RPG.DialogueSystem.Graph;
using UnityTemplateProjects.Test;

namespace RPG.QuestSystem
{
    [System.Serializable]
    public class PlayerQuestStatus
    {
        [System.Serializable]
        public class ProgressPair
        {
            public int progress = default;      // 进度
            public bool isCompleted = default;  // 是否完成
        }
        [System.Serializable]
        public class QuestStoreInfo
        {
            public string questSOUniqueID = default;
            public Dictionary<string, ProgressPair> objectiveData = new Dictionary<string, ProgressPair>();
        }
        public bool IsFinish => isFinish;
        public string QuestUniqueID => questUniqueID;

        private string questUniqueID;         // 任务标识ID
        private bool isFinish;                  // 是否完成任务
        private Action onRemove;                // 销毁对象时回调

        private Dictionary<QuestObjective, ProgressPair> objectiveDic = new Dictionary<QuestObjective, ProgressPair>(); // 任务目标字典

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PlayerQuestStatus() {}
        
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="quest">任务SO</param>
        public PlayerQuestStatus(QuestSO quest)
        {
            InitPlayerQuestStatus(quest);
        }

        private void InitPlayerQuestStatus(QuestSO quest, Dictionary<string, ProgressPair> objectiveData = null)
        {
            questUniqueID = quest.questUniqueID;
            foreach (QuestObjective questObjective in quest.GetObjectives())
            {
                objectiveDic.Add(questObjective, objectiveData == null ? new ProgressPair() : objectiveData[questObjective.UniqueID]);
                // 处理主动任务监听
                HandleProactiveQuestOnProgressListener(questObjective);
            }
        }
        
        public object GetData()
        {
            QuestStoreInfo storeInfo = new QuestStoreInfo {questSOUniqueID = questUniqueID};
            foreach (var objectivePair in objectiveDic)
            {
                storeInfo.objectiveData.Add(objectivePair.Key.UniqueID, objectivePair.Value);
            }
            return storeInfo;
        }

        public void LoadData(object dataObj)
        {
            if (!(dataObj is QuestStoreInfo loadQuestStoreInfo))
            {
                Debug.LogError("LoadQuestStoreInfo Is Empty");
                return;
            }
            QuestSO questSO = GlobalResource.Instance.questDataBaseSO.questSODic[loadQuestStoreInfo.questSOUniqueID];
            InitPlayerQuestStatus(questSO, loadQuestStoreInfo.objectiveData);
            CheckFinish();
        }
        
        /// <summary>
        /// 预备销毁
        /// </summary>
        public void PreDestroy()
        {
            onRemove?.Invoke();
        }
        
        /// <summary>
        /// 获取当前任务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<QuestObjective> GetObjective()
        {
            foreach (var objectivePair in objectiveDic)
            {
                yield return objectivePair.Key;
            }
            // return objectiveDic.Select(objectivePair => objectivePair.Key);
        }

        /// <summary>
        /// 获取当前任务的进度
        /// </summary>
        /// <param name="questObjective"></param>
        /// <returns></returns>
        public int GetProgress(QuestObjective questObjective)
        {
            if (objectiveDic.ContainsKey(questObjective))
            {
                return objectiveDic[questObjective].progress;
            }
            return -1;
        }
        
        /// <summary>
        /// 处理被动任务监听
        /// </summary>
        /// <param name="entityID">实体ID</param>
        public void HandleReactiveQuestOnProgressListener(string entityID)
        {
            foreach (var questObjective in GetObjective())
            {
                // 判断被动任务监听的种类
                if (questObjective is KillQuestObjective killQuestObjective)
                {
                    // 由被杀死的怪物主动对玩家任务类进行询问
                    if (killQuestObjective.EntityID == entityID)
                    {
                        // 推进
                        OnProgress(questObjective);
                    }
                }
                else
                {
                    
                }
            }
        }

        /// <summary>
        /// 处理主动任务监听事件
        /// </summary>
        /// <param name="questObjective">任务目标</param>
        private void HandleProactiveQuestOnProgressListener(QuestObjective questObjective)
        { 
            // 判断任务类型
            if (questObjective is DialogueQuestObjective dialogueQuestObjective)
            {
                // 获取场景NPC信息字典
                TestDialogueListenerCenter.Instance.AddListener("KELM01", delegate { OnProgress(questObjective); });
                // var sceneNpcDic = GlobalResource.Instance.characterInfoDataBase.sceneCharacterInfoDic;
                // // 字典中存在该任务对话角色
                // if (sceneNpcDic.ContainsKey(dialogueQuestObjective.CharacterInfo))
                // {
                //     // 根据全局资源对话任务字典找到对应的NPC
                //     DialogueNPC questDialogueNpc = sceneNpcDic[dialogueQuestObjective.CharacterInfo].GetComponent<DialogueNPC>();
                //     // 往NPC身上添加监听事件
                //     questDialogueNpc.AddDialogueEnterEvent(dialogueQuestObjective.DialogueNode.UniqueID,
                //         dialogueQuestObjective.EventID,
                //         delegate { OnProgress(questObjective); });
                //     // 添加销毁回调 清空NPC身上监听事件
                //     onRemove += delegate
                //     {
                //         questDialogueNpc.RemoveDialogueEnterEvent(dialogueQuestObjective.DialogueNode.UniqueID,
                //             dialogueQuestObjective.EventID);
                //     };
                // }
            }
        }

        /// <summary>
        /// 任务推进
        /// </summary>
        /// <param name="questObjective"></param>
        /// <returns></returns>
        private void OnProgress(QuestObjective questObjective)
        {
            if (!objectiveDic.ContainsKey(questObjective)) return;
            ProgressPair pair = objectiveDic[questObjective];
            // 若任务目标已完成 则不做处理
            if (pair.isCompleted) return;
            // 任务目标未完成
            if (++pair.progress == questObjective.Target)
            {
                pair.isCompleted = true;
                // 检查整个任务是否已完成
                CheckFinish();
            }
            // 进度字典值发生改变 通知外部
            PlayerQuestManager.Instance.UpdateQuestObjective();
        }

        /// <summary>
        /// 检测整个任务是否完成
        /// </summary>
        private void CheckFinish()
        {
            foreach (var progressPair in objectiveDic)
            {
                // 只要有一个目标未完成 则整个任务未完成
                if (!progressPair.Value.isCompleted)
                {
                    isFinish = false;
                    return;
                }
            }
            // 任务完成
            isFinish = true;
        }
    }
}