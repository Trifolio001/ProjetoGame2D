using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using generic.core.Singleton;
using TMPro;

public class Item_manager : Singleton<Item_manager>
{

    //public static Item_manager Instance;
    public int coins = 0;
    public TextMeshProUGUI textNumCoin;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoin(int ammount = 1)
    {
        coins += ammount;
        textNumCoin.text = ""+coins;
    }


}
