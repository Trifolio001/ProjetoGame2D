using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int StarLife = 10;
    private int _currentLife;
    private bool _isDead = false;


    public FlashColor _flashcolor;
    public Action OnKill;

    private void Awake()
    {
        ConditionInitLife();
        if (_flashcolor == null)
        {
            _flashcolor = GetComponent<FlashColor>();
        }
    }

    public void ConditionInitLife()
    {
        _isDead = false;
        _currentLife = StarLife;
    }

    public void Damage(int damage)
    {
        if (_flashcolor != null)
        {
            _flashcolor.Flash();
        }
        if (_isDead) return;
        _currentLife -= damage;
        if (!_isDead) {
           
        if (_currentLife <= 0)
        {
            kill();
            _isDead = true;
            }
        }
    }


    public void kill()
    {
        if (_flashcolor != null)
        {
            _flashcolor.OnKill();
        }
        OnKill.Invoke(); 
    }

}
