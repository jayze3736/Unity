using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_DamagedState : IKnightState
{
    public IKnightState ChangeState(KnightPlayer knight)
    {

        // ���� �ð��� ���� �ڿ� Idle ���·� ����
        return new Knight_IdleState();
        
    }

    public IKnightState Enter(KnightPlayer knight)
    {
        // Logic ó��
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
