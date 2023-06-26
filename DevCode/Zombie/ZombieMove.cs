using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 좀비의 움직임을 루틴을 정의하는 클래스
/// 1. IDLE 상태에서는 보통 속도로 걷는다.
/// 2. Player를 발견하면 빠른 속도로 걷는다.
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
    /// 보는 방향 반대 방향으로 플레이어를 감지할 수 있는 범위
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

    // 직렬적으로 수행하다가 병렬적으로 수행?
    /// <summary>
    /// IDLE 상태에서 걷는다. 이때 지면에서 떨어지면 안되므로 AvoidFall이 우선이다.
    /// Player를 찾는다. 이때도 지면에서 떨어지면 안되므로 AvoidFall이 우선이다.
    /// 
    /// 만약 AvoidFall과 Player가 동시에 이루어지고 있지는 않지만 짧은 주기로 State를 왔다갔다하면 한 장소에 여러번 좌우 반전이 일어나서 이상한 것처럼 보임
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
        else if (AvoidFall(curPos)) // 다음 이동이 지면에 떨어질지를 먼저 확인
        {
            Debug.Log("AvoidFall");
            manager.Reset(anim);

        }
        else if (HeadAtPlayer(curPos)) // 떨어지지 않으면 Player를 찾는다.
        {
            Debug.Log("FindPlayer");
            manager.ChangeAnimSpeed(anim, animSpeed_onPlayerFound);

        }
        else // 지면에도 못 떨어지고 
        {
            manager.Reset(anim);

        }

        
         fWatcher.VisualizePeek(curPos);
        
        
    }


    /// <summary>
    /// 구덩이에 빠지지 않도록 하는 메소드
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
    /// Player를 찾고 Player가 있는 쪽을 바라보도록 하는 메소드
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
                zombie.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z); // 몬스터 기준으로 플레이어가 오른쪽에 있으면 x가 양의 값으로
                return true;
            }
            else if (pWatcher.FindPlayerOnLeftDir(curPos, backwardSight))
            {
                zombie.localScale = new Vector3(-1.0f * Mathf.Abs(scale.x), scale.y, scale.z);  // 몬스터 기준으로 플레이어가 오른쪽에 있으면 x가 음의 값으로                                                                                //Debug.Log("Player is left of me");
                return true;
            }
        }
        else
        {
            if (pWatcher.FindPlayerOnLeftDir(curPos, Ldiffx))
            {
                zombie.localScale = new Vector3(-1.0f * Mathf.Abs(scale.x), scale.y, scale.z);  // 몬스터 기준으로 플레이어가 오른쪽에 있으면 x가 음의 값으로                                                                                //Debug.Log("Player is left of me");
                return true;
            }
            else if (pWatcher.FindPlayerOnRightDir(curPos, backwardSight))
            {
                zombie.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);  // 몬스터 기준으로 플레이어가 오른쪽에 있으면 x가 음의 값으로                                                                                //Debug.Log("Player is left of me");
                return true;
            }

        }


        return false;
        //Debug.Log("There is no player");



    }

  


}
