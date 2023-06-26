using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_FallState : IKnightState
{


    public IKnightState Enter(KnightPlayer knight)
    {

         return this;
    }


    public IKnightState ChangeState(KnightPlayer knight)
    {

        if (!knight.isOnGround())
        {
            DebugStateUtils.DebugStateChange("FALL", "FALL");
            return this;
        }
        else
        {
            DebugStateUtils.DebugStateChange("FALL", "IDLE");
            return new Knight_IdleState();
        }

    }

   

    public void Exit(KnightPlayer knight)
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate(KnightPlayer knight)
    {
        knight.RunFixedUpdate();
    }

    public void Update(KnightPlayer knight)
    {
      
    }

   
}
