using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update 문에서 특정한 순서로 이벤트처리가 이루어져야하고 이벤트 수행 단계에서 다른 이벤트 처리가 통과되서는 안되는 경우를 처리한다.
/// 
/// StepAdmin은 JUnity와 협력한다. JUpdate에서 true를 반환하면 Operate 함수는 가입되어있는 JUpdate 함수에서 다음 JUpdate 함수를 호출한다.
/// 그러나 만약 특정 JUpdate가 false를 반환하면 해당 단계까지 실행하고 다음 JUpdate를 통과시키지않는다.
/// 해당 단계의 JUpdate에서 False가 True로 바뀌면 통과 시켜준다.
/// 
/// 
/// 
/// </summary>
public class StepAdmin
{
    public delegate bool JUpdate();

    public List<JUpdate> admin = new List<JUpdate>();
   
    //public event Update admin;


    /// <summary>
    /// Subscribe new event step to list in backward
    /// </summary>
    /// <param name="update"></param>
    public void Subscribe(JUpdate update)
    {
        //admin += update;
        admin.Add(update);

    }

    /// <summary>
    /// admin을 초기화 시킨다.
    /// </summary>
    public void Reset()
    {
        admin = null;

    }

    /// <summary>
    /// admin에 저장되어있는 이벤트들을 실행한다.
    /// </summary>
    public void Operate()
    {
       if(admin.Count > 0)
        {
            for(int i = 0; i < admin.Count; i++)
            {
                
                if (!admin[i]())
                {
                    Debug.Log("지금은 할 수 없는 행동입니다");
                    return;
                }
            }
        }
       
        
    }

}
