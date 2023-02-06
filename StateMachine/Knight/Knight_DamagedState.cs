using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_DamagedState : IKnightState
{
    public IKnightState ChangeState(KnightPlayer knight)
    {

        // 스턴 시간이 지난 뒤에 Idle 상태로 복귀
        return new Knight_IdleState();
        
    }

    public IKnightState Enter(KnightPlayer knight)
    {
        // Logic 처리
        return this;
    }

    public void Exit(KnightPlayer knight)
    {
        
    }

    public void FixedUpdate(KnightPlayer knight)
    {
        
    }

    public void Update(KnightPlayer knight)
    {


    }
}
