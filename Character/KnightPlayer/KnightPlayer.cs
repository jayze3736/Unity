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
        //여기서 Coyote Time 째주면 됨
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
    /// JumpCut시 호출할 함수
    /// </summary>
    public void OnJumpUp()
    {
        
        if (rb.velocity.y > 0 && Data_Move.isJumping)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - Data_Move.JumpCutMultiplier), ForceMode2D.Impulse);

        }
    }

    /// <summary>
    /// Jump시 호출할 함수
    /// </summary>
    public void Jump()
    {
        // PressedDown은 한번 누르면 True이지만 누른 상태를 유지하고 있으면 False를 출력
        // Pressed는 누른 시점부터 유지되는 시점까지 True를 반환
        // FixedUpdate는 고정된 주기를 가지고 물리연산 업데이트를 수행하는데,
        // 만약 입력감지를 FixedUpdate에서 사용하면 주기가 Update와 다르게 길기때문에
        // 입력감지의 주기가 길어서 PressedDown같은 정확한 입력감지가 힘듦
        // 따라서 Pressed를 사용하고 루틴 처리를 한번만 수행하도록 처리해야한다.
        if (Data_Move.isJumping)
        {
            Debug.Log("Knight is jumping another input are denied");
            return;
        }

        float force = Data_Move.JumpForce;


        // 중력때문에 점프가 되지않을때 음의 속도값의 절댓값만큼 위의 방향으로 더해줌
        if (rb.velocity.y < 0)
        {
            force -= rb.velocity.y;
        }

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        //Data.isJumping = true;


    }

    /// <summary>
    /// 현재 캐릭터가 지면과 맞닿았는지 확인하는 함수
    /// </summary>
    /// <returns></returns>
    public bool isOnGround()
    {
        // 원이라서 접점이 하나니까 힘을 주면 다음 프레임에 바로 지면과 떨어질 것이라고 예상했지만
        // 실제로 그게 아닐 수도 있음
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
    /// Full Gravity 사용시 호출할 함수
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
    /// 오른쪽으로 보게하는 함수
    /// </summary>
    public void TurnRight()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
    }

    /// <summary>
    /// 왼쪽으로 보게 하는 함수
    /// </summary>
    public void TurnLeft()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
    }


    /// <summary>
    /// 달릴때 호출할 함수
    /// </summary>
    public void Run(RunDirection direction)
    {


        float dir = (float)direction;

        if(Mathf.Abs(dir) > 1)
        {
            dir = 0;
        }

        

        #region AddForce
        float targetSpeed = dir * Data_Move.MaxSpeed; //오른쪽 이동 => maxSpeed, 왼쪽 이동 = -maxSpeed, 정지 = 0

        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, Data_Move.LerpAmount); // 현재 speed 에서 targetSpeed까지 사이에서 lerpAmount 비율만큼의 값을 반환, 비율이 크면 클수록 프레임당 더 많이 이동한다.



        float accelRate;

        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data_Move.RunAccelAmount : Data_Move.RunDeccelAmount; // 이동할 경우 runAccelAmount만큼 rate을 지정, 정지할 경우 runDeccelAmount만큼 rate 지정

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.velocity.x; //(오차인거임) - 목표 속도까지 따라갈 수록 가해지는 힘은 줄어들며 목표 속도에 도달했을때는 힘을 부여하지 않음
        float movement;

        movement = speedDif * accelRate;

        

        // 변동이 빠르도록 하고 싶다면 runAccelAmount 값을 크게 정하기, 감속을 빠르게 하고 싶다면 runDeccelAmount값을 크게 정하기

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);


        #endregion





        #region Friction
        
        if (dir == 0 && isOnGround())
        {
            
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(Data_Move.FrictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            //현재 진행중인 방향과는 반대의 힘을 작용한다.
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
    /// 기본 상태일때 호출할 함수
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

    public void DamageEnemy(Enemy enemy, float damage)
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

    // 중첩 클래스가 private일때 중첩 클래스가 선언된 KnightPlayer 클래스에서는 KnightPlayerStat 클래스 인스턴스에 접근이 가능하나, 외부에서는 접근이 불가능하다.
    // 즉, KnightPlayer만 KnightPlayerStat의 필드, 메소드에 접근하는 것이 가능하므로 HP, ATK, DEF 등 보안에 민감한 데이터들은 외부에서 필요로 할때 읽기만 가능하도록
    // KnightPlayer에서 메소드를 작성한다.
    class KnightPlayerStat
    {
        float maxHp;
        public float MaxHp { get { return maxHp; } }

        float hp;
        public float HP { 
            get { return hp; }
            set 
            {  
                
                if(hp > value) // 이전 HP(hp)가 새로운 HP(value)보다 높다면 데미지를 입었단 것을 의미함
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
    /// 현재 점프가 가능한지 여부를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        // 자신이 지면위에 존재하고 현재 점프중이 아니라면 True를 반환
        // 지면위에 존재하지않고 점프중이 아닌데 lasOngroundTime > 0 이면 True 반환
       
        return !Data_Move.isJumping && (isOnGround() || lastOnGroundTime > 0);

    }

    #endregion

    #region Cooldown Timer

    /// <summary>
    /// 공격 쿨 타임을 잼
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
