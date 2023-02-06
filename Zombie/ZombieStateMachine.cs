using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine
{

    IEnemyState curState;


    public void Start()
    {
        curState = new ZombieIdleState();

    }


    public void Update(Enemy zombie)
    {
        
        if (zombie.isDead())
        {
            curState = new ZombieDeathState();
            curState.Update(zombie);
        }
        else
        {
            curState.Update(zombie);
            curState = curState.ChangeState(zombie);
        }

        

    }





}
