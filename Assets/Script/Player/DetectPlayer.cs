using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject player;


    void OnTriggerEnter2D(Collider2D other)
    {
        var Enimy = other.gameObject.GetComponent<EnimyControl>();

        if (Enimy != null)
        {
            Enimy.ActiveAttack(player);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var Enimy = other.gameObject.GetComponent<EnimyControl>();

        if (Enimy != null)
        {
            Enimy.DesactiveAttack();
        }

    }
}
