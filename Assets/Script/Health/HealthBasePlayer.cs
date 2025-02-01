using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasePlayer : MonoBehaviour
{
    [Header("setup")]
    public SOPlayerSetup soPlayerSetup;
    public PlayerTest player;
    public bool DestroyOnKill = false;
    public float dalayToKill = 0f;

    public GameObject Menu;
    public GameObject MenuConditionVictory;
    public GameObject MenuConditionLoser;

    private int _currentLife;
    private bool _isDead = false;

    public FlashColor _flashcolor;


    private void Awake()
    {
        ConditionInitLife();
    }

    private void Start()
    {
        Invoke(nameof(listColor), 1f);
    }

    private void listColor()
    {
        if (_flashcolor == null)
        {
            _flashcolor = GetComponentInChildren<FlashColor>();
        }
    }

    public void ConditionInitLife()
    {
        _isDead = false;
        _currentLife = soPlayerSetup.Startlife;
        Menu.SetActive(false);
        MenuConditionVictory.SetActive(false);
        MenuConditionLoser.SetActive(false);
    }

    public void Damage(int damage)
    {
        if (_flashcolor != null)
        {
            _flashcolor.Flash();
        }
        if (_isDead) return;
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            kill();
        }
    }

    public void VictoryPlayer()
    {
        Debug.Log("passou aki");
        Menu.SetActive(true);
        MenuConditionVictory.SetActive(true);
        MenuConditionLoser.SetActive(false);
    }

    public void kill()
    {
        _isDead = true;

        if (DestroyOnKill)
        {
            StartCoroutine(AnimationKill());
        }
    }

    IEnumerator AnimationKill()
    {
        player.KillPlayer();
        yield return new WaitForSeconds(2);
        Menu.SetActive(true);
        MenuConditionVictory.SetActive(false);
        MenuConditionLoser.SetActive(true);
    }
}
