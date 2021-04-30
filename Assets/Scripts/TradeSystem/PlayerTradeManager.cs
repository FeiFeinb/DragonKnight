using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
using RPG.Utility;

namespace RPG.TradeSystem
{
    public class PlayerTradeManager : BaseSingletonWithMono<PlayerTradeManager>, IListenable<string>
    {
        [SerializeField] public Coin coin;          // 玩家上金币量
        private Action<string> onCoinUpdate;        // 货币数量变更事件
        // TODO: 对货币的保存
        public void AddCoin(Coin _coin)
        {
            coin.AddCopperCoin(_coin.copperCoin);
            coin.AddSilverCoin(_coin.silverCoin);
            coin.AddGoldCoin(_coin.goldCoin);
            onCoinUpdate?.Invoke(_coin.coinStr);
        }

        public bool SubCoin(Coin _coin)
        {
            if (_coin.value > coin.value)
            {
                Debug.LogError("超出金额");
                return false;
            }
            // 金币数量的进制
            coin.SubCopperCoin(_coin.copperCoin);
            coin.SubSilverCoin(_coin.silverCoin);
            coin.SubGoldCoin(_coin.goldCoin);
            onCoinUpdate?.Invoke(_coin.coinStr);
            return true;
        }

        public void AddListener(Action<string> action)
        {
            onCoinUpdate += action;
        }

        public void RemoveListener(Action<string> action)
        {
            onCoinUpdate -= action;
        }
    }
}