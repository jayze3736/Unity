using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class IdleState : ICharacterState
{
  
    public void Enter(TestCharacter character)
    {
        throw new System.NotImplementedException();
    }

   
    public void Exit(TestCharacter character)
    {
       
    }


    public void FixedUpdate(TestCharacter character)
    {
        
    }

    public ICharacterState ChangeState()
    {

        if (InputManager.manager.isPressedDownAttack())
        {
            Debug.Log("EVENT: IDLE -> ATTACK");
            //return CharacterState.attack;
            return new AttackState();
        }

        if (InputManager.manager.isPressedLMove() || InputManager.manager.isPressedRMove())
        {
            Debug.Log("EVENT: IDLE -> MOVE");
            return new MoveState();
        }

        if (InputManager.manager.isPressedDownEquip())
        {
            Debug.Log("EVENT: IDLE -> EQUIP");
            return new EquipState();
        }

        return this; // ��� Input �Է��� ������ Idle ���¿��� ����

    }

    public void Update(TestCharacter character)
    {
        
        character.IDLE();


        
    }
}
