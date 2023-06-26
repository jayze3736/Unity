using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// AttackState�� ��� �ѹ� State �۾��� ������ �� IDLE ���·� ���ư�����
/// </summary>
/// 
/// �ᱹ�� Attack State�� ����ϰ� �����ߵǴ� �ǰ�
public class AttackState : ICharacterState
{
    

    public void Enter(TestCharacter character)
    {
       
    }

    public void Exit(TestCharacter character)
    {
       
    }

    public void FixedUpdate(TestCharacter character)
    {
       
    }

    public ICharacterState ChangeState()
    {

        //if (!done)
        //{
        //    Debug.Log("EVENT: ATTACK -> ATTACK");
        //    return this;
        //}


        if (InputManager.manager.isPressedDownLMove() || InputManager.manager.isPressedDownRMove())
        {
            Debug.Log("EVENT: ATTACK -> MOVE");
            return new MoveState();
        }

        Debug.Log("EVENT: ATTACK -> IDLE");
        return new IdleState();

  
    }

    public void Update(TestCharacter character)
    {

        // Weapon�� 1st state

        // Weapon�� 2nd state

        // Weapon�� 3rd state
    }
}
