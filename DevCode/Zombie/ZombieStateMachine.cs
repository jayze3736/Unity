using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine
{
    IEnemyState curState;
    


    public void Start(Enemy zombie)
    {
        curState = new ZombieIdleState();
        curState.Enter(zombie);

    }


    public void Update(Enemy zombie)
    {
        
        if (zombie.isDead()) // global state(any state)
        {
            curState = new ZombieDeathState();
            curState.Update(zombie);
        }
        else
        {
            curState.Update(zombie);
            var nextState = curState.ChangeState(zombie);

            if(nextState != curState)
            {
                curState.Exit(zombie);
                nextState.Enter(zombie);
            }
            curState = nextState;
        }

        

    }





}
