using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ܺο��� JStart�Լ��� JUpdate�Լ��� ȣ��ǵ��� �ϴ� �������̽�
/// </summary>
public interface JUnity
{
    

    // Start is called before the first frame update
    public void JStart(Transform transform);

    // Update is called once per frame
    public bool JUpdate();
}
