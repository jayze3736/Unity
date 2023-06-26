using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class MoveState : ICharacterState
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
        // MoveState -> AttackState
        if (InputManager.manager.isPressedDownAttack())
        {
            Debug.Log("EVENT: MOVE -> ATTACK");
            return new AttackState();
        }

        if(InputManager.manager.isPressedLMove() || InputManager.manager.isPressedRMove())
        {

            Debug.Log("EVENT: MOVE -> MOVE");
            return new MoveState();
        }

        // 키보드 입력이 없으면 IDLE 상태로 돌아감
        Debug.Log("EVENT: MOVE -> IDLE");
        return new IdleState();
        

        
    }


    

    public void Update(TestCharacter character)
    {
        if (InputManager.manager.isPressedLMove()) 
        {
            character.TurnLeft();
            character.LMove();
        }

        if (InputManager.manager.isPressedRMove())
        {
            character.TurnRight();
            character.RMove();
        }

        if (InputManager.manager.isPressedDownJump())
        {
            character.Jump();
        }


        
    }
}
