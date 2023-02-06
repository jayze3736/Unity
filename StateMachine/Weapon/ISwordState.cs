using jsh;

public interface ISwordState
{

    /// <summary>
    /// Update에서 처리할 동작
    /// </summary>
    public void Update(Sword sword);

    /// <summary>
    /// FixedUpdate에서 처리할 동작
    /// </summary>
    public void FixedUpdate(Sword sword);

    /// <summary>
    /// 외부 입력을 받아 State를 변경하는 함수
    /// </summary>
    /// <returns></returns>
    public ISwordState ChangeState(Sword sword);

    /// <summary>
    /// 이전 State에서 현재 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Enter(Sword sword);

    /// <summary>
    /// 현재 State에서 다른 State로 넘어갈때 실행되는 함수
    /// </summary>
    public void Exit(Sword sword);



}
