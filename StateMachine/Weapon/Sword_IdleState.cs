using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// 기본적으로 검을 들고 있을때 상태
/// </summary>
public class Sword_IdleState : ISwordState
{

    public ISwordState ChangeState(Sword sword)
    {
        if (InputManager.manager.isPressedDownAttack())
        {
            Debug.Log("SWORD_IDLE -> SWORD_READY");
            return new Sword_ReadyState();
        }

        return new Sword_IdleState();
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
        
        sword.IDLE();
    }
}
