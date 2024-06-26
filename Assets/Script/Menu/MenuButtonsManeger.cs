using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManeger : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    public float duration = 0.2f;
    public float delay = 0.05f;
    public  Ease ease = Ease.OutBack;

    public void OnEnable()
    {
        HideAllButtons();
        showButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    private void showButtons()
    {
        Debug.Log("valor " + buttons.Count);
        for(int i = 0; i < buttons.Count; i++)
        {
            Debug.Log("contando = " + i);
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i * delay).SetEase(ease);
        }
    }
}
