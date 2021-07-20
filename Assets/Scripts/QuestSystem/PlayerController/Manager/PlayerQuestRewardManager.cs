using UnityEngine;
using RPG.Module;
using RPG.InventorySystem;
using RPG.TradeSystem;
namespace RPG.QuestSystem
{
    public class PlayerQuestRewardManager : BaseSingleton<PlayerQuestRewardManager>
    {
        /// <summary>
        /// 发送任务奖励
        /// </summary>
        /// <param name="reward">奖励</param>
        /// <returns>发送是否成功</returns>
        public bool SendReward(QuestReward reward)
        {
            // 发送物品
            if (reward.itemObj)
            {
                ItemData rewardItem = new ItemData(reward.itemObj);
                int rewardAmount = reward.itemObjAmount;
                if (!PlayerInventoryManager.Instance.inventoryObject.AddWithCheck(rewardItem, rewardAmount))
                {
                    Debug.Log("背包已满 无法发放奖励");
                    return false;
                }
            }
            // 发送货币
            PlayerTradeManager.Instance.AddCoin(reward.coin);
            // TODO:  发送经验
            return true;
        }
    }
}