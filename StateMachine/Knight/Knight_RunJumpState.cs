using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. Run -> Jump로 변환할때 나가기 전에 x방향의 impulse를 준다.
//2. Jump 상태에서 x 방향의 방향키 입력시
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
