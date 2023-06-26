using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RevoluteJoint
{


    public void PointToMouse(Vector2 pivot, Transform t)
    {
        //ù��° ���콺 ������ ��ġ�� �����´�.
        Vector2 mousePos = CameraInstance.cam.ScreenToWorldPoint(Input.mousePosition);


        //�ι�° pivot�� ���콺 �����Ͱ��� ���� ���͸� ����
        Vector2 vDir = (mousePos - pivot).normalized;

        //����° ���� ���͸� �ؼ��Ͽ� Rotation���� ���ϰ� ����
        float ang = Mathf.Atan2(vDir.y, vDir.x) * (float)Constant.RAD2DEG;

        t.rotation = Quaternion.Euler(0, 0, ang);



    }



    public void PointToPoint(Vector2 pivot, Vector2 point, Transform t)
    {
       

        //�ι�° pivot�� ���콺 �����Ͱ��� ���� ���͸� ����
        Vector2 vDir = (point - pivot).normalized;

        //����° ���� ���͸� �ؼ��Ͽ� Rotation���� ���ϰ� ����
        float ang = Mathf.Atan2(vDir.y, vDir.x) * (float)Constant.RAD2DEG;

        t.rotation = Quaternion.Euler(0, 0, ang);



    }

    /// <summary>
    /// uDir�� ����� ��ġ�ϵ��� ȸ��
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
    ///// �ð� �������� ������ ȸ����Ű�� ��ƾ
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

    //    // ȸ�� ������ �ð� ����
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

        // �� ���·� ����
        joint.rotation = Quaternion.Euler(0, 0, 0);



    }



}
