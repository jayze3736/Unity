using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveSpeedManager
{
   

    public void ChangeAnimSpeed(Animator anim, float speed)
    {
        anim.speed = speed;
    }


    public void Stop(Animator anim)
    {
        anim.speed = 0;
    }

    public void Reset(Animator anim)
    {
        anim.speed = 1;
    }


}
