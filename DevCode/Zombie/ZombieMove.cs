using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ �������� ��ƾ�� �����ϴ� Ŭ����
/// 1. IDLE ���¿����� ���� �ӵ��� �ȴ´�.
/// 2. Player�� �߰��ϸ� ���� �ӵ��� �ȴ´�.
/// </summary>
/// 
[System.Serializable]
public class ZombieMove
{


    [SerializeField]
    PlayerWatcher pWatcher;

    [SerializeField]
    FallWatcher fWatcher;

    Transform zombie;

    [SerializeField]
    Animator anim;

    MoveSpeedManager manager;


    public float animSpeed_onPlayerFound;


    /// <summary>
    /// ���� ���� �ݴ� �������� �÷��̾ ������ �� �ִ� ����
    /// </summary>
    [Range(1f, 2f)]
    public float backwardSight;
    

    public void Init(Transform zombie)
    {
        
        fWatcher.Init(zombie.position);
        this.zombie = zombie;
        manager = new MoveSpeedManager();

       
       

        if(pWatcher.PlayerRange < fWatcher.absPeekX)
        {
            Debug.LogError("PlayerRange should be larger than Peek");

        }
        else if(pWatcher.FindRange < fWatcher.absPeekX)
        {
            Debug.LogError("FindRange should be larger than Peek");
        }
        else if(pWatcher.FindRange < pWatcher.PlayerRange)
        {
            Debug.LogError("FindRange should be larger than PlayerRange");
        }
        

    }

    // ���������� �����ϴٰ� ���������� ����?
    /// <summary>
    /// IDLE ���¿��� �ȴ´�. �̶� ���鿡�� �������� �ȵǹǷ� AvoidFall�� �켱�̴�.
    /// Player�� ã�´�. �̶��� ���鿡�� �������� �ȵǹǷ� AvoidFall�� �켱�̴�.
    /// 
    /// ���� AvoidFall�� Player�� ���ÿ� �̷������ ������ ������ ª�� �ֱ�� State�� �Դٰ����ϸ� �� ��ҿ� ������ �¿� ������ �Ͼ�� �̻��� ��ó�� ����
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public void Move()
    {
        Vector3 curPos = zombie.position;

        
        if(pWatcher.FindPlayerNearMe(curPos))
        {
            HeadAtPlayer(curPos);
            manager.Reset(anim);
            manager.Stop(anim);
                
        }
        else if (AvoidFall(curPos)) // ���� �̵��� ���鿡 ���������� ���� Ȯ��
        {
            Debug.Log("AvoidFall");
            manager.Reset(anim);

        }
        else if (HeadAtPlayer(curPos)) // �������� ������ Player�� ã�´�.
        {
            Debug.Log("FindPlayer");
            manager.ChangeAnimSpeed(anim, animSpeed_onPlayerFound);

        }
        else // ���鿡�� �� �������� 
        {
            manager.Reset(anim);

        }

        
         fWatcher.VisualizePeek(curPos);
        
        
    }


    /// <summary>
    /// �����̿� ������ �ʵ��� �ϴ� �޼ҵ�
    /// </summary>
    /// <param name="curPos"></param>
    /// <returns></returns>
    public bool AvoidFall(Vector3 curPos)
    {

        if (fWatcher.isFallToRight(curPos))
        {
            Vector3 scale = zombie.localScale;
            zombie.localScale = new Vector3(-1.0f * Mathf.Abs(scale.x), scale.y, scale.z); // flip
            Debug.Log("IDLE: Will Fall to right");
            return true;
        }
        else if (fWatcher.isFallToLeft(curPos))
        {
            Vector3 scale = zombie.localScale;
            zombie.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z); // flip
            Debug.Log("IDLE: Will Fall to left");
            return true;

        }

        return false;

    }


    



    /// <summary>
    /// Player�� ã�� Player�� �ִ� ���� �ٶ󺸵��� �ϴ� �޼ҵ�
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public bool HeadAtPlayer(Vector3 curPos)
    {
     

        Vector3 scale = zombie.localScale;
        float Ldiffx = curPos.x - fWatcher.LeftFallPos.x;
        float Rdiffx = fWatcher.RightFallPos.x - curPos.x;

        if (scale.x > 0.0f)
        {
           
            if (pWatcher.FindPlayerOnRightDir(curPos, Rdiffx))
            {
                zombie.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z); // ���� �������� �÷��̾ �����ʿ� ������ x�� ���� ������
                return true;
            }
            else if (pWatcher.FindPlayerOnLeftDir(curPos, backwardSight))
            {
                zombie.localScale = new Vector3(-1.0f * Mathf.Abs(scale.x), scale.y, scale.z);  // ���� �������� �÷��̾ �����ʿ� ������ x�� ���� ������                                                                                //Debug.Log("Player is left of me");
                return true;
            }
        }
        else
        {
            if (pWatcher.FindPlayerOnLeftDir(curPos, Ldiffx))
            {
                zombie.localScale = new Vector3(-1.0f * Mathf.Abs(scale.x), scale.y, scale.z);  // ���� �������� �÷��̾ �����ʿ� ������ x�� ���� ������                                                                                //Debug.Log("Player is left of me");
                return true;
            }
            else if (pWatcher.FindPlayerOnRightDir(curPos, backwardSight))
            {
                zombie.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);  // ���� �������� �÷��̾ �����ʿ� ������ x�� ���� ������                                                                                //Debug.Log("Player is left of me");
                return true;
            }

        }


        return false;
        //Debug.Log("There is no player");



    }

  


}
