using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

[RequireComponent(typeof(Rigidbody2D))]
public class Zombie : Enemy
{
    #region Field

    

    /// <summary>
    /// Player�� ã������ ������ �ӵ�
    /// </summary>
    public float onPlayerFoundSpeed;

    /// <summary>
    /// ������� ���������� ������ �ʵ��� �ϱ� ���� x ������ ����(margin) ����Ʈ
    /// </summary>
    [Header("Margin position")]
 
    public Transform rightFallPoint;

    public Transform leftFallPoint;

    [Header("etc")]

    [SerializeField]
    Rigidbody2D rb;


    /// <summary>
    /// ���� ���, ���� ���� �˹� ������ ũ��.
    /// </summary>
    public float frictionConstant;

    /// <summary>
    /// �˹��� ����Ǵ� �ð�, �˹��� ��� �ð��� ������ ���¸� �ʱ�ȭ(�ٽ� ������ �簳)
    /// </summary>
    float knockBackTimer;

    
    float soundIdleTimer;

    [Header("SFX")]
    [SerializeField]
    float walk_minSoundDelay;

    [SerializeField]
    float walk_maxSoundDelay;

    [SerializeField]
    float damaged_soundPitch;

    bool isStunning = false;
    ZombieStateMachine stateMachine;



    #endregion

    #region Mono

    private void Awake()
    {
        Init();
        stateMachine = new ZombieStateMachine();
        stateMachine.Start(this);

    }

    // Start is called before the first frame update
    void Start()
    {
  
        ResetAttackCoolTimer(); // �⺻�ʱ�ȭ
        ResetKnockBackTimer(); // �⺻ �ʱ�ȭ

        if (rb == null)
            rb = this.transform.GetComponent<Rigidbody2D>();

        

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
    /// �����̿� ������ �ʵ��� �ϴ� �޼ҵ�
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
    /// Player�� ã�� Player�� �ִ� ���� �ٶ󺸵��� �ϴ� �޼ҵ�
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

        // �����������ʰ� �ִ� �˹� �ð��� ������ ���������� ������ ����
        while (rb.velocity.x != 0 && knockBackTimer > 0)
        {
            // Force�� ���� �����ӿ� ���ļ� ���� ������ ����ǹǷ� FixedUpdate�� ���� �ֱ�� ��ġ��Ŵ
            knockBackTimer -= Time.fixedDeltaTime;
            ApplyFriction(rb, frictionConstant, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();

        }

        #endregion



        
        //Ȯ���ϰ� �ӵ��� 0���� �ٲ�(��� �̲������� ������ ������)
        rb.velocity = new Vector2(0, rb.velocity.y);

        // �ٽ� �ʱ�ȭ
        knockBackTimer = DATA.KnockBackTime;
        OnExitUsingRigidBody2D();
        isStunning = false;

    }

    

    public override IEnumerator Death()
    {
        SetAnimatorDefaultState(); // anim speed�� 0�϶��� �־ speed�� 1(default)�� ����
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

        #region Play SFX
        if (soundIdleTimer <= 0)
        {
            jsh.SoundManager.instance.PlaySFX("Zombie", "ZombieWalk");
            soundIdleTimer = Random.Range(walk_minSoundDelay, walk_maxSoundDelay);
        }
        else 
        { 
            soundIdleTimer -= Time.deltaTime; 
        
        }
        #endregion



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
        knockBackTimer = DATA.KnockBackTime; // �˹��� �ǰ� ���Ͻð����� �ٽ� ������ �Ǿ����� �˹� Ÿ���� �ʱ�ȭ�ǵ���
        STAT.HP -= damage;

        if (!isDead())
        {
            jsh.SoundManager.instance.PlaySFX("Zombie", "ZombieWalk", damaged_soundPitch);
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
