using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public interface IKnightState
{

    /// <summary>
    /// ���� State���� ���� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public IKnightState Enter(KnightPlayer knight);

    /// <summary>
    /// Update���� ó���� ����
    /// </summary>
    public void Update(KnightPlayer knight);

    /// <summary>
    /// FixedUpdate���� ó���� ����
    /// </summary>
    public void FixedUpdate(KnightPlayer knight);

    /// <summary>
    /// �ܺ� �Է��� �޾� State�� �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public IKnightState ChangeState(KnightPlayer knight);

    /// <summary>
    /// ���� State���� �ٸ� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Exit(KnightPlayer knight);

   

}
