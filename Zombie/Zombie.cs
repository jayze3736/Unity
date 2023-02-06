using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

[RequireComponent(typeof(Rigidbody2D))]
public class Zombie : Enemy
{
    #region Field

    

    /// <summary>
    /// Player를 찾았을때 좀비의 속도
    /// </summary>
    public float onPlayerFoundSpeed;

    /// <summary>
    /// 좀비들이 낭떠러지에 빠지지 않도록 하기 위한 x 방향의 마진(margin) 포인트
    /// </summary>
    [Header("Margin position")]
 
    public Transform rightFallPoint;

    public Transform leftFallPoint;

    [Header("etc")]

    [SerializeField]
    Rigidbody2D rb;


    /// <summary>
    /// 마찰 계수, 높을 수록 넉백 저항이 크다.
    /// </summary>
    public float frictionConstant;

    /// <summary>
    /// 넉백이 진행되는 시간, 넉백이 허용 시간이 지나면 상태를 초기화(다시 움직임 재개)
    /// </summary>
    float knockBackTimer;


    bool isStunning = false;
    ZombieStateMachine stateMachine;
    


    #endregion

    #region Mono
    // Start is called before the first frame update
    void Start()
    {
  
        Init();

        ResetAttackCoolTimer(); // 기본초기화
        ResetKnockBackTimer(); // 기본 초기화

        if (rb == null)
            rb = this.transform.GetComponent<Rigidbody2D>();

        stateMachine = new ZombieStateMachine();
        stateMachine.Start();
        

    }



    // Update is called once per frame
    void Update()
    {
        stateMachine.Update(this);
      

        AvoidFall();

    }

  

    


    #endregion



    #region MoveMent
    /// <summary>
    /// 구덩이에 빠지지 않도록 하는 메소드
    /// </summary>
    /// <param name="curPos"></param>
    /// <returns></returns>
    public void AvoidFall()
    {

        if (IsFallToRight())
        {
            TurnLeft();
            transform.position = new Vector3(rightFallPoint.position.x, transform.position.y, transform.position.z);
           
        }
        else if (IsFallToLeft())
        {
            TurnRight();
            transform.position = new Vector3(leftFallPoint.position.x, transform.position.y, transform.position.z);
            

        }

        

    }

    public bool IsFallToRight()
    {
        return transform.position.x >= rightFallPoint.position.x;

    }

    public bool IsFallToLeft()
    {
        return transform.position.x <= leftFallPoint.position.x;
    }



    /// <summary>
    /// Player를 찾고 Player가 있는 쪽을 바라보도록 하는 메소드
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public void HeadAtPlayer()
    {
        Vector3 curPos = transform.position;
        float Ldiffx = curPos.x - leftFallPoint.position.x;
        float Rdiffx = rightFallPoint.position.x - curPos.x;

        if (FindPlayerOnLeftDir(Ldiffx))
        {
            if (IsTowardRight())
            {
                TurnLeft();
            }
            
        }
        else if (FindPlayerOnRightDir(Rdiffx))
        {
            if (IsTowardLeft())
            {
                TurnRight();
            }
            
        }

        DebugFindRangeInRay(Vector2.right, curPos, Rdiffx);
        DebugFindRangeInRay(Vector2.left, curPos, Ldiffx);

       

    }

    

    #endregion

    #region Enemy State Logic

    IEnumerator KnockBack(Vector2 power)
    {
        isStunning = true;
        ApplyKnockBack(rb, power);


        #region Apply Friction until knockbackTime reaches 0

        // 정지해있지않고 최대 넉백 시간이 지나지 않을때까지 마찰력 가함
        while (rb.velocity.x != 0 && knockBackTimer > 0)
        {
            // Force는 여러 프레임에 걸쳐서 물리 연산이 적용되므로 FixedUpdate의 연산 주기와 일치시킴
            knockBackTimer -= Time.fixedDeltaTime;
            ApplyFriction(rb, frictionConstant, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();

        }

        #endregion



        
        //확실하게 속도를 0으로 바꿈(계속 미끄러지는 현상이 없도록)
        rb.velocity = new Vector2(0, rb.velocity.y);

        // 다시 초기화
        knockBackTimer = DATA.KnockBackTime;
        OnExitUsingRigidBody2D();
        isStunning = false;

    }

    

    public override IEnumerator Death()
    {
        SetAnimatorDefaultState(); // anim speed가 0일때도 있어서 speed를 1(default)로 변경
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(DATA.DeathAnimPlayTime);
        Destroy(this.gameObject);

    }

    void RunFaster() { anim.speed = onPlayerFoundSpeed; }
    void RunNormal() { anim.speed = 1; }
    void SetAnimatorDefaultState() { anim.speed = 1; }
    void Stop() { anim.speed = 0; }
    
   
    
    public override void Attack()
    {
        HeadAtPlayer();
        Stop();

        
        if (CanAttack())
        {
            var player = GetPlayerNearOnXaxis();
            player.ReceiveDamage(STAT.DMG);
        }

        if (STAT.ACT > 0)
        {
            STAT.ACT -= Time.deltaTime;
        }
        else
        {
            ResetAttackCoolTimer();
        }

        

    }



    public override void Idle()
    {
        float Ldiffx = transform.position.x - leftFallPoint.position.x;
        float Rdiffx = rightFallPoint.position.x - transform.position.x;
        // Player Found -> Run Faster and Avoid Fall
        if (FindPlayerOnRightDir(Rdiffx))
        {   
            RunFaster();
            HeadAtPlayer();
           
        }
        else if (FindPlayerOnLeftDir(Ldiffx))
        {
            RunFaster();
            HeadAtPlayer();

        }
        else // Player Not Found -> Normal Run and Avoid Fall
        {
            RunNormal();
        }
        


    }

  
    public override bool IsPlayerNear()
    {
        return (GetPlayerNearOnXaxis() != null);
    }

    
    public override bool IsStunning()
    {
        return isStunning;
    }

    public override void GiveDamage(float damage, Vector2 power)
    {
        knockBackTimer = DATA.KnockBackTime; // 넉백이 되고 스턴시간동안 다시 공격이 되었을때 넉백 타임이 초기화되도록
        STAT.HP -= damage;

        if (!isDead())
        {
            Debug.Log("Left Zombie life:" + STAT.HP);
            Debug.Log("damaged");

            StartCoroutine(KnockBack(power / DATA.Mass));

        }
        else
        {
            StartCoroutine(Death());
        }
        

    }

   





    #endregion

}
