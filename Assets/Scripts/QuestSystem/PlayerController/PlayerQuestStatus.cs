using System.ComponentModel.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
using RPG.DialogueSystem;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class PlayerQuestStatus
    {
        public class ObjectivePair
        {
            public int progress = default;          // 进度
            public bool isCompleted = default;      // 是否完成
        }
        public QuestSO quest;
        public bool IsFinish => isFinish;
        [SerializeField] private bool isFinish;                                                                             // 是否完成任务
        private Dictionary<QuestObjective, ObjectivePair> progressDic = new Dictionary<QuestObjective, ObjectivePair>();    // 任务目标字典
        public PlayerQuestStatus(QuestSO _quest)
        {
            quest = _quest;
            foreach (QuestObjective questObjective in quest.GetObjectives())
            {
                progressDic.Add(questObjective, new ObjectivePair());
                AddOnProgressListener(questObjective);
            }
        }
        public IEnumerable<T> GetObjectiveOfType<T>() where T : QuestObjective
        {
            foreach (var questObjective in progressDic)
            {
                if (questObjective.Key is T)
                {
                    yield return questObjective.Key as T;
                }
            }
        }
        private void AddOnProgressListener(QuestObjective _questObjective)
        {
            if (_questObjective is DialogueQuestObjective)
            {
                DialogueQuestObjective dialogueQuestObjective = _questObjective as DialogueQuestObjective;
                var sceneNPCDic = GlobalResource.Instance.characterInfoDataBase.sceneCharacterInfoDic;
                if (sceneNPCDic.ContainsKey(dialogueQuestObjective.CharacterInfo))
                {
                    // 根据全局资源对话任务字典找到对应的NPC
                    sceneNPCDic[dialogueQuestObjective.CharacterInfo].GetComponent<DialogueNPC>().AddDialogueEvent(
                    dialogueQuestObjective.DialogueUniqueID, dialogueQuestObjective.EventID, delegate
                    {
                        OnProgress(_questObjective);
                    });
                }
                if (_questObjective is SendItemDialogueQuestObjective)
                {
                    SendItemDialogueQuestObjective send = _questObjective as SendItemDialogueQuestObjective;
                    
                }
            }
            // TODO: 填补击杀任务的处理情况
            else if (_questObjective is KillQuestObjective)
            {

            }
        }
        /// <summary>
        /// 获取当前任务的进度
        /// </summary>
        /// <param name="_questObjective"></param>
        /// <returns></returns>
        public int GetProgress(QuestObjective _questObjective)
        {
            if (progressDic.ContainsKey(_questObjective))
            {
                return progressDic[_questObjective].progress;
            }
            return -1;
        }

        /// <summary>
        /// 任务推进
        /// </summary>
        /// <param name="questObjective"></param>
        /// <returns></returns>
        private void OnProgress(QuestObjective questObjective)
        {
            if (progressDic.ContainsKey(questObjective))
            {
                ObjectivePair pair = progressDic[questObjective];
                if (pair.isCompleted) return;
                if (++pair.progress == questObjective.ObjectiveTarget)
                {
                    pair.isCompleted = true;
                    CheckFinish();
                }
                // 进度字典值发生改变 通知外部
                PlayerQuestManager.Instance.QuestObjectiveUpdate();
            }
        }

        /// <summary>
        /// 检测任务是否完成
        /// </summary>
        private void CheckFinish()
        {
            foreach (var progressPair in progressDic)
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
        public void KillQuestProgress(string _entityID)
        {
            // 由被杀死的怪物主动对玩家任务类进行询问
            foreach (var progressPair in GetObjectiveOfType<KillQuestObjective>())
            {
                if (progressPair.EntityID == _entityID)
                {
                    OnProgress(progressPair);
                }
            }
        }
    }
}

