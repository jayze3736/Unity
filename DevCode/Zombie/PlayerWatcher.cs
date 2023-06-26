using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Player�� ���ϵ��� Scale�� �����ϴ� ��ü
/// </summary>
[System.Serializable]
public class PlayerWatcher
{

    [SerializeField]
    LayerMask playerLayer;

    // üũ/��üũ
    [SerializeField]
    float xFindSightRange;

    [SerializeField]
    float xPlayerNearRange;


    public float FindRange { get => xFindSightRange; }
    public float PlayerRange { get => xPlayerNearRange; }

   

    /// <summary>
    /// ������ ���⿡�� Player�� ã���� True�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="player"></param>
    public bool FindPlayerOnRightDir(Vector3 curPos, float findDist)
    {

        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, findDist, playerLayer); // ���� ���� ���
 

        return rHit ? true : false;

    }

    /// <summary>
    /// ������ ���⿡�� Player�� ã���� True�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="player"></param>
    public bool FindPlayerOnRightDir(Vector3 curPos)
    {

        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, xFindSightRange, playerLayer); // ���� ���� ���

        return rHit ? true : false;

    }



    /// <summary>
    /// ���ʹ��⿡�� Player�� ã���� True�� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    public bool FindPlayerOnLeftDir(Vector3 curPos, float findDist)
    {

       
        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, findDist, playerLayer); // ���� ���� ���

        return lHit ? true : false;



    }

    /// <summary>
    /// ���ʹ��⿡�� Player�� ã���� True�� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    public bool FindPlayerOnLeftDir(Vector3 curPos)
    {


        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, xFindSightRange, playerLayer); // ���� ���� ���


        return lHit ? true : false;



    }


    public void DebugRay(Vector2 dir, Vector3 curPos, float dist)
    {
        Vector3 RangeOnDebug = new Vector3(curPos.x, curPos.y - 0.3f, curPos.z);
        Debug.DrawRay(RangeOnDebug, dir * dist, Color.blue);

    }


    /// <summary>
    /// Player�� ���� ������ ���� ������ ������ True�� ��ȯ�ϴ� �޼ҵ�
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="rangeDist"></param>
    /// <returns></returns>
    public bool PlayerInRange(Vector3 curPos, float findDist)
    {
        
        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, findDist, playerLayer); // ���� ���� ���
        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, findDist, playerLayer); // ���� ���� ���

        Vector3 RangeOnDebug = new Vector3(curPos.x, curPos.y - 0.1f, curPos.z);

       
        Debug.DrawRay(RangeOnDebug, Vector3.right * findDist, Color.red);
        Debug.DrawRay(RangeOnDebug, Vector3.left * findDist, Color.red);



        return (rHit || lHit) ? true : false;




    }

    /// <summary>
    /// Player�� ���� ������ ���� ������ ������ Player�� ��ȯ�ϴ� �޼ҵ�
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="rangeDist"></param>
    /// <returns></returns>
    public KnightPlayer FindPlayerNearMe(Vector3 curPos)
    {

        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, xPlayerNearRange, playerLayer); // ���� ���� ���
        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, xPlayerNearRange, playerLayer); // ���� ���� ���

        Vector3 RangeOnDebug = new Vector3(curPos.x, curPos.y - 0.1f, curPos.z);

        Debug.DrawRay(RangeOnDebug, Vector3.right * xPlayerNearRange, Color.red);
        Debug.DrawRay(RangeOnDebug, Vector3.left * xPlayerNearRange, Color.red);

        if (rHit)
        {
            return rHit.transform.GetComponent<KnightPlayer>();
        }
        else if (lHit)
        {
            return lHit.transform.GetComponent<KnightPlayer>();
        }
        else
        {
            return null;
        }
        

    }


}
