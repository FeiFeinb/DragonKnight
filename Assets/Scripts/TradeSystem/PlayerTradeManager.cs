using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;

namespace RPG.TradeSystem
{
    public class PlayerTradeManager : BaseSingletonWithMono<PlayerTradeManager>
    {
        [SerializeField] private Coin coin; // 玩家上金币量

        public void AddCoin(Coin _coin)
        {
            coin.AddCopperCoin(_coin.copperCoin);
            coin.AddSilverCoin(_coin.silverCoin);
            coin.AddGoldCoin(_coin.goldCoin);
        }

        public void SubCoin(Coin _coin)
        {
            // 金币数量的进制
            coin.SubCopperCoin(_coin.copperCoin);
            coin.SubSilverCoin(_coin.silverCoin);
            coin.SubGoldCoin(_coin.goldCoin);
        }

        private void Update()
        {
            Debug.Log(string.Concat(coin.goldCoin, "金", coin.silverCoin, "银", coin.copperCoin, "铜"));
        }
    }
}