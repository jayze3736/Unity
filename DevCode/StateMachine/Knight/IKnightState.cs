using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public interface IKnightState
{

    /// <summary>
    /// 이전 State에서 현재 State로 넘어갈때 실행되는 함수
    /// </summary>
    public IKnightState Enter(KnightPlayer knight);

    /// <summary>
    /// Update에서 처리할 동작
    /// </summary>
    public void Update(KnightPlayer knight);

    /// <summary>
    /// FixedUpdate에서 처리할 동작
    /// </summary>
    public void FixedUpdate(KnightPlayer knight);

    /// <summary>
    /// 외부 입력을 받아 State를 변경하는 함수
    /// </summary>
    /// <returns></returns>
    public IKnightState ChangeState(KnightPlayer knight);

    /// <summary>
    /// 현재 State에서 다른 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Exit(KnightPlayer knight);

   

}
