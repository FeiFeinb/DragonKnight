using System;
using System.Collections;
using System.Collections.Generic;
using RPG.InventorySystem;
using UnityEngine;
using RPG.Module;
using RPG.SaveSystem;
using RPG.Utility;

namespace RPG.TradeSystem
{
    public class PlayerTradeManager : BaseSingletonWithMono<PlayerTradeManager>, ISaveable
    {
        [SerializeField] private Coin coin;          // 玩家上金币量
        private Action<string> onCoinUpdate;        // 货币数量变更事件

        public void AddCoin(Coin _coin)
        {
            // 金币累加计算
            coin.AddCopperCoin(_coin.copperCoin);
            coin.AddSilverCoin(_coin.silverCoin);
            coin.AddGoldCoin(_coin.goldCoin);
            // 调用事件
            onCoinUpdate?.Invoke(coin.coinStr);
        }

        public bool SubCoin(Coin _coin)
        {
            if (_coin.value > coin.value)
            {
                Debug.LogError("超出金额");
                return false;
            }

            // 金币减法计算
            coin.SubCopperCoin(_coin.copperCoin);
            coin.SubSilverCoin(_coin.silverCoin);
            coin.SubGoldCoin(_coin.goldCoin);
            // 调用事件
            onCoinUpdate?.Invoke(coin.coinStr);
            return true;
        }

        public bool BuyItem(BaseItemObject itemObj)
        {
            if (PlayerInventoryManager.Instance.inventoryObject.EmptySlotNum == 0 || !SubCoin(itemObj.sellPrice)) return false;
            // 成功购买物品
            return PlayerInventoryManager.Instance.inventoryObject.AddItem(new ItemData(itemObj), 1) == null;
        }

        public void AddOnCoinUpdateListener(Action<string> action)
        {
            onCoinUpdate += action;
        }

        public void RemoveOnCoinUpdateListener(Action<string> action)
        {
            onCoinUpdate -= action;
        }

        public object CreateState()
        {
            return coin as object;
        }

        public void LoadState(object stateInfo)
        {
            Coin tempCoin = stateInfo as Coin;
            if (tempCoin == null)
            {
                Debug.LogError("Cant Load State -- PlayerCoin");
                return;
            }

            coin = tempCoin;
            onCoinUpdate?.Invoke(coin.coinStr);
        }

        public void ResetState()
        {
        }
    }
}