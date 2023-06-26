using jsh;

public interface ISwordState
{

    /// <summary>
    /// Update���� ó���� ����
    /// </summary>
    public void Update(Sword sword);

    /// <summary>
    /// FixedUpdate���� ó���� ����
    /// </summary>
    public void FixedUpdate(Sword sword);

    /// <summary>
    /// �ܺ� �Է��� �޾� State�� �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public ISwordState ChangeState(Sword sword);

    /// <summary>
    /// ���� State���� ���� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Enter(Sword sword);

    /// <summary>
    /// ���� State���� �ٸ� State�� �Ѿ�� ����Ǵ� �Լ�
    /// </summary>
    public void Exit(Sword sword);



}
