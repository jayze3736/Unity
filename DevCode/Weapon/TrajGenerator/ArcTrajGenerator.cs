using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArcTrajGenerator
{

    public struct ArcData
    {
        public Vector3[] points;
        public int size;

       
    }


    /// <summary>
    /// ���ش� - �󸶳� ���� ����� ȣ�� �����ϴ°� ������ degree
    /// </summary>
    
    int resolution;

    
    float radius;

    [SerializeField]
    int fadeRate;

    


  

    public void Init(int resolution, float radius)
    {

        this.resolution = resolution;
        this.radius = radius;

        
    }


    /// <summary>
    /// ȣ ������ ������ �������� �Լ�
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="center"></param>
    /// <param name="endPos"></param>
    public ArcData GetArcData2D(Vector2 startPos, Vector2 center, Vector2 endPos)
    {



        // ���� ������ �������� �˰����� -> startPos, endPos�� ������ �˾ƾ���
        // ���� �߽ɰ� �������� �˰�����


        // �������� ������ ���� ���͸� ����
        Vector2 sDir = (startPos - center).normalized; // ���� ��ġ�� ���� ����
        Vector2 eDir = (endPos - center).normalized; // �� ��ġ�� ���� ����


        // ���⺤�ͷκ��� ���� �������� ���� �������� ����
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG; // center�� �������� startPos�� ����Ű�� ���� ������ ���� [rad], ������ -180 ~ 180�� ����
        float endAng = Mathf.Atan2(eDir.y, eDir.x) * Constant.RAD2DEG; // center�� �������� EndPos�� ����Ű�� ���� ������ ���� [rad]


        // �������� ���� ���� ������ 0���� 360���� ����
        startAng = Constant.NormalizeDegree(startAng);
        endAng = Constant.NormalizeDegree(endAng);




        // x = r * cos(theta) �� y =  r * sin(theta) ��



        // startAng�� ������ endAng���� �۴ٰ� ���� ����
        // ���� startAng�� endAng���� ũ�� �ٲ��ش�.
        // startAng�� ������ ���� ���� �ǹ��Ѵ�. endAng�� ������ ū ���� �ǹ��Ѵ�.
        if (startAng > endAng)
        {
            
            float tmp = startAng;
            startAng = endAng;
            endAng = tmp; 


        }



        // 2. ������ ū �Ϳ��� ���� ���� ����. �ش� ������� ������ ���� ���������� ū �������� �����ؾ��� �� �������� �ǹ��Ѵ�.
        float diff = endAng - startAng; //[deg], diff is larger than 0

        Vector3[] points;

        float theta = startAng;

        points = new Vector3[resolution];

        // �� ������ �մ� ��� �߿� ���� ª������ ���ؾ��� 
        // ���� ���� ���̰� 180�� �̻� ���̰� ���� �ݽð�������� ���ƾ��ϰ� �ƴϸ� �ð�������� ���ƾ���
        if (diff > 180.0f) // �ݽð� �������� ����, ���� ���� ���̰� 180�� �̻��϶� ������ StartAng�� ���� ����, endAng�� ���� ����
        {
            
            diff = 360.0f - diff;
            // resolution�� diff�� ����� ������, ����� ������ŭ �迭�� �����Ǿ����
            
            for (int i = 0; i < resolution; i++)
            {

                float x = center.x + radius * Mathf.Cos(theta * Constant.DEG2RAD); // ȣ�� ������ x ��ǥ
                float y = center.y + radius * Mathf.Sin(theta * Constant.DEG2RAD); // ȣ�� ������ y ��ǥ
                Vector3 point = new Vector3(x, y, 0);

                //Debug.Log(diff);
                //Debug.Log(resolution);
                //Debug.Log("CCW" + (diff / resolution));
                points[i] = point;
                theta -= (diff / resolution);


            }



        }
        else // �ð� �������� ����
        {
            

            for (int i = 0; i < resolution; i++)
            {

                float x = center.x + radius * Mathf.Cos(theta * Constant.DEG2RAD); // ȣ�� ������ x ��ǥ
                float y = center.y + radius * Mathf.Sin(theta * Constant.DEG2RAD); // ȣ�� ������ y ��ǥ
                Vector3 point = new Vector3(x, y, 0);

                //Debug.Log(diff);
                //Debug.Log(resolution);
                //Debug.Log("CCW" + (diff / resolution));
                points[i] = point;
                theta += (diff / resolution);


            }

            //���������� for���� �ݺ��� Ƚ����ŭ �迭�� ũ�⸦ ����
        }

        ArcData data = new ArcData();
        data.points = points;
        data.size = resolution;


        return data;









    }

    public void DrawArc2D(Vector2 startPos, Vector2 center, Vector2 endPos, LineRenderer lr, float lineWidth = 0.7F)
    {

        ArcData arc = GetArcData2D(startPos, center, endPos);
        

        // startAng�� ū�� EndPos�� ū��?

        lr.positionCount = arc.size;
        lr.SetPositions(arc.points);
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        Color sColor = lr.startColor;
        Color eColor = lr.endColor;

        lr.startColor = new Color(sColor.r, sColor.g, sColor.b, 1.0f);
        lr.endColor = new Color(eColor.r, eColor.g, eColor.b, 0.0f);


    }

    public void ClearArc2D(LineRenderer lr)
    {
        lr.positionCount = 0;
        
    }

    public IEnumerator FadeAwayArc2D(LineRenderer lr)
    {
        Debug.Log("Fade Away");

        Color sColor = lr.startColor;
        Color eColor = lr.endColor;
        float alpha = 1.0f;
        

        for(int i = 0; i < fadeRate; i++)
        {

            lr.startColor = new Color(sColor.r, sColor.g, sColor.b, alpha);
            lr.endColor = new Color(eColor.r, eColor.g, eColor.b, alpha);
            
            alpha -= (1.0f / fadeRate);


            if(alpha < 0.0f)
            {
                alpha = 0.0f;
            }
            
            yield return null;

        }

        alpha = 0.0f;
        lr.startColor = new Color(sColor.r, sColor.g, sColor.b, alpha);
        lr.endColor = new Color(eColor.r, eColor.g, eColor.b, alpha);


    }





  
}
