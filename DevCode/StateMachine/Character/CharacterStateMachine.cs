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

    /* �ܺο����� State�� �ֹ�(����)�Ͽ� ���¸� �ٲ� �� �ֵ��� �Ѵ�. - ���������δ� Input Ű�� �ԷµǸ� ChangeState(Attack) ���·� State�� ��ȯ�Ѵ�.
    * State���� State�� �̵��� �� �ִ� ��Ģ�� �� Ŭ�������� �����ؾ���
    * ���� Attack State ���� Move State�� �����ϴ� ���� �Ұ����ϴٸ� �ܺο��� Ư�� Input Ű�� �ԷµǾ� ChangeState(Move) ���·� State ��ȯ�� �ֹ��ص�
    * ���� State�� Attack State�̰� Attack State���� ���ǵ� ��Ģ������ Move ���·� ��ȯ�� �Ұ����ϹǷ� State ������ ������ �� �ִ�.
    * 
    */



   

 

}
