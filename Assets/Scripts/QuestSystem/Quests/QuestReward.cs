using System.Text;
using RPG.TradeSystem;
using RPG.InventorySystem;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class QuestReward
    {
        public Coin coin = default;     // 奖励金币
        public int experience = 0;      // 奖励经验
        public int itemID = -1;         // 奖励物品
        public BaseItemObject itemObj;  // 奖励物品
        public int itemObjAmount;       // 奖励物品数量
        public string CoinStr
        {
            get
            {
                return $"{coin.goldCoin}金 {coin.silverCoin}银 {coin.copperCoin}铜";
            }
        }
        public string ExperienceStr
        {
            get
            {
                return $"{experience}点经验";
            }
        }
        public string ItemIDStr
        {
            get
            {
                return $"{itemID}号物品";
            }
        }
    }
}