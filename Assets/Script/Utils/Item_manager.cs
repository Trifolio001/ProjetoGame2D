using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using generic.core.Singleton;
using TMPro;

public class Item_manager : Singleton<Item_manager>
{

    public SOint coins;
    //public TextMeshProUGUI textNumCoin;

    private void Reset()
    {
        coins.Value = 0;
        UpdateUi();
    }

    public void AddCoin(int ammount = 1)
    {
        coins.Value += ammount;
        UpdateUi();
    }

    private void UpdateUi()
    {
        //UIGameManeger.Instance.UpdateTextCoins(coins.Value.ToString());
    }

}
