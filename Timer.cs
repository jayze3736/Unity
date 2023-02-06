using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update 문에서 시간을 세고 해당 시간이 되면 알려주는 클래스
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
    /// 설정한 시간이 되면 true를 리턴함
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
