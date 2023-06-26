using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class Knight_JumpState : IKnightState
{
    public IKnightState Enter(KnightPlayer knight)
    {
        //Jump�� ��� �ѹ��� ȣ��Ǵ� ���� �����ϱ� ���ؼ� Enter ��ƾ�� �Լ��� ����
        if (knight.CanJump())
        {
            knight.Jump();
            knight.SetTriggerJumpAnim();
            knight.OnEnterJump();
        }

        return this;
    }

    public void Exit(KnightPlayer knight)
    {
        knight.OnExitJump();
    }

    public  void FixedUpdate(KnightPlayer knight)
    {
        knight.RunFixedUpdate();
    }

    public  IKnightState ChangeState(KnightPlayer knight)
    {
        if (knight.isJumping())
        {
            DebugStateUtils.DebugStateChange("JUMP", "JUMP");
            return this;
        }
        else if (knight.isFalling())
        {
            DebugStateUtils.DebugStateChange("JUMP", "FALL");
            Exit(knight);
            return new Knight_FallState().Enter(knight);
        }
        else
        {

            DebugStateUtils.DebugStateChange("JUMP", "IDLE");
            Exit(knight);
            return new Knight_IdleState().Enter(knight);
        }

    }

    public void Update(KnightPlayer knight)
    {

        if (knight.isPressedUpJumpKey())
        {
            knight.OnJumpUp();
        }

        // Jump ���߿��� Run�� �����ϵ��� ����




    }
}
