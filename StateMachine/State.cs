using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    /// <summary>
    /// Update에서 처리할 동작
    /// </summary>
    public void Update();

    /// <summary>
    /// FixedUpdate에서 처리할 동작
    /// </summary>
    public void FixedUpdate();

    /// <summary>
    /// 외부 입력을 받아 State를 변경하는 함수
    /// </summary>
    /// <returns></returns>
    public State ChangeState();

    /// <summary>
    /// 이전 State에서 현재 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Enter();

    /// <summary>
    /// 현재 State에서 다른 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Exit();


}
