using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_AttackState : IKnightState
{

    public IKnightState ChangeState(KnightPlayer knight)
    {
        // 짧은 시간이 지난 후에 Idle State로 복귀하도록
        IKnightState nextState;

        if (knight.isAttacking())
        {
            DebugStateUtils.DebugStateChange("ATTACK", "ATTACK");
            nextState = this;
            
        }
        else
        {
            DebugStateUtils.DebugStateChange("ATTACK", "IDLE");
            nextState = new Knight_IdleState();
            
        }

        if(nextState != this)
        {
            Exit(knight);
        }
        
        return nextState;

    }

    public IKnightState Enter(KnightPlayer knight)
    {
        knight.SetTriggerAttackAnim();
        return this;
    }

    public void Exit(KnightPlayer knight)
    {
        DamageEnemy(knight);
        knight.ResetAttackCoolTime();
    }

    public void FixedUpdate(KnightPlayer knight)
    {
       
    }

    public void Update(KnightPlayer knight)
    {
       

    }

    void DamageEnemy(KnightPlayer knight)
    {
        var enemies = knight.DetectEnemyInAttackRange();

        if (enemies == null) return;

        foreach (var enemy in enemies)
        {
            knight.DamageEnemy(enemy, knight.Attack1Damage);
        }

    }
}
