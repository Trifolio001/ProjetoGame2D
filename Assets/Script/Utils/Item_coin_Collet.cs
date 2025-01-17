using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_coin_Collet : Colect_Base
{
    //public GameObject coinObj;

    protected override void OnCollect()
    {        

        base.OnCollect();
        Item_manager.Instance.AddCoin();
    }
}
