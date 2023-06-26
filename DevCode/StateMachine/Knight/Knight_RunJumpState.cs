using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. Run -> Jump�� ��ȯ�Ҷ� ������ ���� x������ impulse�� �ش�.
//2. Jump ���¿��� x ������ ����Ű �Է½�
public class Knight_RunJumpState : IKnightState
{
    public IKnightState ChangeState(KnightPlayer knight)
    {
        // Run Jump -> Run
        if(knight.isJumping())
        {

        }
        // Run jump -> Jump

        // Run jump -> Idle

        return null;

    }

    public IKnightState Enter(KnightPlayer knight)
    {
        var jump = new Knight_JumpState();
        jump.Enter(knight);
        return this;
    }

    public void Exit(KnightPlayer knight)
    {
       
    }

    public void FixedUpdate(KnightPlayer knight)
    {
        var run = new Knight_RunState();
        var jump = new Knight_JumpState();
        run.FixedUpdate(knight);
        jump.FixedUpdate(knight);
    }

    public void Update(KnightPlayer knight)
    {
        var run = new Knight_RunState();
        var jump = new Knight_JumpState();
        run.Update(knight);
        jump.Update(knight);
        

    }
}
