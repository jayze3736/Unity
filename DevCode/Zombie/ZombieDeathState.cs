using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathState : IEnemyState
{
    


    public IEnemyState ChangeState(Enemy enemy)
    {
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
        // Do Nothing
    }
}
