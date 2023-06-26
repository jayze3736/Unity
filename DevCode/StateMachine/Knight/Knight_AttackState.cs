using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_AttackState : IKnightState
{

    public IKnightState ChangeState(KnightPlayer knight)
    {
        // ª�� �ð��� ���� �Ŀ� Idle State�� �����ϵ���
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
        jsh.SoundManager.instance.PlaySFX(knight.SFXCategory, "SwordSlash");

        if (enemies == null)
        {
            return;
        }

        foreach (var enemy in enemies)
        {
            knight.Attack(enemy, knight.Attack1Damage);
        }

        

    }
}
