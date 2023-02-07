using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamagedState : IEnemyState
{
    public IEnemyState ChangeState(Enemy enemy)
    {
        if (enemy.IsStunning())
        {
            return this;
        }
        else
        {
            return new ZombieIdleState();
        }
    }

    public void Enter(Enemy enemy)
    {
        
    }

    public void Exit(Enemy enemy)
    {
       
    }

    public void Update(Enemy enemy)
    {
        // Do Nothing
    }
}
