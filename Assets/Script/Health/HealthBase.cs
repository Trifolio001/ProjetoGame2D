using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int StarLife = 10;
    public bool DestroyOnKill = false;
    public float dalayToKill = 0f;
    private int _currentLife;
    private bool _isDead = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = StarLife;
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

    public void kill()
    {
        _isDead = true;

        if (DestroyOnKill)
        {
            Destroy(gameObject, dalayToKill);
        }
    }
}
