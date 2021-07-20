using System;
using System.Collections;
using System.Collections.Generic;
using RPG.InventorySystem;
using UnityEngine;
using RPG.Module;
using RPG.SaveSystem;
using RPG.Utility;
using UnityEditor;

namespace RPG.TradeSystem
{
    public class PlayerTradeManager : BaseSingletonWithMono<PlayerTradeManager>, ISaveable
    {
        [SerializeField] private Coin _coin;          // 玩家上金币量
        
        private Action<string> _onCoinUpdate;        // 货币数量变更事件

        public void AddCoin(Coin coin)
        {
            // 金币累加计算
            _coin.AddCopperCoin(coin.copperCoin);
            _coin.AddSilverCoin(coin.silverCoin);
            _coin.AddGoldCoin(coin.goldCoin);
            // 调用事件
            _onCoinUpdate?.Invoke(_coin.coinStr);
        }
        
        private void SubCoin(Coin coin)
        {
            // 金币减法计算
            _coin.SubCopperCoin(coin.copperCoin);
            _coin.SubSilverCoin(coin.silverCoin);
            _coin.SubGoldCoin(coin.goldCoin);
            // 调用事件
            _onCoinUpdate?.Invoke(_coin.coinStr);
        }

        public bool IsCoinEnough(Coin coin)
        {
            return _coin.value > coin.value;
        }
        
        public bool BuyItem(BaseItemObject itemObj)
        {
            int buyAmount = 1;
            Coin sellPrice = itemObj.sellPrice;
            
            if (IsCoinEnough(sellPrice) && PlayerInventoryManager.Instance.inventoryObject.AddWithCheck(new ItemData(itemObj), buyAmount))
            {
                // 成功购买物品
                SubCoin(sellPrice);
                return true;
            }
            return false;
        }

        public void AddOnCoinUpdateListener(Action<string> action)
        {
            _onCoinUpdate += action;
        }

        public void RemoveOnCoinUpdateListener(Action<string> action)
        {
            _onCoinUpdate -= action;
        }

        public object CreateState()
        {
            return _coin as object;
        }

        public void LoadState(object stateInfo)
        {
            Coin tempCoin = stateInfo as Coin;
            if (tempCoin == null)
            {
                Debug.LogError("Cant Load State -- PlayerCoin");
                return;
            }

            _coin = tempCoin;
            _onCoinUpdate?.Invoke(_coin.coinStr);
        }

        public void ResetState()
        {
        }
    }
}