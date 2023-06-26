using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// AttackState의 경우 한번 State 작업을 수행한 뒤 IDLE 상태로 돌아가야함
/// </summary>
/// 
/// 결국에 Attack State를 기록하고 나가야되는 건가
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

        // Weapon의 1st state

        // Weapon의 2nd state

        // Weapon의 3rd state
    }
}
