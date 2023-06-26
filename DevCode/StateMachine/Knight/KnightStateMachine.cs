using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStateMachine
{
    //CharacterState state;
    IKnightState currentState;


    // Start is called before the first frame update
    public void Start()
    {
        //currentState = new IdleState();
        currentState = new Knight_IdleState();
    }

    // Update is called once per frame
    public void Update(KnightPlayer knight)
    {

        currentState.Update(knight);
        currentState = currentState.ChangeState(knight);

    }

    public void FixedUpdate(KnightPlayer knight)
    {
        currentState.FixedUpdate(knight);
        
    }

    /* �ܺο����� State�� �ֹ�(����)�Ͽ� ���¸� �ٲ� �� �ֵ��� �Ѵ�. - ���������δ� Input Ű�� �ԷµǸ� ChangeState(Attack) ���·� State�� ��ȯ�Ѵ�.
    * State���� State�� �̵��� �� �ִ� ��Ģ�� �� Ŭ�������� �����ؾ���
    * ���� Attack State ���� Move State�� �����ϴ� ���� �Ұ����ϴٸ� �ܺο��� Ư�� Input Ű�� �ԷµǾ� ChangeState(Move) ���·� State ��ȯ�� �ֹ��ص�
    * ���� State�� Attack State�̰� Attack State���� ���ǵ� ��Ģ������ Move ���·� ��ȯ�� �Ұ����ϹǷ� State ������ ������ �� �ִ�.
    * 
    */



}
