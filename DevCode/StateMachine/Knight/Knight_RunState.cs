using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// Run�� Jump ���°� ���ÿ��� �ƴϴ��� ������ ������ �ۿ��� �ؾ��ϰ� �ִϸ��̼ǰ��� ���� �켱���� ������ ����������
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
