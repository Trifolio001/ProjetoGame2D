using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{


    public Vector2 PositionInicial;
    public BoxCollider2D boxCollider; 
    public CircleCollider2D circleCollider;
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
    public string boolGun = "IsGun";
    public string boolNotGun = "IsNotGun";
    public string TrigerGun1 = "IsTrigGun"; 
    public bool InPlataform = false;
    public Animator animation;
    public Animator animation2;
    public Transform DetectPlataform;
    public LayerMask MaskPlataform;

    [Header("Detect Zombys")]
    public int radiusWalkDetection = 25;
    public int radiusRunDetection = 50;

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
        circleCollider.radius = 20f;
        _CurrentSpeed = speed;
        boxCollider.isTrigger = false;
        ConditionKill = (false);
        gameObject.SetActive(true); 
        myRigidbody.transform.localScale = new Vector2(ScalePlayer, ScalePlayer);
        RespawPlayerGame();
        gameObject.transform.localPosition = PositionInicial;
        gameObject.transform.localRotation = Quaternion.Euler(Vector2.zero);
        animation.SetBool(TrigerJump, true);
        animation2.SetBool(TrigerJump, true);
        animation2.SetBool(boolGun, false);
        animation2.SetBool(boolNotGun, false);
        animation.SetBool(boolRun, false);
        animation2.SetBool(boolRun, false);
    }

    void Update()
    {

        if (!ConditionKill)
        {

            InPlataform = Physics2D.OverlapCircle(DetectPlataform.position, 0.5f, MaskPlataform);
            if (InPlataform)
            {
               animation.SetBool(boolJump, false);
               animation2.SetBool(boolJump, false);
            }
            else
            {
                if (animation.GetBool(boolJump) == false)
                {
                    animation.SetBool(TrigerJump, true);
                    animation2.SetBool(TrigerJump, true);
                }
                animation.SetBool(boolJump, true);
                animation2.SetBool(boolJump, true);
            }
            if(myRigidbody.velocity.y <= 0)
            {
                animation.SetBool(boolJumpDown, true);
                animation2.SetBool(boolJumpDown, true);
            }
            else
            {
                animation.SetBool(boolJumpDown, false);
                animation2.SetBool(boolJumpDown, false);
            }
            HandleJump();
            HandleMoviment();
        }
    }

    private void HandleMoviment()
    {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                circleCollider.radius = radiusRunDetection;
                _CurrentSpeed = speedRun;
                animation.SetBool(boolRun, true);
                animation2.SetBool(boolRun, true);
            }
            else
            {
                circleCollider.radius = radiusWalkDetection;
                _CurrentSpeed = speed;
                animation.SetBool(boolRun, false);
                animation2.SetBool(boolRun, false);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody.velocity = new Vector2(-_CurrentSpeed, myRigidbody.velocity.y);
                myRigidbody.transform.localScale = new Vector3(ScalePlayer, ScalePlayer, ScalePlayer);
                animation.SetBool(boolWalk, true);
                animation2.SetBool(boolWalk, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody.velocity = new Vector2(_CurrentSpeed, myRigidbody.velocity.y); 
                myRigidbody.transform.localScale = new Vector3(-ScalePlayer, ScalePlayer, ScalePlayer);
                animation.SetBool(boolWalk, true);
                animation2.SetBool(boolWalk, true);

            }
            else
            {
                animation.SetBool(boolWalk, false);
                animation2.SetBool(boolWalk, false);
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


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            {
                animation2.SetBool(boolGun, false);
                Invoke(nameof(OperacaodeTempo), 1.5f);
            }
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

    public void OperacaodeTempo()
    {
        animation2.SetBool(boolNotGun, false);
    }


        public void KillPlayer()
    {
        ConditionKill = true;
        animation.SetTrigger(TrigerKillPlayer);
        animation2.SetTrigger(TrigerKillPlayer);
    }

    public void RespawPlayerGame()
    {        
        ConditionKill = false;
    }

    /*public void test(float alpha)
    {
        // Garante que o valor de alpha esteja entre 0 e 1
        alpha = Mathf.Clamp01(alpha);

        // Obtém a cor atual do sprite
        Color color = spriteRenderer.color;

        // Altera o valor do canal alfa
        color.a = alpha;

        // Aplica a nova cor ao sprite
        spriteRenderer.color = color;
    }*/

    public void colletGun()
    {

            if (animation2.GetBool(boolGun) == false)
            {
                animation2.SetBool(TrigerGun1, true);
                animation2.SetBool(boolGun, true);
                animation2.SetBool(boolNotGun, true);
            }
    }

}
