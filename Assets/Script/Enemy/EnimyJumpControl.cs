using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyJumpControl : EnimyControl
{

    [Header("Enimy jump")]
    public Transform DetectPlataform;
    public LayerMask MaskPlataform; 
    public float forcejump = 10;
    public float Veljump = 10;

    public BoxCollider2D myBoxColliderPhisic;

    private float TimeWaitJump = 0;
    private bool timeContJump;

    void Awake()
    {

        base.ConditionInitial();
        base.state = STATES.WAITING;
        timeContJump = true;
    }

    void Update()
    {
        if (!base._isDead)
        {
            if (base.state == STATES.JUMP || base.state == STATES.ATTACK)
            {
                switch (state)
                {
                    case STATES.ATTACK:
                        base.StateEnimy();
                        if (timeContJump)
                        {
                            timeContJump = false;
                            RandowTimeJumpAttack();
                            Invoke(nameof(VerificationinitialJump), TimeWaitJump);
                        }
                        break;

                    case STATES.JUMP:
                        TipeJump();
                        break;
                }
            }
            else
            {
                base.StateEnimy();
            }
        }
        else
        {
            StateEnimyKill();
        }
    }

    private void TipeJump() {  
        InPlataform = Physics2D.OverlapCircle(DetectPlataform.position, 0.5f, MaskPlataform);
            if (InPlataform)
            {
               animation.SetBool(boolJump, false);
               animation2.SetBool(boolJump, false);
            }
            else
            {
            animation.SetBool(boolJump, true);
            animation2.SetBool(boolJump, true);
        }
        if (myRigidbody.velocity.y <= 0)
        {
            animation.SetBool(boolJumpDown, true);
            animation2.SetBool(boolJumpDown, true);
        }
        else
        {
            animation.SetBool(boolJumpDown, false);
            animation2.SetBool(boolJumpDown, false);
        }
    }


    private void RandowTimeJumpAttack()
    {
         TimeWaitJump = Random.Range(2f, 10f);
    }

    private void VerificationinitialJump()
    {
        if (base.state == STATES.ATTACK)
        {
            animation.SetBool(TrigerJump, true);
            animation2.SetBool(TrigerJump, true);
            animation.SetBool(boolJump, true);
            animation2.SetBool(boolJump, true);
            base.state = STATES.JUMP;
        }
        else
        {
            timeContJump = true;
        }
    }

    public void VerificationJump()
    {
        myRigidbody.velocity = Vector2.up * forcejump;
        int directionA = (target.transform.position.x - transform.position.x) < 0 ? 1 : -1;
        if (directionA != base.direction)
        {
            direction = directionA;
            MovimenAngle = new Vector3(direction * ScaleEnimy, ScaleEnimy, ScaleEnimy);
        }
        myRigidbody.velocity = new Vector2(-((direction) * Veljump), myRigidbody.velocity.y);
        myRigidbody.transform.localScale = base.MovimenAngle;
        base.state = STATES.JUMP;        
    }

    public void FinishJumpEnimy()
    {
        timeContJump = true;
        base.state = STATES.ATTACK;
        base.InitialAttack();
    }

    public override void ActiveKill()
    {

        myBoxColliderPhisic.enabled = false;
        base.ActiveKill();
    }
}
