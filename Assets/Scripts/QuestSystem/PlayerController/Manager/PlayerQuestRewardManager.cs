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
            for (int i = 0; i < reward.itemObjAmount; i++)
            {
                ItemData newItem = new ItemData(reward.itemObj);
                newItem.itemBuffs.RenerateValues();
                if (!PlayerInventoryManager.Instance.inventoryObject.AddItem(newItem, 1))
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