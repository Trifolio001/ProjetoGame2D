using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public Vector2 PositionInicial;
    public BoxCollider2D boxCollider;
    public Rigidbody2D myRigidbody;
    public float ScalePlayer;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forcejump = 2;

    [Header("Animation setup")]
    public string boolRun = "IsRun";
    public string boolWalk = "IsWalk";
    public string boolJump = "IsJump";
    public string boolJumpDown = "IsJumpDown";
    public string TrigerJump = "IsJumpTrig";
    public string TrigerKillPlayer = "IsKillPlayer";
    public bool InPlataform = false;
    public Animator animation;
    public Transform DetectPlataform;
    public LayerMask MaskPlataform;

    //public float jumpScaleY = 1.5f;
    //public float jumpScaleX = 0.5f;
    //public float animationduration = 2;
    //public Ease ease = Ease.OutBack;
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
        //animation TrigerReset = false;
        //animation.SetBool(TrigerReset, false);
        boxCollider.isTrigger = false;
        ConditionKill = (false);
        gameObject.SetActive(true); 
        myRigidbody.transform.localScale = new Vector2(ScalePlayer, ScalePlayer);
        RespawPlayerGame();
        gameObject.transform.localPosition = PositionInicial;
        gameObject.transform.localRotation = Quaternion.Euler(Vector2.zero);
        animation.SetBool(TrigerJump, true);
    }

    void Update()
    {

        if (!ConditionKill)
        {
            InPlataform = Physics2D.OverlapCircle(DetectPlataform.position, 0.5f, MaskPlataform);
            if (InPlataform)
            {
               animation.SetBool(boolJump, false);
            }
            else
            {
                if (animation.GetBool(boolJump) == false)
                {
                animation.SetBool(TrigerJump, true);
                }
                animation.SetBool(boolJump, true);
            }
            if(myRigidbody.velocity.y <= 0)
            {
                animation.SetBool(boolJumpDown, true);
            }
            else
            {
                animation.SetBool(boolJumpDown, false);
            }
            HandleJump();
            HandleMoviment();
        }
    }

    private void HandleMoviment()
    {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _CurrentSpeed = speedRun;
                animation.SetBool(boolRun, true);
            }
            else
            {
                _CurrentSpeed = speed;
                animation.SetBool(boolRun, false);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody.velocity = new Vector2(-_CurrentSpeed, myRigidbody.velocity.y);
                myRigidbody.transform.localScale = new Vector3(ScalePlayer, ScalePlayer, ScalePlayer);
                animation.SetBool(boolWalk, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody.velocity = new Vector2(_CurrentSpeed, myRigidbody.velocity.y); 
                myRigidbody.transform.localScale = new Vector3(-ScalePlayer, ScalePlayer, ScalePlayer);
                animation.SetBool(boolWalk, true);

            }
            else
            {
                animation.SetBool(boolWalk, false);
            }

            if ((myRigidbody.velocity.x < 0.2f) && (myRigidbody.velocity.x > -0.2f))
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
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

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (InPlataform == true)
            {
                myRigidbody.velocity = Vector2.up * forcejump;
            }
            /*
            boxCollider = GetComponent<BoxCollider2D>();

            ContactPoint2D[] contacts = new ContactPoint2D[10];
            int numContacts = boxCollider.GetContacts(contacts);

            for (int i = 0; i < numContacts; i++)
            {
                Collider2D otherCollider = contacts[i].collider;

                if (otherCollider.gameObject.CompareTag("Plataform"))
                {

                    myRigidbody.velocity = Vector2.up * forcejump;
                    
                }
            }
            */
        }


    }

      

    public void KillPlayer()
    {
        ConditionKill = true;
        animation.SetTrigger(TrigerKillPlayer);
    }

    public void RespawPlayerGame()
    {        
        ConditionKill = false;
    }


}
