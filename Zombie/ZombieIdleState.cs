using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : IEnemyState
{
    public IEnemyState ChangeState(Enemy enemy)
    {
        if (enemy.IsStunning()) // 공격을 받아서 스턴상태인 경우
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
        throw new System.NotImplementedException();
    }

    public void Exit(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Enemy enemy)
    {
        enemy.Idle();
    }
}
