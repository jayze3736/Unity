using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class CharacterStateMachine
{
    //CharacterState state;
    ICharacterState currentState;


    // Start is called before the first frame update
    public void Start()
    {
        currentState = new IdleState();
    }

    // Update is called once per frame
    public void Update(TestCharacter character)
    {
 
        currentState.Update(character);
        currentState = currentState.ChangeState();

    }

    public void FixedUpdate(TestCharacter character)
    {
        currentState.FixedUpdate(character);
        currentState = currentState.ChangeState();
    }

    /* 외부에서는 State를 주문(오더)하여 상태를 바꿀 수 있도록 한다. - 실질적으로는 Input 키가 입력되면 ChangeState(Attack) 형태로 State를 변환한다.
    * State에서 State로 이동할 수 있는 규칙은 각 클래스에서 규정해야함
    * 만약 Attack State 에서 Move State로 변경하는 것이 불가능하다면 외부에서 특정 Input 키가 입력되어 ChangeState(Move) 형태로 State 변환을 주문해도
    * 현재 State가 Attack State이고 Attack State에서 정의된 규칙에서는 Move 상태로 변환이 불가능하므로 State 구조를 설계할 수 있다.
    * 
    */



   

 

}
