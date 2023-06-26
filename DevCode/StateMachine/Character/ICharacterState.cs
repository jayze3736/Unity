using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public interface ICharacterState 
{
    /// <summary>
    /// Update에서 처리할 동작
    /// </summary>
    public void Update(TestCharacter character);

    /// <summary>
    /// FixedUpdate에서 처리할 동작
    /// </summary>
    public void FixedUpdate(TestCharacter character);

    /// <summary>
    /// 외부 입력을 받아 State를 변경하는 함수
    /// </summary>
    /// <returns></returns>
    public ICharacterState ChangeState();

    /// <summary>
    /// 이전 State에서 현재 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Enter(TestCharacter character);

    /// <summary>
    /// 현재 State에서 다른 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Exit(TestCharacter character);



}
