using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWatcherData : ScriptableObject
{
    // <summary>
    /// Ground를 제외하고 나머지 Layer 들은 전부 마스킹 시켜서 Ground만 Ray 반환 결과를 얻도록 한다.
    /// </summary>
    [SerializeField]
    LayerMask groundLayer;

    /// <summary>
    /// 현재 위치와 검출되는 지면까지의 거리의 절댓값을 의미       
    /// </summary>
    [SerializeField]
    float xPeek;

    /// <summary>
    /// Fall 감지를 위해 어느 거리만큼 볼 것인지에 대한 정도
    /// </summary>
    [SerializeField]
    float peekDepth;

    /// <summary>
    /// 현재 붙어야있어야하는 ground
    /// </summary>
    RaycastHit2D ground;


    /// <summary>
    /// 월드 좌표계 기준으로 오른쪽으로 이동시 떨어질 좌표
    /// </summary>
    [SerializeField]
    Vector3 rightFallPos;

    /// <summary>
    /// 월드 좌표계 기준으로 왼쪽으로 이동시 떨어질 좌표
    /// </summary>
    [SerializeField]
    Vector3 leftFallPos;

    public Vector3 RightFallPos { get { return rightFallPos; } }
    public Vector3 LeftFallPos { get { return leftFallPos; } }


    public float Peek { get { return xPeek; } }


    public float absPeekX
    {
        get
        {
            return Mathf.Abs(xPeek);
        }
    }

    Vector3 GetPeekPos(Vector3 curPos, float xPeek)
    {
        float x = curPos.x + xPeek;
        float y = curPos.y;
        float z = curPos.z;

        return new Vector3(x, y, z);


    }



    /// <summary>
    /// 다음 프레임 업데이트에 오브젝트가 떨어질 경우 True를 반환하고 그렇지 않으면 false를 반환
    /// </summary>
    /// <returns></returns>
    public bool isFallToRight(Vector3 curPos)
    {

        if (!ground)
        {
            // ground가 아직 세팅되지않았거나 ground 위치에 놓여있지않으면 발생
            Debug.Log("Ground is not set");
            return false;
        }

        RaycastHit2D rHit = Physics2D.Raycast(GetPeekPos(curPos, absPeekX), Vector2.down, peekDepth, groundLayer);



        if (rHit.transform != ground.transform) // PeekPos에서 아래로 쳐다봤을때 현재 Raycast로 사출한 결과가 저장되어있던 Ground 정보랑 다르면 그건 다음 프레임 업데이트에서 Ground에서 벗어날 수 있음을 의미하므로 true를 반환
        {

            return true;
        }
        else
        {
            return false;
        }


    }

    /// <summary>
    /// Scale이 반전이되면 좌표축도 반전이 됨
    /// </summary>
    /// <param name="curPos"></param>
    /// <returns></returns>
    public bool isFallToLeft(Vector3 curPos)
    {

        if (!ground)
        {
            // ground가 아직 세팅되지않았거나 ground 위치에 놓여있지않으면 발생
            Debug.Log("Ground is not set");
            return false;
        }



        RaycastHit2D lHit = Physics2D.Raycast(GetPeekPos(curPos, -absPeekX), Vector2.down, peekDepth, groundLayer);

        if (lHit.transform != ground.transform) // PeekPos에서 아래로 쳐다봤을때 현재 Raycast로 사출한 결과가 저장되어있던 Ground 정보랑 다르면 그건 다음 프레임 업데이트에서 Ground에서 벗어날 수 있음을 의미하므로 true를 반환
        {

            return true;
        }
        else
        {

            return false;
        }


    }

    /// <summary>
    /// 다음 프레임 업데이트에 오브젝트가 떨어질 경우 True를 반환하고 그렇지 않으면 false를 반환
    /// </summary>
    /// <returns></returns>
    public bool isFallToRight(Vector3 curPos, float peek)
    {

        if (!ground)
        {
            // ground가 아직 세팅되지않았거나 ground 위치에 놓여있지않으면 발생
            Debug.Log("Ground is not set");
            return false;
        }

        peek = Mathf.Abs(peek);
        RaycastHit2D rHit = Physics2D.Raycast(GetPeekPos(curPos, peek), Vector2.down, peekDepth, groundLayer);



        if (rHit.transform != ground.transform) // PeekPos에서 아래로 쳐다봤을때 현재 Raycast로 사출한 결과가 저장되어있던 Ground 정보랑 다르면 그건 다음 프레임 업데이트에서 Ground에서 벗어날 수 있음을 의미하므로 true를 반환
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    /// <summary>
    /// Scale이 반전이되면 좌표축도 반전이 됨
    /// </summary>
    /// <param name="curPos"></param>
    /// <returns></returns>
    public bool isFallToLeft(Vector3 curPos, float peek)
    {

        if (!ground)
        {
            // ground가 아직 세팅되지않았거나 ground 위치에 놓여있지않으면 발생
            Debug.Log("Ground is not set");
            return false;
        }


        peek = Mathf.Abs(peek);
        RaycastHit2D lHit = Physics2D.Raycast(GetPeekPos(curPos, -peek), Vector2.down, peekDepth, groundLayer);

        if (lHit.transform != ground.transform) // PeekPos에서 아래로 쳐다봤을때 현재 Raycast로 사출한 결과가 저장되어있던 Ground 정보랑 다르면 그건 다음 프레임 업데이트에서 Ground에서 벗어날 수 있음을 의미하므로 true를 반환
        {

            leftFallPos = GetPeekPos(curPos, -absPeekX);
            return true;
        }
        else
        {
            return false;
        }


    }




    /// <summary>
    /// curPos위치에서 현재 Ground 정보를 세팅함
    /// </summary>
    /// <param name="vPeekDiff"></param>
    /// <param name="curPos"></param>
    /// <param name="peekDepth"></param>
    /// <returns></returns>
    public bool Init(float xPeek, Vector3 curPos)
    {
        this.xPeek = Mathf.Abs(xPeek);  // 현재 위치와 검출되는 지면까지의 거리의 절댓값을 의미       

        RaycastHit2D ground = Physics2D.Raycast(curPos, Vector2.down, peekDepth, groundLayer);

        if (ground)
        {
            this.ground = ground;
            return true;
        }
        else
        {
            Debug.Log("Init Failed, Is prefab on the ground?");
            return false;
        }

    }

    public bool Init(Vector3 curPos)
    {

        RaycastHit2D ground = Physics2D.Raycast(curPos, Vector2.down, peekDepth, groundLayer);

        if (ground)
        {
            this.ground = ground;
            return true;
        }
        else
        {
            Debug.Log("Init Failed, Is prefab on the ground?");
            return false;
        }

    }





    public void VisualizePeek(Vector3 curPos)
    {

        Debug.DrawRay(GetPeekPos(curPos, -absPeekX), Vector3.down * peekDepth, Color.black);
        Debug.DrawRay(GetPeekPos(curPos, absPeekX), Vector3.down * peekDepth, Color.black);

    }





}
