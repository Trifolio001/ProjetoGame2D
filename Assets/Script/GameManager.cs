using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using generic.core.Singleton;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{

    [Header("Players")]
    public GameObject PlayerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;


    [Header("Reference")]
    public Transform StartPoint;


    [Header("Animation")] 
    public float duration = 0.2f;
    public float delay = 0.05f;
    public Ease ease = Ease.OutBack;

    private GameObject _CurrentPlayer;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        SpawPlayer();
    }

    private void SpawPlayer()
    {
        Debug.Log("akiiiii");
        _CurrentPlayer = Instantiate(PlayerPrefab);
        _CurrentPlayer.transform.position = StartPoint.transform.position;
        _CurrentPlayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delay);

    }
}
