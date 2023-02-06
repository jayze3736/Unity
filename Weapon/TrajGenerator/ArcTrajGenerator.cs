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
    /// 분해능 - 얼마나 많은 점들로 호를 구성하는가 단위는 degree
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
    /// 호 궤적의 점들을 가져오는 함수
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="center"></param>
    /// <param name="endPos"></param>
    public ArcData GetArcData2D(Vector2 startPos, Vector2 center, Vector2 endPos)
    {



        // 시작 지점과 끝지점을 알고있음 -> startPos, endPos의 각도를 알아야함
        // 원의 중심과 반지름을 알고있음


        // 시작점과 끝점의 방향 벡터를 구함
        Vector2 sDir = (startPos - center).normalized; // 시작 위치의 방향 벡터
        Vector2 eDir = (endPos - center).normalized; // 끝 위치의 방향 벡터


        // 방향벡터로부터 시작 각도값과 끝점 각도값을 구함
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG; // center를 기준으로 startPos가 가리키는 방향 벡터의 각도 [rad], 범위는 -180 ~ 180도 까지
        float endAng = Mathf.Atan2(eDir.y, eDir.x) * Constant.RAD2DEG; // center를 기준으로 EndPos가 가리키는 방향 벡터의 각도 [rad]


        // 직관성을 위해 각도 범위를 0에서 360도로 변경
        startAng = Constant.NormalizeDegree(startAng);
        endAng = Constant.NormalizeDegree(endAng);




        // x = r * cos(theta) 고 y =  r * sin(theta) 임



        // startAng이 무조건 endAng보다 작다고 보장 못함
        // 따라서 startAng이 endAng보다 크면 바꿔준다.
        // startAng은 각도가 작은 값을 의미한다. endAng은 각도가 큰 값을 의미한다.
        if (startAng > endAng)
        {
            
            float tmp = startAng;
            startAng = endAng;
            endAng = tmp; 


        }



        // 2. 각도가 큰 것에서 작은 것은 뺀다. 해당 결과값은 각도가 작은 지점서부터 큰 지점까지 증가해야할 총 변위각을 의미한다.
        float diff = endAng - startAng; //[deg], diff is larger than 0

        Vector3[] points;

        float theta = startAng;

        points = new Vector3[resolution];

        // 두 지점을 잇는 경로 중에 가장 짧은쪽을 택해야함 
        // 둘의 각도 차이가 180도 이상 차이가 나면 반시계방향으로 돌아야하고 아니면 시계방향으로 돌아야함
        if (diff > 180.0f) // 반시계 방향으로 더함, 둘의 각도 차이가 180도 이상일때 무조건 StartAng은 음의 각도, endAng은 양의 각도
        {
            
            diff = 360.0f - diff;
            // resolution은 diff를 등분한 개수로, 등분한 개수만큼 배열이 생성되어야함
            
            for (int i = 0; i < resolution; i++)
            {

                float x = center.x + radius * Mathf.Cos(theta * Constant.DEG2RAD); // 호의 자취의 x 좌표
                float y = center.y + radius * Mathf.Sin(theta * Constant.DEG2RAD); // 호의 자취의 y 좌표
                Vector3 point = new Vector3(x, y, 0);

                //Debug.Log(diff);
                //Debug.Log(resolution);
                //Debug.Log("CCW" + (diff / resolution));
                points[i] = point;
                theta -= (diff / resolution);


            }



        }
        else // 시계 방향으로 더함
        {
            

            for (int i = 0; i < resolution; i++)
            {

                float x = center.x + radius * Mathf.Cos(theta * Constant.DEG2RAD); // 호의 자취의 x 좌표
                float y = center.y + radius * Mathf.Sin(theta * Constant.DEG2RAD); // 호의 자취의 y 좌표
                Vector3 point = new Vector3(x, y, 0);

                //Debug.Log(diff);
                //Debug.Log(resolution);
                //Debug.Log("CCW" + (diff / resolution));
                points[i] = point;
                theta += (diff / resolution);


            }

            //최종적으로 for문이 반복된 횟수만큼 배열의 크기를 설정
        }

        ArcData data = new ArcData();
        data.points = points;
        data.size = resolution;


        return data;









    }

    public void DrawArc2D(Vector2 startPos, Vector2 center, Vector2 endPos, LineRenderer lr, float lineWidth = 0.7F)
    {

        ArcData arc = GetArcData2D(startPos, center, endPos);
        

        // startAng이 큰지 EndPos가 큰지?

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
