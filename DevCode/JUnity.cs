using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 외부에서 JStart함수와 JUpdate함수가 호출되도록 하는 인터페이스
/// </summary>
public interface JUnity
{
    

    // Start is called before the first frame update
    public void JStart(Transform transform);

    // Update is called once per frame
    public bool JUpdate();
}
