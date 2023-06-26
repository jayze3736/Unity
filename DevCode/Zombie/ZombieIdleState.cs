using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : IEnemyState
{
    public IEnemyState ChangeState(Enemy enemy)
    {
        if (enemy.IsStunning()) // ������ �޾Ƽ� ���ϻ����� ���
        {
            return new ZombieDamagedState();
        }

        if (enemy.IsPlayerNear())
        {
            return new ZombieAttackState();
        }


        return this;
    }

    public void Enter(Enemy enemy)
    {
        
    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        enemy.Idle();
    }
}
