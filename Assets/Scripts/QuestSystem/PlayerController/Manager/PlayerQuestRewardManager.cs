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
            // TODO: 解决背包满载时应处理的情况
            for (int i = 0; i < reward.itemObjAmount; i++)
            {
                if (!PlayerInventoryManager.Instance.inventoryObject.AddItem(new ItemData(reward.itemObj), 1))
                {
                    Debug.Log("背包已满");
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