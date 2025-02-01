using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteReferenceAnimator : MonoBehaviour
{
    public Animator referenceArm;

    public void Animar(Animator entrada)
    {
        referenceArm = entrada;
    }
}
