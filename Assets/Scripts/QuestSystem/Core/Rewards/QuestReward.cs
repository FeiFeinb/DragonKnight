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
        public BaseItemObject itemObj;  // 奖励物品
        public int itemObjAmount;       // 奖励物品数量
        public string CoinStr
        {
            get
            {
                return coin.coinStr;
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
                return $"{itemObj.name} x{itemObjAmount}";
            }
        }
    }
}