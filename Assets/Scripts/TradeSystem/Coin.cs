using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace RPG.TradeSystem
{
    [System.Serializable]
    public class Coin
    {
        public bool IsZero
        {
            get
            {
                // 判断是否为0
                return goldCoin == 0 && silverCoin == 0 && copperCoin == 0;
            }
        }

        public int value
        {
            get
            {
                return goldCoin * baseScale * baseScale + silverCoin * baseScale + copperCoin;
            }
        }

        public string coinStr
        {
            get
            {
                return $"{goldCoin} 金 {silverCoin} 银 {copperCoin} 铜";
            }
        }
        private const int baseScale = 100;     // 进制
        public int goldCoin;            // 金币
        public int silverCoin;          // 银币
        public int copperCoin;          // 铜币
        public Coin(Coin coin)
        {
            goldCoin = coin.goldCoin;
            silverCoin = coin.silverCoin;
            copperCoin = coin.copperCoin;
        }

        public Coin(int _goldCoin, int _silverCoin, int _copperCoin)
        {
            goldCoin = _goldCoin;
            silverCoin = _silverCoin;
            copperCoin = _copperCoin;
        }

        public void AddGoldCoin(int _goldCoin)
        {
            goldCoin += _goldCoin;
        }

        public void AddSilverCoin(int _silverCoin)
        {
            int addValue = silverCoin + _silverCoin;
            if (addValue >= baseScale)
            {
                silverCoin = addValue - baseScale;
                AddGoldCoin(1);
            }
            else
            {
                silverCoin = addValue;
            }
        }

        public void AddCopperCoin(int _copperCoin)
        {
            int addValue = copperCoin + _copperCoin;
            if (addValue >= baseScale)
            {
                copperCoin = addValue - baseScale;
                AddSilverCoin(1);
            }
            else
            {
                copperCoin = addValue;
            }
        }

        public void SubGoldCoin(int _goldCoin)
        {
            goldCoin -= _goldCoin;
        }

        public void SubSilverCoin(int _silverCoin)
        {
            int subValue = silverCoin - _silverCoin;
            if (subValue < 0)
            {
                silverCoin = baseScale + subValue;
                SubGoldCoin(1);
            }
            else
            {
                silverCoin = subValue;
            }
        }

        public void SubCopperCoin(int _copperCoin)
        {
            int subValue = copperCoin - _copperCoin;
            if (subValue < 0)
            {
                copperCoin = baseScale + subValue;
                SubSilverCoin(1);
            }
            else
            {
                copperCoin = subValue;
            }
        }
        
        
    }
}

