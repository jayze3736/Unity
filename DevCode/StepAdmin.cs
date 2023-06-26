using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update ������ Ư���� ������ �̺�Ʈó���� �̷�������ϰ� �̺�Ʈ ���� �ܰ迡�� �ٸ� �̺�Ʈ ó���� ����Ǽ��� �ȵǴ� ��츦 ó���Ѵ�.
/// 
/// StepAdmin�� JUnity�� �����Ѵ�. JUpdate���� true�� ��ȯ�ϸ� Operate �Լ��� ���ԵǾ��ִ� JUpdate �Լ����� ���� JUpdate �Լ��� ȣ���Ѵ�.
/// �׷��� ���� Ư�� JUpdate�� false�� ��ȯ�ϸ� �ش� �ܰ���� �����ϰ� ���� JUpdate�� �����Ű���ʴ´�.
/// �ش� �ܰ��� JUpdate���� False�� True�� �ٲ�� ��� �����ش�.
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
    /// admin�� �ʱ�ȭ ��Ų��.
    /// </summary>
    public void Reset()
    {
        admin = null;

    }

    /// <summary>
    /// admin�� ����Ǿ��ִ� �̺�Ʈ���� �����Ѵ�.
    /// </summary>
    public void Operate()
    {
       if(admin.Count > 0)
        {
            for(int i = 0; i < admin.Count; i++)
            {
                
                if (!admin[i]())
                {
                    Debug.Log("������ �� �� ���� �ൿ�Դϴ�");
                    return;
                }
            }
        }
       
        
    }

}
