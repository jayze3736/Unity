using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attack Guide 오브젝트를 조작하고 관리하는 클래스
/// </summary>
public class CircleTrajectoryController : AttackTrajectoryController
{
    Vector3 center;
    double radius;

    double ang = 0;

    // 회전 속도 - 튜닝해야함
    double speed = 1;

    float r { get => (float)radius; }

    Vector3 vDist;


    public void Init(Vector3 center, double radius, double speed)
    {
        this.center = center;
        this.radius = radius;   
        this.speed = speed;
       

    }

    /// <summary>
    /// Transform을 회전시키는 함수
    /// </summary>
    /// <param name="t"></param>
    public void Rotate(GuideObject obj)
    {

        // 
        
            ang += speed;
            float x = r * Mathf.Cos((float)ang * Constant.DEG2RAD);
            float y = r * Mathf.Sin((float)ang * Constant.DEG2RAD);

            obj.SetWorldPosition(new Vector3(center.x + x, center.y + y));

            if (ang >= 360)
            {
                ang = ang % 360.0;
            }





    }

    public void CWRotate(GuideObject obj)
    {

        // 

        ang -= speed;
        float x = r * Mathf.Cos((float)ang * Constant.DEG2RAD);
        float y = r * Mathf.Sin((float)ang * Constant.DEG2RAD);

        obj.SetWorldPosition(new Vector3(center.x + x, center.y + y));

        if (ang <= -360.0)
        {
            ang = -1*( (-1 * ang) % 360.0);
        }





    }



    /// <summary>
    /// 가이드 오브젝트의 상대위치를 저장한다.
    /// </summary>
    /// <param name="obj"></param>
    public void SaveCurrentPosition(GuideObject obj)
    {
        
        vDist = obj.GetWorldPosition() - center;

    }
    
    /// <summary>
    /// center를 원점으로 vDist만큼 이동한 위치에 해당 오브젝트를 위치시킴
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="center"></param>
    public void Stop(GuideObject obj, Vector3 center)
    {
       
        obj.SetWorldPosition(center + vDist);


    }

    /// <summary>
    /// Freeze 상태에서 오브젝트를 해방
    /// </summary>
    /// <param name="obj"></param>
    public void Release(GuideObject obj)
    {
        
    }

    
   







}
