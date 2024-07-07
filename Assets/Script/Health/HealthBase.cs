using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Player player;
    public int StarLife = 10;
    public bool DestroyOnKill = false;
    public float dalayToKill = 0f;

    public GameObject Menu;
    public GameObject MenuConditionVictory;
    public GameObject MenuConditionLoser;

    private int _currentLife;
    private bool _isDead = false;

    private void Awake()
    {
        ConditionInitLife();
    }

    public void ConditionInitLife()
    {
        _isDead = false;
        _currentLife = StarLife;
        Menu.SetActive(false);
        MenuConditionVictory.SetActive(false);
        MenuConditionLoser.SetActive(false);
}

    public void Damage(int damage)
    {
        if (_isDead) return;
        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            kill();
        }
    }

    public void VictoryPlayer()
    {
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
        player.StopKillPlayer();
        gameObject.SetActive(false);
        Menu.SetActive(true);
        MenuConditionVictory.SetActive(false);
        MenuConditionLoser.SetActive(true);
    }
}
