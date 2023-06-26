using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// 검의 첫번째 공격 단계, 현재 움직임을 고정하고 공격 키를 재입력 받아 공격한다.
/// </summary>
public class Sword_ReadyState : ISwordState
{
    public ISwordState ChangeState(Sword sword)
    {
        if (InputManager.manager.isPressedDownAttack())
        {
            Debug.Log("SWORD_READY -> SWORD_ATTACK");
            var state = new Sword_AttackState();
            state.Enter(sword);
            return state;
        }

        return new Sword_ReadyState();
    }

    public void Enter(Sword sword)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(Sword sword)
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate(Sword sword)
    {
       
    }

    public void Update(Sword sword)
    {

        sword.Ready();
    }
}
