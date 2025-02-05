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

    public ParticleSystem particleDirtRun;
    public ParticleSystem particlesystemLand1;
    public ParticleSystem particlesystemLand2;


    [Header("Configurações camera")]
    public GameObject Cam;
    public float velCamMax = 12f;
    public float positioncam = 15f;
    public float directioncam = 1f;

    private float velCam = 0;
    private float velCammov = 10f;
    private float maxPosition = 20f;


    public List<Animator> animacao;

    //private ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emission; // Módulo de emissão
    private float currentSpeed;
    private bool down = false;


    void Awake()
    {

        emission = particleDirtRun.emission; // Acessa o módulo de emissão



        Vector2 vetorref = new Vector2(0, 0);
        Cam.transform.localPosition = new Vector3(positioncam, 0, 0);
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
                if ((particlesystemLand1 != null) && (particlesystemLand2 != null) && (down))
                {
                    particlesystemLand1.Play();
                    particlesystemLand2.Play();
                    down = false;
                }
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
            if (myRigidbody.velocity.y <= 0)
            {
                _curretPlayerLegs.SetBool(soPlayerSetup.boolJumpDown, true);
                _curretPlayerArms.SetBool(soPlayerSetup.boolJumpDown, true);
                if (_curretPlayerLegs.GetBool(soPlayerSetup.boolJump) == true) 
                { 
                    down = true; 
                }   
                    
                
            }
            else
            {
                _curretPlayerLegs.SetBool(soPlayerSetup.boolJumpDown, false);
                _curretPlayerArms.SetBool(soPlayerSetup.boolJumpDown, false);

            }
            HandleJump();
            HandleMoviment();
            Movimentcam();
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
            if (particleDirtRun != null)
            {
                //particleDirtRun.Play();
            }
            if (InPlataform == true)
            {
                emission.enabled = true;
            }
            else
            {
                emission.enabled = false;
            }
        }
        else
        {
            circleCollider.radius = soPlayerSetup.radiusWalkDetection;
            _CurrentSpeed = soPlayerSetup.speed;
            _curretPlayerLegs.SetBool(soPlayerSetup.boolRun, false);
            _curretPlayerArms.SetBool(soPlayerSetup.boolRun, false);
            //stopParticle();
            emission.enabled = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.velocity = new Vector2(-_CurrentSpeed, myRigidbody.velocity.y);
            _curretPlayerLegs.transform.localScale = new Vector3(1, 1, 1);
            _curretPlayerLegs.SetBool(soPlayerSetup.boolWalk, true);
            _curretPlayerArms.SetBool(soPlayerSetup.boolWalk, true);
            velCammov = velCam * 2;
            directioncam = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(_CurrentSpeed, myRigidbody.velocity.y);
            _curretPlayerLegs.transform.localScale = new Vector3(-1, 1, 1);
            _curretPlayerLegs.SetBool(soPlayerSetup.boolWalk, true);
            _curretPlayerArms.SetBool(soPlayerSetup.boolWalk, true);
            velCammov = velCam * 2;
            directioncam = 1;
        }
        else
        {
            velCammov = velCam;
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
            if (ReferenciaItem.ListSlotsGuns[0].codigo != 0)
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


    private void Movimentcam()
    {


        if ((directioncam == 1) && (positioncam < maxPosition))
        {
            positioncam += velCammov * directioncam * Time.deltaTime;
            Cam.transform.localPosition = new Vector3(positioncam, 0, 0);
            velCam = Mathf.Lerp(velCamMax, 1f, ((positioncam) + maxPosition) / (maxPosition * 2));
        }
        else if ((directioncam == -1) && (positioncam > -maxPosition))
        {
            positioncam += velCammov * directioncam * Time.deltaTime;
            Cam.transform.localPosition = new Vector3(positioncam, 0, 0);
            velCam = Mathf.Lerp(velCamMax, 1f, ((maxPosition * 2) - ((positioncam) + maxPosition)) / (maxPosition * 2));
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
    private void stopParticle()
    {
        if (particleDirtRun != null)
        {
            //particleDirtRun.emission.enabled = false;
        }
    }
    public void colletGun()
    {
        if (ReferenciaItem.ListSlotsGuns[ReferenciaItem.selectrender].codigo != 0)
        {
            activeGun();
        }
    }


}
