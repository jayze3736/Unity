using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// ���� ù��° ���� �ܰ�, ���� �������� �����ϰ� ���� Ű�� ���Է� �޾� �����Ѵ�.
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
