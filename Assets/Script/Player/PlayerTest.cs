using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public BoxCollider2D boxCollider; 
    public CircleCollider2D circleCollider;
    public Rigidbody2D myRigidbody;

    [Header("setup")]
    public SOPlayerSetup soPlayerSetup;
    public SOInfoUI ReferenciaItem;
  
    public bool InPlataform = false;
    public Animator _curretPlayerLegs;
    public Animator _curretPlayerArms;
    //public Animator animation;
    // Animator animation2;
    public Transform DetectPlataform;
    public LayerMask MaskPlataform;

    public bool ConditionKill;
    public bool ConditionVictory;

    private float _CurrentSpeed;


    public List<Animator> animacao;


    void Awake()
    {
      
        Vector2 vetorref = new Vector2(0,0);
        _curretPlayerLegs = Instantiate(soPlayerSetup.playerLegs, transform);
        //_curretPlayerLegs.transform.position = new Vector3(0, -1.51f, 0);
        //_curretPlayerArms = Instantiate(soPlayerSetup.referenceAnimator.referenceArm);
        int a = 0;
        animacao = new List<Animator>();
        foreach (var child in transform.GetComponentsInChildren<Animator>())
        {
            animacao.Add(child);
            Debug.Log(a);
        }
        _curretPlayerArms = animacao[animacao.Count - 1];

        soPlayerSetup.PositionInicial = transform.position;
        ConditionInitial();
    }

    private void Start()
    {

    }

    public void ConditionInitial()
    {
        circleCollider.radius = 20f;
        _CurrentSpeed = soPlayerSetup.speed;
        boxCollider.isTrigger = false;
        ConditionKill = (false);
        gameObject.SetActive(true); 
        myRigidbody.transform.localScale = new Vector2(soPlayerSetup.ScalePlayer, soPlayerSetup.ScalePlayer);
        RespawPlayerGame();
        gameObject.transform.localPosition = soPlayerSetup.PositionInicial;
        gameObject.transform.localRotation = Quaternion.Euler(Vector2.zero);
        _curretPlayerLegs.SetBool(soPlayerSetup.TrigerJump, true);
        _curretPlayerArms.SetBool(soPlayerSetup.TrigerJump, true);
        _curretPlayerArms.SetBool(soPlayerSetup.boolGun, false);
        _curretPlayerArms.SetBool(soPlayerSetup.boolNotGun, false);
        _curretPlayerLegs.SetBool(soPlayerSetup.boolRun, false);
        _curretPlayerArms.SetBool(soPlayerSetup.boolRun, false);
    }

    void Update()
    {

        if (!ConditionKill)
        {

            InPlataform = Physics2D.OverlapCircle(DetectPlataform.position, 0.5f, MaskPlataform);
            if (InPlataform)
            {
                _curretPlayerLegs.SetBool(soPlayerSetup.boolJump, false);
                _curretPlayerArms.SetBool(soPlayerSetup.boolJump, false);
            }
            else
            {
                if (_curretPlayerLegs.GetBool(soPlayerSetup.boolJump) == false)
                {
                    _curretPlayerLegs.SetBool(soPlayerSetup.TrigerJump, true);
                    _curretPlayerArms.SetBool(soPlayerSetup.TrigerJump, true);
                }
                _curretPlayerLegs.SetBool(soPlayerSetup.boolJump, true);
                _curretPlayerArms.SetBool(soPlayerSetup.boolJump, true);
            }
            if(myRigidbody.velocity.y <= 0)
            {
                _curretPlayerLegs.SetBool(soPlayerSetup.boolJumpDown, true);
                _curretPlayerArms.SetBool(soPlayerSetup.boolJumpDown, true);
            }
            else
            {
                _curretPlayerLegs.SetBool(soPlayerSetup.boolJumpDown, false);
                _curretPlayerArms.SetBool(soPlayerSetup.boolJumpDown, false);
            }
            HandleJump();
            HandleMoviment();
        }
    }

    private void HandleMoviment()
    {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                circleCollider.radius = soPlayerSetup.radiusRunDetection;
                _CurrentSpeed = soPlayerSetup.speedRun;
                _curretPlayerLegs.SetBool(soPlayerSetup.boolRun, true);
                _curretPlayerArms.SetBool(soPlayerSetup.boolRun, true);
            }
            else
            {
                circleCollider.radius = soPlayerSetup.radiusWalkDetection;
                _CurrentSpeed = soPlayerSetup.speed;
                _curretPlayerLegs.SetBool(soPlayerSetup.boolRun, false);
                _curretPlayerArms.SetBool(soPlayerSetup.boolRun, false);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody.velocity = new Vector2(-_CurrentSpeed, myRigidbody.velocity.y);
                myRigidbody.transform.localScale = new Vector3(soPlayerSetup.ScalePlayer, soPlayerSetup.ScalePlayer, soPlayerSetup.ScalePlayer);
                _curretPlayerLegs.SetBool(soPlayerSetup.boolWalk, true);
                _curretPlayerArms.SetBool(soPlayerSetup.boolWalk, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody.velocity = new Vector2(_CurrentSpeed, myRigidbody.velocity.y); 
                myRigidbody.transform.localScale = new Vector3(-soPlayerSetup.ScalePlayer, soPlayerSetup.ScalePlayer, soPlayerSetup.ScalePlayer);
                _curretPlayerLegs.SetBool(soPlayerSetup.boolWalk, true);
                _curretPlayerArms.SetBool(soPlayerSetup.boolWalk, true);

            }
            else
            {
                _curretPlayerLegs.SetBool(soPlayerSetup.boolWalk, false);
                _curretPlayerArms.SetBool(soPlayerSetup.boolWalk, false);
            }

                

        if ((myRigidbody.velocity.x < 0.2f) && (myRigidbody.velocity.x > -0.2f))
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }
            else if (myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity -= soPlayerSetup.friction;
            }
            else if (myRigidbody.velocity.x < 0)
            {
                myRigidbody.velocity += soPlayerSetup.friction;
            }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(ReferenciaItem.ListSlotsGuns[0].codigo != 0)
            {
                activeGun();
            }
            else
            {
                desactiveGun();
            } 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (ReferenciaItem.ListSlotsGuns[1].codigo != 0)
            {
                activeGun();
            }
            else
            {
                desactiveGun();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (ReferenciaItem.ListSlotsGuns[2].codigo != 0)
            {
                activeGun();
            }
            else
            {
                desactiveGun();
            }
        }


    }

    private void activeGun()
    {
          _curretPlayerArms.SetBool(soPlayerSetup.TrigerGun1, true);
          _curretPlayerArms.SetBool(soPlayerSetup.boolGun, true);
          _curretPlayerArms.SetBool(soPlayerSetup.boolNotGun, true);
        //Invoke(nameof(OperacaodeTempo), 1.5f);
        
    }
    private void desactiveGun()
    {
         _curretPlayerArms.SetBool(soPlayerSetup.boolGun, false);
         Invoke(nameof(OperacaodeTempo), 1.5f);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (InPlataform == true)
            {
                myRigidbody.velocity = Vector2.up * soPlayerSetup.forcejump;
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
        _curretPlayerArms.SetBool(soPlayerSetup.boolNotGun, false);
    }


        public void KillPlayer()
    {
        ConditionKill = true;
        _curretPlayerLegs.SetTrigger(soPlayerSetup.TrigerKillPlayer);
        _curretPlayerArms.SetTrigger(soPlayerSetup.TrigerKillPlayer);
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
        //Debug.Log("jsaghbfjiha " + ReferenciaItem.ListSlotsGuns[ReferenciaItem.selectrender].codigo);

        if (ReferenciaItem.ListSlotsGuns[ReferenciaItem.selectrender].codigo != 0)
        {
            activeGun();
        }
    }

}
