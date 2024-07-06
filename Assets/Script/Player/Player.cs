using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public GameObject Teste1;
    public Rigidbody2D myRigidbody; 

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forcejump = 2;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.5f;
    public float animationduration = 2;
    public Ease ease = Ease.OutBack;

    private float _CurrentSpeed;
    private bool Plataform = false;
    private BoxCollider2D boxCollider;

    void Update()
    {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _CurrentSpeed = speedRun;
        }
        else
        {
            _CurrentSpeed = speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.velocity = new Vector2(-_CurrentSpeed, myRigidbody.velocity.y);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(_CurrentSpeed, myRigidbody.velocity.y);

        }

        if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= friction;
        }
        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boxCollider = GetComponent<BoxCollider2D>();

            ContactPoint2D[] contacts = new ContactPoint2D[10];
            int numContacts = boxCollider.GetContacts(contacts);

            for (int i = 0; i < numContacts; i++)
            {
                Collider2D otherCollider = contacts[i].collider;

                if (otherCollider.gameObject.CompareTag("Plataform"))
                {

                    myRigidbody.velocity = Vector2.up * forcejump;
                    myRigidbody.transform.localScale = Vector2.one;

                    DOTween.Kill(myRigidbody.transform);

                    HandleScaleJump();
                }
            }
        }
        
    }

    private void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleY(jumpScaleY, animationduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX, animationduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }



}
