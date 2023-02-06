using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : IEnemyState
{
    public IEnemyState ChangeState(Enemy enemy)
    {
        

        if (enemy.IsPlayerNear())
        {
            return this;
        }
        else
        {
            Exit(enemy);
            return new ZombieIdleState();
        }

        

    }

    public void Enter(Enemy enemy)
    {
       

    }

    public void Exit(Enemy enemy)
    {
        enemy.ResetAttackCoolTimer();
    }

    public void Update(Enemy enemy)
    {
        enemy.Attack();
    }
}
