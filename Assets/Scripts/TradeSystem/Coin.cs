using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.TradeSystem
{
    [System.Serializable]
    public class Coin
    {
        public bool isEmpty
        {
            get
            {
                return goldCoin == 0 && silverCoin == 0 && copperCoin == 0;
            }
        }
        public int goldCoin;
        public int silverCoin;
        public int copperCoin;

    }
}

