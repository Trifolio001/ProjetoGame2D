using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    public HealthBase healtBase;

    private void Awake()
    {
        if(healtBase != null)
        {
            healtBase.OnKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        healtBase.OnKill -= OnEnemyKill;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBasePlayer>();

        if (health != null)
        {
            health.Damage(damage);
        }
    }


    public void Damage(int dano)
    {
        healtBase.Damage(dano);
    }
}
