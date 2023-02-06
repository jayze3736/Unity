using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Player를 향하도록 Scale을 조정하는 객체
/// </summary>
[System.Serializable]
public class PlayerWatcher
{

    [SerializeField]
    LayerMask playerLayer;

    // 체크/언체크
    [SerializeField]
    float xFindSightRange;

    [SerializeField]
    float xPlayerNearRange;


    public float FindRange { get => xFindSightRange; }
    public float PlayerRange { get => xPlayerNearRange; }

   

    /// <summary>
    /// 오른쪽 방향에서 Player를 찾으면 True를 반환한다.
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="player"></param>
    public bool FindPlayerOnRightDir(Vector3 curPos, float findDist)
    {

        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, findDist, playerLayer); // 우측 검출 결과
 

        return rHit ? true : false;

    }

    /// <summary>
    /// 오른쪽 방향에서 Player를 찾으면 True를 반환한다.
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="player"></param>
    public bool FindPlayerOnRightDir(Vector3 curPos)
    {

        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, xFindSightRange, playerLayer); // 우측 검출 결과

        return rHit ? true : false;

    }



    /// <summary>
    /// 왼쪽방향에서 Player를 찾으면 True를 반환한다.
    /// </summary>
    /// <returns></returns>
    public bool FindPlayerOnLeftDir(Vector3 curPos, float findDist)
    {

       
        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, findDist, playerLayer); // 좌측 검출 결과

        return lHit ? true : false;



    }

    /// <summary>
    /// 왼쪽방향에서 Player를 찾으면 True를 반환한다.
    /// </summary>
    /// <returns></returns>
    public bool FindPlayerOnLeftDir(Vector3 curPos)
    {


        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, xFindSightRange, playerLayer); // 좌측 검출 결과


        return lHit ? true : false;



    }


    public void DebugRay(Vector2 dir, Vector3 curPos, float dist)
    {
        Vector3 RangeOnDebug = new Vector3(curPos.x, curPos.y - 0.3f, curPos.z);
        Debug.DrawRay(RangeOnDebug, dir * dist, Color.blue);

    }


    /// <summary>
    /// Player가 공격 범위에 들어올 정도로 가까우면 True를 반환하는 메소드
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="rangeDist"></param>
    /// <returns></returns>
    public bool PlayerInRange(Vector3 curPos, float findDist)
    {
        
        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, findDist, playerLayer); // 우측 검출 결과
        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, findDist, playerLayer); // 좌측 검출 결과

        Vector3 RangeOnDebug = new Vector3(curPos.x, curPos.y - 0.1f, curPos.z);

       
        Debug.DrawRay(RangeOnDebug, Vector3.right * findDist, Color.red);
        Debug.DrawRay(RangeOnDebug, Vector3.left * findDist, Color.red);



        return (rHit || lHit) ? true : false;




    }

    /// <summary>
    /// Player가 공격 범위에 들어올 정도로 가까우면 Player를 반환하는 메소드
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="rangeDist"></param>
    /// <returns></returns>
    public KnightPlayer FindPlayerNearMe(Vector3 curPos)
    {

        RaycastHit2D rHit = Physics2D.Raycast(curPos, Vector2.right, xPlayerNearRange, playerLayer); // 우측 검출 결과
        RaycastHit2D lHit = Physics2D.Raycast(curPos, Vector2.left, xPlayerNearRange, playerLayer); // 좌측 검출 결과

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
