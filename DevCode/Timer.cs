using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update ������ �ð��� ���� �ش� �ð��� �Ǹ� �˷��ִ� Ŭ����
/// </summary>
public class Timer
{

    float curTime;
    float endTime;

    public Timer()
    {
        curTime = 0;
        endTime = 0;
    }

    public Timer(float delay)
    {
        curTime = 0;
        endTime = delay;
    }

    public bool SetTimer(float delay)
    {
        if(curTime != 0)
        {
            Debug.Log("Timer Setting failded");
            return false;
        }
        else
        {
            endTime = delay;
            return true;
        }


    }
    



    /// <summary>
    /// ������ �ð��� �Ǹ� true�� ������
    /// </summary>
    /// <returns></returns>
    public bool Awake()
    {
        if(curTime >= endTime)
        {
            curTime = 0;
            return true;
        }
        else
        {
            curTime += Time.deltaTime;
            return false;
        }


    }




}
