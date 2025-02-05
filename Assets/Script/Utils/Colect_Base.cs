using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colect_Base : MonoBehaviour
{

    public string compareTag = "Player";

    public ParticleSystem particlesystem;
    public float timeToHide = 2;
    public GameObject graphicItem;

    private bool capture = false;

    private void Awake()
    {
        capture = false;
       /* if (particlesystem != null)
        {
            particlesystem.transform.SetParent(null);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        if (graphicItem != null) { 
            graphicItem.SetActive(false); 
        }
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        if ((particlesystem != null) && (!capture))
        {
            particlesystem.Play();
            capture = true;
        }
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

}
