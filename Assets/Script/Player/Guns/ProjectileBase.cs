using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public SOBullet soBullet;
    public float side = 2;


    void Update()
    {
        Vector3 direction = new Vector3(-soBullet.velocity, 0, 0);
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void Awake()
    {
        Destroy(gameObject, soBullet.timedestroy);
    }

    private void Start()
    {
        transform.localScale = new Vector3(side, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enimy = collision.transform.GetComponent<HealthBase>();

        if(enimy != null)
        {
            VFXBullet();
            enimy.Damage(soBullet.damage);
            Destroy(gameObject);
        }
    }

    public void VFXBullet()
    {
        VFXManeger.Instance.PlayVFXByTipe(VFXManeger.VFXType.BulletEfect, transform.position, -transform.localScale.x);

    }

}
