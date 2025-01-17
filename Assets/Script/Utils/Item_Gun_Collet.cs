using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Gun_Collet : Colect_Base
{
    public UnityEvent eventCallback;

    
        //public GameObject coinObj;
        protected override void OnCollect()
        {
                
        eventCallback?.Invoke();
        
        base.OnCollect();
            //Item_manager.Instance.AddCoin();
        }
    
}
