using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public interface ICharacterState 
{
    /// <summary>
    /// Update���� ó���� ����
    /// </summary>
    public void Update(TestCharacter character);

    /// <summary>
    /// FixedUpdate���� ó���� ����
    /// </summary>
    public void FixedUpdate(TestCharacter character);

    /// <summary>
    /// �ܺ� �Է��� �޾� State�� �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public ICharacterState ChangeState();

    /// <summary>
    /// ���� State���� ���� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Enter(TestCharacter character);

    /// <summary>
    /// ���� State���� �ٸ� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Exit(TestCharacter character);



}
