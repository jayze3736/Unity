using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    /// <summary>
    /// Update���� ó���� ����
    /// </summary>
    public void Update();

    /// <summary>
    /// FixedUpdate���� ó���� ����
    /// </summary>
    public void FixedUpdate();

    /// <summary>
    /// �ܺ� �Է��� �޾� State�� �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public State ChangeState();

    /// <summary>
    /// ���� State���� ���� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Enter();

    /// <summary>
    /// ���� State���� �ٸ� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Exit();


}
