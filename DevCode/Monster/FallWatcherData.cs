using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWatcherData : ScriptableObject
{
    // <summary>
    /// Ground�� �����ϰ� ������ Layer ���� ���� ����ŷ ���Ѽ� Ground�� Ray ��ȯ ����� �򵵷� �Ѵ�.
    /// </summary>
    [SerializeField]
    LayerMask groundLayer;

    /// <summary>
    /// ���� ��ġ�� ����Ǵ� ��������� �Ÿ��� ������ �ǹ�       
    /// </summary>
    [SerializeField]
    float xPeek;

    /// <summary>
    /// Fall ������ ���� ��� �Ÿ���ŭ �� �������� ���� ����
    /// </summary>
    [SerializeField]
    float peekDepth;

    /// <summary>
    /// ���� �پ���־���ϴ� ground
    /// </summary>
    RaycastHit2D ground;


    /// <summary>
    /// ���� ��ǥ�� �������� ���������� �̵��� ������ ��ǥ
    /// </summary>
    [SerializeField]
    Vector3 rightFallPos;

    /// <summary>
    /// ���� ��ǥ�� �������� �������� �̵��� ������ ��ǥ
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
    /// ���� ������ ������Ʈ�� ������Ʈ�� ������ ��� True�� ��ȯ�ϰ� �׷��� ������ false�� ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool isFallToRight(Vector3 curPos)
    {

        if (!ground)
        {
            // ground�� ���� ���õ����ʾҰų� ground ��ġ�� �������������� �߻�
            Debug.Log("Ground is not set");
            return false;
        }

        RaycastHit2D rHit = Physics2D.Raycast(GetPeekPos(curPos, absPeekX), Vector2.down, peekDepth, groundLayer);



        if (rHit.transform != ground.transform) // PeekPos���� �Ʒ��� �Ĵٺ����� ���� Raycast�� ������ ����� ����Ǿ��ִ� Ground ������ �ٸ��� �װ� ���� ������ ������Ʈ���� Ground���� ��� �� ������ �ǹ��ϹǷ� true�� ��ȯ
        {

            return true;
        }
        else
        {
            return false;
        }


    }

    /// <summary>
    /// Scale�� �����̵Ǹ� ��ǥ�൵ ������ ��
    /// </summary>
    /// <param name="curPos"></param>
    /// <returns></returns>
    public bool isFallToLeft(Vector3 curPos)
    {

        if (!ground)
        {
            // ground�� ���� ���õ����ʾҰų� ground ��ġ�� �������������� �߻�
            Debug.Log("Ground is not set");
            return false;
        }



        RaycastHit2D lHit = Physics2D.Raycast(GetPeekPos(curPos, -absPeekX), Vector2.down, peekDepth, groundLayer);

        if (lHit.transform != ground.transform) // PeekPos���� �Ʒ��� �Ĵٺ����� ���� Raycast�� ������ ����� ����Ǿ��ִ� Ground ������ �ٸ��� �װ� ���� ������ ������Ʈ���� Ground���� ��� �� ������ �ǹ��ϹǷ� true�� ��ȯ
        {

            return true;
        }
        else
        {

            return false;
        }


    }

    /// <summary>
    /// ���� ������ ������Ʈ�� ������Ʈ�� ������ ��� True�� ��ȯ�ϰ� �׷��� ������ false�� ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool isFallToRight(Vector3 curPos, float peek)
    {

        if (!ground)
        {
            // ground�� ���� ���õ����ʾҰų� ground ��ġ�� �������������� �߻�
            Debug.Log("Ground is not set");
            return false;
        }

        peek = Mathf.Abs(peek);
        RaycastHit2D rHit = Physics2D.Raycast(GetPeekPos(curPos, peek), Vector2.down, peekDepth, groundLayer);



        if (rHit.transform != ground.transform) // PeekPos���� �Ʒ��� �Ĵٺ����� ���� Raycast�� ������ ����� ����Ǿ��ִ� Ground ������ �ٸ��� �װ� ���� ������ ������Ʈ���� Ground���� ��� �� ������ �ǹ��ϹǷ� true�� ��ȯ
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    /// <summary>
    /// Scale�� �����̵Ǹ� ��ǥ�൵ ������ ��
    /// </summary>
    /// <param name="curPos"></param>
    /// <returns></returns>
    public bool isFallToLeft(Vector3 curPos, float peek)
    {

        if (!ground)
        {
            // ground�� ���� ���õ����ʾҰų� ground ��ġ�� �������������� �߻�
            Debug.Log("Ground is not set");
            return false;
        }


        peek = Mathf.Abs(peek);
        RaycastHit2D lHit = Physics2D.Raycast(GetPeekPos(curPos, -peek), Vector2.down, peekDepth, groundLayer);

        if (lHit.transform != ground.transform) // PeekPos���� �Ʒ��� �Ĵٺ����� ���� Raycast�� ������ ����� ����Ǿ��ִ� Ground ������ �ٸ��� �װ� ���� ������ ������Ʈ���� Ground���� ��� �� ������ �ǹ��ϹǷ� true�� ��ȯ
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
    /// curPos��ġ���� ���� Ground ������ ������
    /// </summary>
    /// <param name="vPeekDiff"></param>
    /// <param name="curPos"></param>
    /// <param name="peekDepth"></param>
    /// <returns></returns>
    public bool Init(float xPeek, Vector3 curPos)
    {
        this.xPeek = Mathf.Abs(xPeek);  // ���� ��ġ�� ����Ǵ� ��������� �Ÿ��� ������ �ǹ�       

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
