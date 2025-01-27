using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float duration = .001f;

    private Tween _curretTween;
    private bool Referencetime = false;
    private int timeMili = 0;

    private void OnValidate()
    {
        Referencetime = true;
        spriteRenderers = new List<SpriteRenderer>();
        foreach(var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
               
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Flash();
            Referencetime = true;
        }
    }
    */


    public void Flash()
    {
        /*Debug.Log(_curretTween);
        if (_curretTween != null)
        {
            Debug.Log("passo");
            spriteRenderers.ForEach(i => i.color = Color.white);
            _curretTween.Kill();
        }*/

        //Debug.Log(timeMili + " = e = " + Referencetime);

        if(Referencetime)
        {
            foreach (var s in spriteRenderers)
            {
                s.DOColor(color, duration).SetLoops(1, LoopType.Yoyo);
            }
            timeMili = 10;
            Referencetime = false;
            Invoke(nameof(OperaçãodeTempo), duration+0.1f);
        }
        
    }

    public void OperaçãodeTempo()
    {
        spriteRenderers.ForEach(i => i.color = Color.white);
        Referencetime = true;
    }
}
