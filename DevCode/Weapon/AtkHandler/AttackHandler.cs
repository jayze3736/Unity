using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격 방법을 처리하고 설계하는 클래스
/// </summary>
public class AttackHandler
{
    // 핵심적인 기능은 Raycast를 이용하여 공격 판정을 설계하고 처리한다.

    public struct RayData
    {
        public Vector3 start;
        public Vector3 dir;
        public Color color;

    }

    


    // Start is called before the first frame update





    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public bool Attack(Enemy enemy, int damage)
    {
        
        if (enemy != null)
        {
            //enemy.ReceiveDamage(damage);
        }
        
            

        return true;
    }

    /// <summary>
    /// 중심과 반지름을 주면 원형의 공격 범위를 생성
    /// </summary>
    public void DetectEnemyInCircleRange()
    {

    }

    /// <summary>
    /// 두개의 점을 주면 직선 형태의 공격 범위 생성
    /// </summary>
    public void DetectEnemyInLineRange()
    {

    }


    /// <summary>
    /// 네개의 점을 주면 사각형 형태의 공격 범위 생성
    /// </summary>
    public void DetectEnemyInRectRange(Vector3 LtTop, Vector3 RtTop, Vector3 LtLow, Vector3 RtLow)
    {
 


    }

    public Stack<Enemy> DetectEnemyInArcRange(Vector2 startPos, Vector2 center, Vector2 endPos, int resolution, float distance)
    {
        
        Vector2 sDir = (startPos - center).normalized; // 시작 위치의 방향 벡터
        Vector2 eDir = (endPos - center).normalized; // 끝 위치의 방향 벡터


        // 방향벡터로부터 시작 각도값과 끝점 각도값을 구함
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG; // center를 기준으로 startPos가 가리키는 방향 벡터의 각도 [rad], 범위는 -180 ~ 180도 까지
        float endAng = Mathf.Atan2(eDir.y, eDir.x) * Constant.RAD2DEG;

        // 직관성을 위해 각도 범위를 0에서 360도로 변경
        startAng = Constant.NormalizeDegree(startAng);
        endAng = Constant.NormalizeDegree(endAng);


        if (startAng > endAng)
        {

            float tmp = startAng;
            startAng = endAng;
            endAng = tmp;


        }

        // 2. 각도가 큰 것에서 작은 것은 뺀다. 해당 결과값은 각도가 작은 지점서부터 큰 지점까지 증가해야할 총 변위각을 의미한다.
        float diff = endAng - startAng;
        float theta = startAng;

        // 모든 Raycast 정보 저장
        Dictionary<string, RaycastHit2D> objNameRayPairs = new Dictionary<string, RaycastHit2D>();

        // 실제로 출력될 결과
        Stack<Enemy> enemies = new Stack<Enemy>();

        //LayData[] data = new LayData[resolution];

        // 두 지점을 잇는 경로 중에 가장 짧은쪽을 택해야함 
        // 둘의 각도 차이가 180도 이상 차이가 나면 반시계방향으로 돌아야하고 아니면 시계방향으로 돌아야함
        if (diff > 180.0f) // 반시계 방향으로 더함, 둘의 각도 차이가 180도 이상일때 무조건 StartAng은 음의 각도, endAng은 양의 각도
        {

            diff = 360.0f - diff;
            // resolution은 diff를 등분한 개수로, 등분한 개수만큼 배열이 생성되어야함

            for (int i = 0; i < resolution; i++)
            {
                theta -= (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // 호의 자취의 x 좌표
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // 호의 자취의 y 좌표
                Vector3 dir = new Vector3(x, y, 0);

               
                RaycastHit2D [] hits = Physics2D.RaycastAll(center, dir, distance);

                foreach(RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        // name은 씬에서 고유의 값을 가짐
                        string name = hit.transform.name;

                        // 새로운 오브젝트라면(기존에 저장되어있던 오브젝트가 아니라면)
                        if (!objNameRayPairs.ContainsKey(name))
                        {
                            objNameRayPairs[name] = hit;

                        }


                    }

                }

               

                


                


                // if shot object is enemy then attack 처리


            }

           

        }
        else // 시계 방향으로 더함
        {


            for (int i = 0; i < resolution; i++)
            {
                theta += (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // 호의 자취의 x 좌표
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // 호의 자취의 y 좌표
                Vector3 dir = new Vector3(x, y, 0);

                RaycastHit2D[] hits = Physics2D.RaycastAll(center, dir, distance);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        // name은 씬에서 고유의 값을 가짐
                        string name = hit.transform.name;

                        // 새로운 오브젝트라면(기존에 저장되어있던 오브젝트가 아니라면)
                        if (!objNameRayPairs.ContainsKey(name))
                        {
                            objNameRayPairs[name] = hit;

                        }


                    }

                }






            }

            
        }

        // 모든 고유의 오브젝트들에 대하여 Enemy만을 필터링한 후  
        foreach(KeyValuePair<string, RaycastHit2D> pair in objNameRayPairs)
        {
            
            Enemy enemy = pair.Value.transform.GetComponent<Enemy>();

            // what happens if i insert null into stack?
            
            if(enemy != null)
            {
                enemies.Push(enemy);
            }
            
        }


        return enemies;
        


    }


    public void VisualizeArcAtkRange(Vector2 startPos, Vector2 center, Vector2 endPos, int resolution, float distance, Color color)
    {
        Vector2 sDir = (startPos - center).normalized; // 시작 위치의 방향 벡터
        Vector2 eDir = (endPos - center).normalized; // 끝 위치의 방향 벡터


        // 방향벡터로부터 시작 각도값과 끝점 각도값을 구함
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG; // center를 기준으로 startPos가 가리키는 방향 벡터의 각도 [rad], 범위는 -180 ~ 180도 까지
        float endAng = Mathf.Atan2(eDir.y, eDir.x) * Constant.RAD2DEG;

        // 직관성을 위해 각도 범위를 0에서 360도로 변경
        startAng = Constant.NormalizeDegree(startAng);
        endAng = Constant.NormalizeDegree(endAng);


        if (startAng > endAng)
        {

            float tmp = startAng;
            startAng = endAng;
            endAng = tmp;


        }

        // 2. 각도가 큰 것에서 작은 것은 뺀다. 해당 결과값은 각도가 작은 지점서부터 큰 지점까지 증가해야할 총 변위각을 의미한다.
        float diff = endAng - startAng;
        float theta = startAng;

        

        //LayData[] data = new LayData[resolution];

        // 두 지점을 잇는 경로 중에 가장 짧은쪽을 택해야함 
        // 둘의 각도 차이가 180도 이상 차이가 나면 반시계방향으로 돌아야하고 아니면 시계방향으로 돌아야함
        if (diff > 180.0f) // 반시계 방향으로 더함, 둘의 각도 차이가 180도 이상일때 무조건 StartAng은 음의 각도, endAng은 양의 각도
        {

            diff = 360.0f - diff;
            // resolution은 diff를 등분한 개수로, 등분한 개수만큼 배열이 생성되어야함

            for (int i = 0; i < resolution; i++)
            {
                theta -= (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // 호의 자취의 x 좌표
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // 호의 자취의 y 좌표
                Vector3 dir = new Vector3(x, y, 0);


                Debug.DrawRay(center, dir * distance, color);



                // if shot object is enemy then attack 처리


            }



        }
        else // 시계 방향으로 더함
        {


            for (int i = 0; i < resolution; i++)
            {
                theta += (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // 호의 자취의 x 좌표
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // 호의 자취의 y 좌표
                Vector3 dir = new Vector3(x, y, 0);

                Debug.DrawRay(center, dir * distance, color);




            }


        }


      





    }



}
