//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyControl : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public BoxCollider2D myBoxCollider;
    public float ScaleEnimy;
    public float _CurrentSpeed;
    public float _CurrentSpeedRun;
    public GameObject target;


    [Header("Animation setup")]
    public string boolRun = "Is_Run";
    public string boolWalk = "Is_Walk";
    public string boolJump = "Is_Jump";
    public string boolJumpDown = "Is_JumpDown";
    public string TrigerJump = "IsJumpTrig";
    public string TrigerKill = "Is_Kill";
    public string boolGun = "IsGun";
    public string boolNotGun = "IsNotGun";
    public string TrigerGun1 = "IsTrigGun";
    public bool InPlataform = false;
    public Animator animation;
    public Animator animation2;

    private float TimeWait = 0;
    protected int direction;
    protected enum STATES { WAITING, WALK, ATTACK, JUMP };
    protected STATES state;

    //private int timeMaxActiveOff = 5;
    //private int timeActiveOff = 0;
    private bool timeCont = true;

    protected float Moviment = 0;
    protected Vector3 MovimenAngle = new Vector3(0, 0, 0);

    public int timeMaxActiveOff = 20;
    private bool timeContAttac = true;
    protected bool timeContAttacactive = false;


    public bool DestroyOnKill = false;
    public float dalayToKill = 0f;
    private int _currentLife;
    protected bool _isDead = false;
    public HealthBase healtBase;

    public BoxCollider2D BoxColliderhand1;
    public BoxCollider2D BoxColliderhand2;

    // Start is called before the first frame update
    void Awake()
    {
        //PositionInicial = transform.position;
        ConditionInitial();

    }


    private void OnEnemyKill()
    {
        healtBase.OnKill -= OnEnemyKill;
        ActiveKill();
    }

    public virtual void ConditionInitial()
    {
        timeCont = true;
        RandowTime();
        state = STATES.WAITING;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        //animation TrigerReset = false;
        //animation.SetBool(TrigerReset, false);
        //boxCollider.isTrigger = false;
        //ConditionKill = (false);
        //gameObject.SetActive(true);
        //myRigidbody.transform.localScale = new Vector2(ScalePlayer, ScalePlayer);
        //RespawPlayerGame();
        //gameObject.transform.localRotation = Quaternion.Euler(Vector2.zero);
        //animation.SetBool(TrigerJump, true);
        //animation2.SetBool(TrigerJump, true);
        //animation2.SetBool(boolGun, false);
        //animation2.SetBool(boolNotGun, false);
        if (healtBase != null)
        {
            healtBase.OnKill += OnEnemyKill;
        }
    }

    void Update()
    {
        if (!_isDead)
        {
            StateEnimy();
        }
        else
        {
            StateEnimyKill();
        }
    }

    public virtual void StateEnimy() {
        if (state != STATES.ATTACK)
        {
            if (timeCont) {
                timeCont = false;
                RandowTime();
                Invoke(nameof(VerificationFinishUsage), TimeWait);
            }
        }
        else
        {
            if (timeContAttac && timeContAttacactive)
            {
                timeContAttac = false;
                Invoke(nameof(VerificationFinishUsageAttac), 1);
            }
        }

        switch (state)
        {
            case STATES.WAITING:

                break;
            case STATES.WALK:

                myRigidbody.velocity = new Vector2(Moviment, myRigidbody.velocity.y);
                myRigidbody.transform.localScale = MovimenAngle;
                break;
            case STATES.ATTACK:
                /*if (target != null)
                {
                    Vector2 direction = target.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                }*/
                int directionA = (target.transform.position.x - transform.position.x) < 0 ? 1 : -1;
                if (directionA != direction)
                {
                    direction = directionA;
                    DirectionAttack();
                }
                myRigidbody.velocity = new Vector2(Moviment, myRigidbody.velocity.y);
                myRigidbody.transform.localScale = MovimenAngle;
                break;
        }

    }

    public virtual void StateEnimyKill()
    {
        state = STATES.WAITING;
        animation.SetBool(boolRun, false);
        animation2.SetBool(boolRun, false);
        animation.SetBool(boolWalk, false);
        animation2.SetBool(boolWalk, false);
        timeCont = false;
    } 

    private void InitiWalk()
    {
        int RandomStart = ((int)Random.Range(0, 2) * 2 - 1);
        Moviment = (-(RandomStart) * _CurrentSpeed);
        MovimenAngle = new Vector3(RandomStart * ScaleEnimy, ScaleEnimy, ScaleEnimy);
    }

    private void RandowTime()
    {
        InitiWalk();
        if (state == STATES.WAITING)
        {
            TimeWait = Random.Range(5f, 50f);
        }
        else
        {
            TimeWait = Random.Range(0.2f, 10f);
        }
    }

    private void VerificationFinishUsageAttac()
    {
        if (timeMaxActiveOff >= 5)
        {
            state = STATES.WAITING;
            animation.SetBool(boolRun, false);
            animation2.SetBool(boolRun, false);
            animation.SetBool(boolWalk, false);
            animation2.SetBool(boolWalk, false);
            if (timeCont == false)
            {
                timeCont = true;
            }
        }
        if (state != STATES.JUMP) {            
            timeMaxActiveOff++; 
        }
        timeContAttac = true;
    }


    public void ActiveAttack(GameObject transformObj)
    {
        target = transformObj;
        InitialAttack();
        timeContAttacactive = false;
    }

    public void InitialAttack()
    {
        if (state != STATES.JUMP) 
        { 
            state = STATES.ATTACK;
            direction = (target.transform.position.x - transform.position.x) < 0 ? 1 : -1;
            DirectionAttack();
        }
    }

    public virtual void DirectionAttack()
    {
        Moviment = (-((direction) * _CurrentSpeedRun));
        MovimenAngle = new Vector3(direction * ScaleEnimy, ScaleEnimy, ScaleEnimy);
        animation.SetBool(boolRun, true);
        animation2.SetBool(boolRun, true);
    }

    public void DesactiveAttack()
    {

        timeContAttacactive = true;
        timeMaxActiveOff = 0;
    }

    private void VerificationFinishUsage()
    {
        if (state != STATES.ATTACK)
        {
            if (state == STATES.WAITING)
            {
                state = STATES.WALK;
                animation.SetBool(boolWalk, true);
                animation2.SetBool(boolWalk, true);
            }
            else
            {
                state = STATES.WAITING;
                animation.SetBool(boolWalk, false);
                animation2.SetBool(boolWalk, false);
            }
            timeCont = true;
        }
    }

    public virtual void ActiveKill()
    {
        animation.SetBool(TrigerKill, true);
        _isDead = true;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        myBoxCollider.size = new Vector2 (0,0);
        BoxColliderhand1.enabled = false;  
        BoxColliderhand2.enabled = false;
    }

}
