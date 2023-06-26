using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class Knight_IdleState : IKnightState
{
    public IKnightState Enter(KnightPlayer knight)
    {
        //knight.SetKnightAnimIDLEState();
        return this;
    }

    public void Exit(KnightPlayer knight)
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate(KnightPlayer knight)
    {
        
    }

    public IKnightState ChangeState(KnightPlayer knight)
    {
        

        if (knight.isPressedDownJumpKey())
        {
            DebugStateUtils.DebugStateChange("IDLE", "JUMP");
            return new Knight_JumpState().Enter(knight);
        }

        if (knight.isPressedLeftRunKey() || knight.isPressedRightRunKey())
        {
            DebugStateUtils.DebugStateChange("IDLE", "RUN");
            return new Knight_RunState().Enter(knight);

        }

        if (knight.isPressedAttackDownKey())
        {
            DebugStateUtils.DebugStateChange("IDLE", "Attack");
            return new Knight_AttackState().Enter(knight);
        }

        return new Knight_IdleState();

    }

    public void Update(KnightPlayer knight)
    {
        
        knight.IDLE();
        
    }
}
