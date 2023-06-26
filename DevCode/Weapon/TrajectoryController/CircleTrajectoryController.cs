using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attack Guide ������Ʈ�� �����ϰ� �����ϴ� Ŭ����
/// </summary>
public class CircleTrajectoryController : AttackTrajectoryController
{
    Vector3 center;
    double radius;

    double ang = 0;

    // ȸ�� �ӵ� - Ʃ���ؾ���
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
    /// Transform�� ȸ����Ű�� �Լ�
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
    /// ���̵� ������Ʈ�� �����ġ�� �����Ѵ�.
    /// </summary>
    /// <param name="obj"></param>
    public void SaveCurrentPosition(GuideObject obj)
    {
        
        vDist = obj.GetWorldPosition() - center;

    }
    
    /// <summary>
    /// center�� �������� vDist��ŭ �̵��� ��ġ�� �ش� ������Ʈ�� ��ġ��Ŵ
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="center"></param>
    public void Stop(GuideObject obj, Vector3 center)
    {
       
        obj.SetWorldPosition(center + vDist);


    }

    /// <summary>
    /// Freeze ���¿��� ������Ʈ�� �ع�
    /// </summary>
    /// <param name="obj"></param>
    public void Release(GuideObject obj)
    {
        
    }

    
   







}
