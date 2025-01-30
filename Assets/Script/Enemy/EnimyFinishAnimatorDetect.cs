using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyFinishAnimatorDetect : MonoBehaviour
{
    public EnimyJumpControl enimyJump;

    public void detectFimJump()
    {
        Debug.Log("disparo");
        enimyJump.FinishJumpEnimy();
    }

    public void detectJump()
    {
        enimyJump.VerificationJump();
    }
    


}
