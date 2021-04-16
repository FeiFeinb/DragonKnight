using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.QuestSystem
{
    public class QuestToolTipsReward : MonoBehaviour
    {
        [SerializeField] private Text questRewardText;   // 奖励数目
        public void SetQuestToolTipsReward(string questRewardStr)
        {
            questRewardText.text = questRewardStr;
        }
    }

}
