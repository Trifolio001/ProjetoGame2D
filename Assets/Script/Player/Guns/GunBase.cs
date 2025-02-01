using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjetil;
    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public Transform PlayerSideReference;

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        PlayerSideReference = GetComponentInParent<PlayerTest>().gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }else if (Input.GetKeyUp(KeyCode.Q))
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    private void Shoot()
    {
        var projectile = Instantiate(prefabProjetil);
        projectile.transform.position = positionToShoot.position;
        projectile.side = PlayerSideReference.transform.localScale.x;
    }
}
