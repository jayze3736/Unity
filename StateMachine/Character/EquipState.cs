using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class EquipState : ICharacterState
{
    public ICharacterState ChangeState()
    {
        return new IdleState();
    }

    public void Enter(TestCharacter character)
    {
       
    }

    public void Exit(TestCharacter character)
    {
        
    }

    public void FixedUpdate(TestCharacter character)
    {
        
    }

    public void Update(TestCharacter character)
    {
        character.Equip();
    }
}
