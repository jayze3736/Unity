using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// Run과 Jump 상태가 동시에는 아니더라도 물리적 연산이 작용은 해야하고 애니메이션같은 것은 우선순위 점프가 먼저여야함
/// </summary>
public class Knight_RunState : IKnightState
{
    public IKnightState Enter(KnightPlayer knight)
    {
        
        return this;
    }

    public void Exit(KnightPlayer knight)
    {
        Debug.Log("EXIT Run State");
        //knight.Run(KnightPlayer.RunDirection.STOP);
    }

    public void FixedUpdate(KnightPlayer knight)
    {
        knight.RunFixedUpdate();

    }

    public IKnightState ChangeState(KnightPlayer knight)
    {
        if (knight.isPressedAttackDownKey())
        {
            return new Knight_AttackState().Enter(knight);
        }

        if (knight.isPressedDownJumpKey())
        {
            DebugStateUtils.DebugStateChange("RUN", "JUMP");
            Exit(knight);
            return new Knight_JumpState().Enter(knight);
        }

        if (knight.isPressedLeftRunKey() || knight.isPressedRightRunKey())
        {
            DebugStateUtils.DebugStateChange("RUN", "RUN");
            return this;
        }

        Exit(knight);
        DebugStateUtils.DebugStateChange("RUN", "IDLE");
        return new Knight_IdleState();

    }

    public void Update(KnightPlayer knight)
    {
        //if (InputManager.manager.isPressedDownJump())
        //{
        //    Debug.Log("Run And Jump");
        //    knight.Jump();

        //}


    }
}
