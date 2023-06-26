using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightPlayer : MonoBehaviour
{
    public enum RunDirection
    {
        STOP = 0, RIGHT = 1, LEFT = -1
    }


    KnightStateMachine stateMachine;

    [SerializeField]
    KnightMoveData Data_Move;

    [SerializeField]
    KnightStatData Data_Stat;

    [SerializeField]
    Animator animKnight;

    #region Debug
    [Header("DEBUG Visual Option")]
    public bool showGroundDetectCollider;

    #endregion

    #region Ground Check
    [Header("Ground Check Paramter")]
    public Transform groundCheckPos;

    public Vector3 GroundCheckPos { get { return groundCheckPos.position; } }

    [SerializeField]
    float groundCheckRadius;

    [SerializeField]
    LayerMask maskGround;

    [SerializeField]
    Rigidbody2D rb;

    #endregion

    #region Attack parameter
    [Header("Attack Parameter")]
    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    Transform attackBoxPos;

    public Vector3 AttackBoxPos { get { return attackBoxPos.position; } }

    [SerializeField]
    Vector2 attackBoxSize;

    #endregion

    [Header("Option")]
    public bool addFullGravityEffect;

    [Header("Note Hit Parameter")]
    [SerializeField]
    Transform HitBarPoint;

    public Vector3 HitBarPosition { get { return HitBarPoint.position; } }



    float lastOnGroundTime;
    float attackCoolTime;
    public readonly string SFXCategory = "Knight"; 
    KnightPlayerStat stat;

    private void OnDrawGizmos()
    {
        if (showGroundDetectCollider)
        {
            Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
            Gizmos.DrawWireCube(attackBoxPos.position, attackBoxSize);
        }
            
        
    }




    // Start is called before the first frame update
    void Awake()
    {
        stateMachine = new KnightStateMachine();
        stat = new KnightPlayerStat(Data_Stat);
        stateMachine.Start();
        ResetAttackCoolTime();

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update(this);

        #region Jump
        if (addFullGravityEffect)
        {
            ApplyFullGravityEffect();
        }
        #endregion


        CountCoyoteTime();
        UpdateAnimParameter();
        //���⼭ Coyote Time °�ָ� ��
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate(this);

        #region Run
        //if (isPressedLeftRunKey())
        //{
        //    Run(RunDirection.LEFT);
        //    TurnLeft();
        //}
        //else if (isPressedRightRunKey())
        //{
        //   Run(RunDirection.RIGHT);
        //   TurnRight();
        //}
        //else
        //{
        //    Run(RunDirection.STOP);
        //}
        #endregion



    }

    public void ResetAttackCoolTime()
    {
        attackCoolTime = Data_Stat.AttackCoolTime;

    }

    #region # Logic - Jump


    /// <summary>
    /// JumpCut�� ȣ���� �Լ�
    /// </summary>
    public void OnJumpUp()
    {
        
        if (rb.velocity.y > 0 && Data_Move.isJumping)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - Data_Move.JumpCutMultiplier), ForceMode2D.Impulse);

        }
    }

    /// <summary>
    /// Jump�� ȣ���� �Լ�
    /// </summary>
    public void Jump()
    {
        // PressedDown�� �ѹ� ������ True������ ���� ���¸� �����ϰ� ������ False�� ���
        // Pressed�� ���� �������� �����Ǵ� �������� True�� ��ȯ
        // FixedUpdate�� ������ �ֱ⸦ ������ �������� ������Ʈ�� �����ϴµ�,
        // ���� �Է°����� FixedUpdate���� ����ϸ� �ֱⰡ Update�� �ٸ��� ��⶧����
        // �Է°����� �ֱⰡ �� PressedDown���� ��Ȯ�� �Է°����� ����
        // ���� Pressed�� ����ϰ� ��ƾ ó���� �ѹ��� �����ϵ��� ó���ؾ��Ѵ�.
        if (Data_Move.isJumping)
        {
            Debug.Log("Knight is jumping another input are denied");
            return;
        }

        float force = Data_Move.JumpForce;


        // �߷¶����� ������ ���������� ���� �ӵ����� ���񰪸�ŭ ���� �������� ������
        if (rb.velocity.y < 0)
        {
            force -= rb.velocity.y;
        }

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        //Data.isJumping = true;


    }

    /// <summary>
    /// ���� ĳ���Ͱ� ����� �´�Ҵ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool isOnGround()
    {
        // ���̶� ������ �ϳ��ϱ� ���� �ָ� ���� �����ӿ� �ٷ� ����� ������ ���̶�� ����������
        // ������ �װ� �ƴ� ���� ����
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, maskGround);
    }

    void CountCoyoteTime()
    {
        if (isOnGround())
        {
            lastOnGroundTime = Data_Move.CoyoteTime;
        }
        else
        {
            lastOnGroundTime -= Time.deltaTime;
        }
        

    }


    public void OnEnterJump()
    {
        Data_Move.isJumping = true;
    }

    public void OnExitJump()
    {
        Data_Move.isJumping = false;
        Data_Move.isFalling = true;
    }

    /// <summary>
    /// Full Gravity ���� ȣ���� �Լ�
    /// </summary>
    public void ApplyFullGravityEffect()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = Data_Move.GravityScale * Data_Move.FallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = Data_Move.GravityScale;
        }



    }


    #endregion

    #region # Logic - Run
    /// <summary>
    /// ���������� �����ϴ� �Լ�
    /// </summary>
    public void TurnRight()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
    }

    /// <summary>
    /// �������� ���� �ϴ� �Լ�
    /// </summary>
    public void TurnLeft()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
    }


    /// <summary>
    /// �޸��� ȣ���� �Լ�
    /// </summary>
    public void Run(RunDirection direction)
    {


        float dir = (float)direction;

        if(Mathf.Abs(dir) > 1)
        {
            dir = 0;
        }

        

        #region AddForce
        float targetSpeed = dir * Data_Move.MaxSpeed; //������ �̵� => maxSpeed, ���� �̵� = -maxSpeed, ���� = 0

        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, Data_Move.LerpAmount); // ���� speed ���� targetSpeed���� ���̿��� lerpAmount ������ŭ�� ���� ��ȯ, ������ ũ�� Ŭ���� �����Ӵ� �� ���� �̵��Ѵ�.



        float accelRate;

        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data_Move.RunAccelAmount : Data_Move.RunDeccelAmount; // �̵��� ��� runAccelAmount��ŭ rate�� ����, ������ ��� runDeccelAmount��ŭ rate ����

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.velocity.x; //(�����ΰ���) - ��ǥ �ӵ����� ���� ���� �������� ���� �پ��� ��ǥ �ӵ��� ������������ ���� �ο����� ����
        float movement;

        movement = speedDif * accelRate;

        

        // ������ �������� �ϰ� �ʹٸ� runAccelAmount ���� ũ�� ���ϱ�, ������ ������ �ϰ� �ʹٸ� runDeccelAmount���� ũ�� ���ϱ�

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);


        #endregion





        #region Friction
        
        if (dir == 0 && isOnGround())
        {
            
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(Data_Move.FrictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            //���� �������� ������� �ݴ��� ���� �ۿ��Ѵ�.
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        

        #endregion


    }

    public void RunFixedUpdate()
    {
        if (isPressedLeftRunKey())
        {
            TurnLeft();
            Run(RunDirection.LEFT);
        }
        else if (isPressedRightRunKey())
        {
            TurnRight();
            Run(RunDirection.RIGHT);
        }
        else
        {
            Run(RunDirection.STOP);
        }





    }


    public bool isJumping()
    {
        return (rb.velocity.y > 0) && Data_Move.isJumping;
    }

    public bool isFalling()
    {
        return (rb.velocity.y < 0) && Data_Move.isFalling;
    }

    public bool isRunning()
    {
        return (rb.velocity.x > 0) && Data_Move.isJumping;
    }

    


    #endregion

    #region # Logic - IDLE

    /// <summary>
    /// �⺻ �����϶� ȣ���� �Լ�
    /// </summary>
    public void IDLE()
    {

        
    }

    #endregion

    #region # Logic - Attack

    public float Attack1Damage
    {
        get { return Data_Stat.ATK * 1.0f; }
    }

    public void Attack(Enemy enemy, float damage)
    {
        Debug.Log("Player Gave " + damage + "dmg to" + enemy.name);
        Vector2 force = Data_Stat.KnockBackPower * (Mathf.Sign(enemy.transform.position.x - transform.position.x)) * Vector2.right;
        Debug.Log("Force:" + force);
        enemy.GiveDamage(damage, force);
        

    }

    public Enemy[] DetectEnemyInAttackRange()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackBoxPos.position, attackBoxSize, 0, enemyLayer);
        if(enemies.Length == 0)
        {
            return null;
        }
        else
        {
            Enemy[] enemy = new Enemy[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemy[i] =  enemies[i].GetComponent<Enemy>();


            }
            return enemy;
        }

    }


    #endregion


    public delegate void OnDamageEvent();

    #region # Logic - Player Stats

    // ��ø Ŭ������ private�϶� ��ø Ŭ������ ����� KnightPlayer Ŭ���������� KnightPlayerStat Ŭ���� �ν��Ͻ��� ������ �����ϳ�, �ܺο����� ������ �Ұ����ϴ�.
    // ��, KnightPlayer�� KnightPlayerStat�� �ʵ�, �޼ҵ忡 �����ϴ� ���� �����ϹǷ� HP, ATK, DEF �� ���ȿ� �ΰ��� �����͵��� �ܺο��� �ʿ�� �Ҷ� �б⸸ �����ϵ���
    // KnightPlayer���� �޼ҵ带 �ۼ��Ѵ�.
    class KnightPlayerStat
    {
        float maxHp;
        public float MaxHp { get { return maxHp; } }

        float hp;
        public float HP { 
            get { return hp; }
            set 
            {  
                
                if(hp > value) // ���� HP(hp)�� ���ο� HP(value)���� ���ٸ� �������� �Ծ��� ���� �ǹ���
                {
                    OnDamaged();
                }

                if(hp < value)
                {

                }

                hp = value;


            } 
        }

        float atk;
        public float ATK { get { return atk; } set { atk = value; } }

        float def;
        public float DEF { get { return def; } set { def = value; } }



        public KnightPlayerStat()
        {

        }

        public KnightPlayerStat(KnightStatData data)
        {
            Init(data);

        }

        public void Init(KnightStatData data)
        {
            hp = data.MAXHP;
            atk = data.ATK;
            maxHp = data.MAXHP;
            

        }


        #region Event Listener
        
        public event OnDamageEvent onDamage;
        public void OnDamaged()
        {
            var handler = onDamage;
            Debug.Log("OnDamage");
            if (handler != null)
            {
                Debug.Log("Handler is not null");
                handler();
            }

        }
        #endregion



    }


    public float HP { get { return stat.HP; } }
    public float ATK { get { return stat.ATK; } }
    public float def { get { return stat.DEF; } }
    public float MaxHP { get { return stat.MaxHp; } }

    public event OnDamageEvent onDamage { add { stat.onDamage += value; } remove { stat.onDamage -= value; } }


    public bool IsDead() { return (stat.HP <= 0); }

    public void ReceiveDamage(float damage)
    {
        stat.HP -= damage;
        if(IsDead())
        {
            
            Death();
        }
        else
        {
            SetTriggerHurtAnim();
            Debug.Log("PLAYER: PLAYER damaged " + damage + " Left Player life is " + stat.HP);

        }
       



    }

    public void Death()
    {
        Debug.Log("Player Death");
        SetTriggerDeathWithNoBlood();
        this.enabled = false;
        
    }
    



    #endregion

    #region Animation

    void UpdateAnimParameter()
    {
        animKnight.SetBool("Grounded", isOnGround());
        animKnight.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animKnight.SetFloat("AirSpeedY", rb.velocity.y);



    }

    public void SetTriggerJumpAnim()
    {
        animKnight.SetTrigger("Jump");
        
    }

    public void SetTriggerAttackAnim()
    {
        animKnight.SetTrigger("Attack1");
        
    }
    
    public void SetTriggerHurtAnim()
    {
        animKnight.SetTrigger("Hurt");
    }

    public void SetTriggerDeathWithNoBlood()
    {
        animKnight.SetTrigger("Death");
        animKnight.SetBool("noBlood", true);
    }
    

   
    #endregion

    #region Input

    public bool isPressedLeftRunKey()
    {
        return InputManager.manager.isPressedLMove();
    }

    public bool isPressedRightRunKey()
    {
        return InputManager.manager.isPressedRMove();
    }

    public bool isPressedDownJumpKey()
    {
        return InputManager.manager.isPressedDownJump();
    }

    public bool isPressedUpJumpKey()
    {
        return InputManager.manager.isPressedUpJump();
    }

    public bool isPressedAttackDownKey()
    {
        return InputManager.manager.isPressedDownAttack();
    }



    #endregion

    #region Can Method

  
    public bool CanRun()
    {
        return true;
    }
    

    /// <summary>
    /// ���� ������ �������� ���θ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        // �ڽ��� �������� �����ϰ� ���� �������� �ƴ϶�� True�� ��ȯ
        // �������� ���������ʰ� �������� �ƴѵ� lasOngroundTime > 0 �̸� True ��ȯ
       
        return !Data_Move.isJumping && (isOnGround() || lastOnGroundTime > 0);

    }

    #endregion

    #region Cooldown Timer

    /// <summary>
    /// ���� �� Ÿ���� ��
    /// </summary>
    /// <returns></returns>
    public bool isAttacking()
    {
        attackCoolTime -= Time.deltaTime;
        
        if(attackCoolTime > 0)
        {
            return true;
        }
        else
        {
            attackCoolTime = Data_Stat.AttackCoolTime;
            return false;
        }

     
    }


    #endregion

    



}
