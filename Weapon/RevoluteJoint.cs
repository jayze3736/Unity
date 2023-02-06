using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RevoluteJoint
{


    public void PointToMouse(Vector2 pivot, Transform t)
    {
        //첫번째 마우스 포인터 위치를 가져온다.
        Vector2 mousePos = CameraInstance.cam.ScreenToWorldPoint(Input.mousePosition);


        //두번째 pivot과 마우스 포인터간의 방향 벡터를 생성
        Vector2 vDir = (mousePos - pivot).normalized;

        //세번째 방향 벡터를 해석하여 Rotation값을 구하고 세팅
        float ang = Mathf.Atan2(vDir.y, vDir.x) * (float)Constant.RAD2DEG;

        t.rotation = Quaternion.Euler(0, 0, ang);



    }



    public void PointToPoint(Vector2 pivot, Vector2 point, Transform t)
    {
       

        //두번째 pivot과 마우스 포인터간의 방향 벡터를 생성
        Vector2 vDir = (point - pivot).normalized;

        //세번째 방향 벡터를 해석하여 Rotation값을 구하고 세팅
        float ang = Mathf.Atan2(vDir.y, vDir.x) * (float)Constant.RAD2DEG;

        t.rotation = Quaternion.Euler(0, 0, ang);



    }

    /// <summary>
    /// uDir의 방향과 일치하도록 회전
    /// </summary>
    /// <param name="uDir"></param>
    /// <param name="point"></param>
    /// <param name="t"></param>
    public void PointToDir(Vector2 uDir, Transform t)
    {
        uDir = uDir.normalized;
        float ang = Mathf.Atan2(uDir.y, uDir.x) * (float)Constant.RAD2DEG;

        t.rotation = Quaternion.Euler(0, 0, ang);
        


    }

    ///// <summary>
    ///// 시계 방향으로 관절을 회전시키는 루틴
    ///// </summary>
    ///// <returns></returns>
    //public IEnumerator JointRotate(Vector2 sDir, Vector2 eDir, int resolution, Transform joint)
    //{
    //    sDir = sDir.normalized;
    //    float startAng = Mathf.Atan2(sDir.y, sDir.x) * (float)Constant.RAD2DEG;

    //    eDir = eDir.normalized;
    //    float endAng = Mathf.Atan2(eDir.y, eDir.x) * (float)Constant.RAD2DEG;

    //    startAng = Constant.NormalizeDegree(startAng);
    //    endAng = Constant.NormalizeDegree(endAng);

    //    if(startAng > endAng)
    //    {
    //        float tmp = endAng;
    //        endAng = startAng;
    //        startAng = tmp;

    //    }

    //    float diff = endAng - startAng;

    //    // 회전 방향은 시계 방향
    //    float ang = endAng;

       
    //    for (int i = 0; i < resolution; i++)
    //    {
    //        ang -= (diff / resolution);
    //        joint.rotation = Quaternion.Euler(0, 0, ang);
    //        yield return null;
    //    }



    //}


    public IEnumerator JointRotateCycle(Vector2 sDir, int resolution, Transform joint)
    {
        sDir = sDir.normalized;
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG;

        startAng = Constant.NormalizeDegree(startAng);
        float diff = 360f + startAng;
        
        for (int i = 0; i < resolution; i++)
        {
            startAng -= (diff / resolution);
            joint.rotation = Quaternion.Euler(0, 0, startAng);
            yield return null;
        }

        // 원 상태로 복구
        joint.rotation = Quaternion.Euler(0, 0, 0);



    }



}
