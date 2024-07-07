using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Vector2 PositionInicial;
    public BoxCollider2D boxCollider;
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
    public bool ConditionKill;
    public bool ConditionVictory;

    private float _CurrentSpeed;

    void Awake()
    {
        PositionInicial = transform.position;
        ConditionInitial();
    }

    public void ConditionInitial()
    {
        boxCollider.isTrigger = false;
        ConditionKill = (false);
        gameObject.SetActive(true); myRigidbody.transform.localScale = Vector2.one;
        DOTween.Kill(myRigidbody.transform);
        gameObject.transform.localPosition = PositionInicial;
        gameObject.transform.localRotation = Quaternion.Euler(Vector2.zero);
    }

    void Update()
    {
        Debug.Log(" sadfasf " + transform.rotation.z);
        HandleJump();
        HandleMoviment();
        ConfirmKill();
    }

    private void HandleMoviment()
    {

        if (!ConditionKill)
        {
            if (Input.GetKey(KeyCode.LeftShift))
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

            if ((myRigidbody.velocity.x > 0) && (myRigidbody.velocity.x < 0))
            {

            }
            else if (myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity -= friction;
            }
            else if (myRigidbody.velocity.x < 0)
            {
                myRigidbody.velocity += friction;
            }
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

    private void ConfirmKill()
    {
        if (ConditionKill) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (1 * Time.deltaTime), transform.position.z);
            myRigidbody.transform.DOScale(0.2f, 3).SetLoops(2, LoopType.Yoyo);
            //float a = transform.rotation.z + 10;
            //Debug.Log("   " + a + " sadfasf " + transform.rotation.z);
            transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime);
        }
    }

    public void KillPlayer()
    {
        myRigidbody.simulated = false;
        ConditionKill = true;
        boxCollider.isTrigger = true;
    }

    public void StopKillPlayer()
    {
        myRigidbody.simulated = true;
        ConditionKill = false;
    }


}