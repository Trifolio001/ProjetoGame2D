using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;
    public float timedestroy = 2f;
    public float side = 1;
    public int damage = 1;


    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void Awake()
    {
        Destroy(gameObject, timedestroy);
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
            enimy.Damage(damage);
            Destroy(gameObject);
        }
    }

}
