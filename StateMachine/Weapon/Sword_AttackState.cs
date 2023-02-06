using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// Sword의 마지막 공격 상태, 시각적 효과 및 로직 처리를 수행한다.
/// </summary>
public class Sword_AttackState : ISwordState
{
    bool changeState;

    public ISwordState ChangeState(Sword sword)
    {
        if (changeState)
        {
            Exit(sword);
            Debug.Log("SWORD_ATTACK -> SWORD_IDLE");
            return new Sword_IdleState();
        }

        return this;
       
        

    }

    public void Enter(Sword sword)
    {
        sword.ShowVisualEffect();
    }

    public void Exit(Sword sword)
    {
        sword.GiveDamage();
    }

    public void FixedUpdate(Sword sword)
    {
       
    }

    public void Update(Sword sword)
    {
        // 설정한 Timer 시간만큼 기다리는 동안에는 공격 범위를 시각화함
        if (!sword.Timer.Awake())
        {
            sword.VisualizeAttackRange();
            changeState = false;
        }
        else
        {
            changeState = true;
        }
       
    }
}
