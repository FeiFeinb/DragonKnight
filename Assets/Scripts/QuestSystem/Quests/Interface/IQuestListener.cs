using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.QuestSystem
{
    public interface IQuestListener
    {
        /// <summary>
        /// 任务进度触发
        /// </summary>
        void OnQuestTrigger();
    }
}
