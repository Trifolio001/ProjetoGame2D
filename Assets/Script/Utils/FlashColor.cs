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

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach(var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
               
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Flash();
        }
    }

    public void Flash()
    {

        if(_curretTween != null)
        {
            spriteRenderers.ForEach(i => i.color = Color.white);
            _curretTween.Kill();
        }

        foreach(var s in spriteRenderers)
        {
            _curretTween = s.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
        }
    }

}
