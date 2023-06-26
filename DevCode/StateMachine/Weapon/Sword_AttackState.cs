using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// Sword�� ������ ���� ����, �ð��� ȿ�� �� ���� ó���� �����Ѵ�.
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
        // ������ Timer �ð���ŭ ��ٸ��� ���ȿ��� ���� ������ �ð�ȭ��
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
