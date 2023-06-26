using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����� ó���ϰ� �����ϴ� Ŭ����
/// </summary>
public class AttackHandler
{
    // �ٽ����� ����� Raycast�� �̿��Ͽ� ���� ������ �����ϰ� ó���Ѵ�.

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
    /// �߽ɰ� �������� �ָ� ������ ���� ������ ����
    /// </summary>
    public void DetectEnemyInCircleRange()
    {

    }

    /// <summary>
    /// �ΰ��� ���� �ָ� ���� ������ ���� ���� ����
    /// </summary>
    public void DetectEnemyInLineRange()
    {

    }


    /// <summary>
    /// �װ��� ���� �ָ� �簢�� ������ ���� ���� ����
    /// </summary>
    public void DetectEnemyInRectRange(Vector3 LtTop, Vector3 RtTop, Vector3 LtLow, Vector3 RtLow)
    {
 


    }

    public Stack<Enemy> DetectEnemyInArcRange(Vector2 startPos, Vector2 center, Vector2 endPos, int resolution, float distance)
    {
        
        Vector2 sDir = (startPos - center).normalized; // ���� ��ġ�� ���� ����
        Vector2 eDir = (endPos - center).normalized; // �� ��ġ�� ���� ����


        // ���⺤�ͷκ��� ���� �������� ���� �������� ����
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG; // center�� �������� startPos�� ����Ű�� ���� ������ ���� [rad], ������ -180 ~ 180�� ����
        float endAng = Mathf.Atan2(eDir.y, eDir.x) * Constant.RAD2DEG;

        // �������� ���� ���� ������ 0���� 360���� ����
        startAng = Constant.NormalizeDegree(startAng);
        endAng = Constant.NormalizeDegree(endAng);


        if (startAng > endAng)
        {

            float tmp = startAng;
            startAng = endAng;
            endAng = tmp;


        }

        // 2. ������ ū �Ϳ��� ���� ���� ����. �ش� ������� ������ ���� ���������� ū �������� �����ؾ��� �� �������� �ǹ��Ѵ�.
        float diff = endAng - startAng;
        float theta = startAng;

        // ��� Raycast ���� ����
        Dictionary<string, RaycastHit2D> objNameRayPairs = new Dictionary<string, RaycastHit2D>();

        // ������ ��µ� ���
        Stack<Enemy> enemies = new Stack<Enemy>();

        //LayData[] data = new LayData[resolution];

        // �� ������ �մ� ��� �߿� ���� ª������ ���ؾ��� 
        // ���� ���� ���̰� 180�� �̻� ���̰� ���� �ݽð�������� ���ƾ��ϰ� �ƴϸ� �ð�������� ���ƾ���
        if (diff > 180.0f) // �ݽð� �������� ����, ���� ���� ���̰� 180�� �̻��϶� ������ StartAng�� ���� ����, endAng�� ���� ����
        {

            diff = 360.0f - diff;
            // resolution�� diff�� ����� ������, ����� ������ŭ �迭�� �����Ǿ����

            for (int i = 0; i < resolution; i++)
            {
                theta -= (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // ȣ�� ������ x ��ǥ
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // ȣ�� ������ y ��ǥ
                Vector3 dir = new Vector3(x, y, 0);

               
                RaycastHit2D [] hits = Physics2D.RaycastAll(center, dir, distance);

                foreach(RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        // name�� ������ ������ ���� ����
                        string name = hit.transform.name;

                        // ���ο� ������Ʈ���(������ ����Ǿ��ִ� ������Ʈ�� �ƴ϶��)
                        if (!objNameRayPairs.ContainsKey(name))
                        {
                            objNameRayPairs[name] = hit;

                        }


                    }

                }

               

                


                


                // if shot object is enemy then attack ó��


            }

           

        }
        else // �ð� �������� ����
        {


            for (int i = 0; i < resolution; i++)
            {
                theta += (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // ȣ�� ������ x ��ǥ
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // ȣ�� ������ y ��ǥ
                Vector3 dir = new Vector3(x, y, 0);

                RaycastHit2D[] hits = Physics2D.RaycastAll(center, dir, distance);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        // name�� ������ ������ ���� ����
                        string name = hit.transform.name;

                        // ���ο� ������Ʈ���(������ ����Ǿ��ִ� ������Ʈ�� �ƴ϶��)
                        if (!objNameRayPairs.ContainsKey(name))
                        {
                            objNameRayPairs[name] = hit;

                        }


                    }

                }






            }

            
        }

        // ��� ������ ������Ʈ�鿡 ���Ͽ� Enemy���� ���͸��� ��  
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
        Vector2 sDir = (startPos - center).normalized; // ���� ��ġ�� ���� ����
        Vector2 eDir = (endPos - center).normalized; // �� ��ġ�� ���� ����


        // ���⺤�ͷκ��� ���� �������� ���� �������� ����
        float startAng = Mathf.Atan2(sDir.y, sDir.x) * Constant.RAD2DEG; // center�� �������� startPos�� ����Ű�� ���� ������ ���� [rad], ������ -180 ~ 180�� ����
        float endAng = Mathf.Atan2(eDir.y, eDir.x) * Constant.RAD2DEG;

        // �������� ���� ���� ������ 0���� 360���� ����
        startAng = Constant.NormalizeDegree(startAng);
        endAng = Constant.NormalizeDegree(endAng);


        if (startAng > endAng)
        {

            float tmp = startAng;
            startAng = endAng;
            endAng = tmp;


        }

        // 2. ������ ū �Ϳ��� ���� ���� ����. �ش� ������� ������ ���� ���������� ū �������� �����ؾ��� �� �������� �ǹ��Ѵ�.
        float diff = endAng - startAng;
        float theta = startAng;

        

        //LayData[] data = new LayData[resolution];

        // �� ������ �մ� ��� �߿� ���� ª������ ���ؾ��� 
        // ���� ���� ���̰� 180�� �̻� ���̰� ���� �ݽð�������� ���ƾ��ϰ� �ƴϸ� �ð�������� ���ƾ���
        if (diff > 180.0f) // �ݽð� �������� ����, ���� ���� ���̰� 180�� �̻��϶� ������ StartAng�� ���� ����, endAng�� ���� ����
        {

            diff = 360.0f - diff;
            // resolution�� diff�� ����� ������, ����� ������ŭ �迭�� �����Ǿ����

            for (int i = 0; i < resolution; i++)
            {
                theta -= (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // ȣ�� ������ x ��ǥ
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // ȣ�� ������ y ��ǥ
                Vector3 dir = new Vector3(x, y, 0);


                Debug.DrawRay(center, dir * distance, color);



                // if shot object is enemy then attack ó��


            }



        }
        else // �ð� �������� ����
        {


            for (int i = 0; i < resolution; i++)
            {
                theta += (diff / resolution);
                float x = Mathf.Cos(theta * Constant.DEG2RAD); // ȣ�� ������ x ��ǥ
                float y = Mathf.Sin(theta * Constant.DEG2RAD); // ȣ�� ������ y ��ǥ
                Vector3 dir = new Vector3(x, y, 0);

                Debug.DrawRay(center, dir * distance, color);




            }


        }


      





    }



}
