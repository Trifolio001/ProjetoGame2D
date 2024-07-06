using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Vector2 friction = new Vector2(0.1f, 0);
    public Vector2 Position = new Vector2(0, 0);
    public float limitMin = -5.0f;
    public float limitMax = 5.0f;
    public float speed = 10.0f;
    //public float deltaTime;
    private bool Booltimer = true;


    void Awake()
    {
        gameObject.transform.position = Position;
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 newPosition = transform.position + Vector3.up * 10 * Time.deltaTime;
        // Calcula a nova posição X
        float novaPosicaoX = transform.position.x + (speed * Time.deltaTime);
        novaPosicaoX = Mathf.Clamp(novaPosicaoX, limitMin, limitMax);
        if (Booltimer)
        {

            if ((gameObject.transform.position.x <= limitMin) || (gameObject.transform.position.x >= limitMax))
            {
                Booltimer = false;
                speed = -1 * speed;
                StartCoroutine(DelayCall());
            }
        }
        transform.position = new Vector3(novaPosicaoX, transform.position.y, transform.position.z);

    }

    IEnumerator DelayCall() {

        yield return new WaitForSeconds(1f);
        Booltimer = true;
    } 
}


