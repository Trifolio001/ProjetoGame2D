using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public Animator playerLegs;

    //public prefa refAnimator;

    [Header("referencias iniciais")]
    public int Startlife;
    public Vector2 PositionInicial;
    public float ScalePlayer;

    [Header("referencias fisicas")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forcejump;

    [Header("zona de perseguição")]
    public int radiusWalkDetection = 25;
    public int radiusRunDetection = 50;

    [Header("referencias animação")]
    public string boolRun = "IsRun";
    public string boolWalk = "IsWalk";
    public string boolJump = "IsJump";
    public string boolJumpDown = "IsJumpDown";
    public string TrigerJump = "IsJumpTrig";
    public string TrigerKillPlayer = "IsKillPlayer";
    public string boolGun = "IsGun";
    public string boolNotGun = "IsNotGun";
    public string TrigerGun1 = "IsTrigGun";

}
