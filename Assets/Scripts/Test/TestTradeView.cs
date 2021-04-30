using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.TradeSystem;

public class TestTradeView : MonoBehaviour
{

    [SerializeField] private Text currentCoin;

    private int goldModify;
    private int silverModify;
    private int copperModify;

    public void OnGoldInputChange(string value)
    {
        int.TryParse(value, out goldModify);
    }

    public void OnSilverInputChange(string value)
    {
        int.TryParse(value, out silverModify);
    }

    public void OnCopperInputChange(string value)
    {
        int.TryParse(value, out copperModify);
    }

    public void OnAddCoin()
    {
        PlayerTradeManager.Instance.AddCoin(new Coin(goldModify, silverModify, copperModify));
    }

    public void OnSubCoin()
    {
        PlayerTradeManager.Instance.SubCoin(new Coin(goldModify, silverModify, copperModify));
    }
    private void Update()
    {
        currentCoin.text = (new Coin(goldModify, silverModify, copperModify)).coinStr;
    }
}