using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// Force를 작용했을때 어떤 움직임을 보이는지 확인하기 위해서 만든 클래스
/// </summary>
public class TestForceMovement : MonoBehaviour
{
    /* FixedUpdate는 고정된 주기를 가지고 호출되므로 물리적인 연산 수행에 사용된다.
     * 물리적 연산을 계산하는데 고정된 주기가 필요한 이유는 같은 입력에 따른 
     * 같은 출력을 내야하기 때문이다.
     * 예를 들어서, 입력키를 눌렀을때 일정한 힘을 가한다고 하면 Update 문에서는
     * frame per second 만큼의 시간동안 힘을 가한다. 하지만 CPU 상태와 작업 상태에 따라서
     * FPS가 달라지기에 일정한 시간동안 힘을 가한다고 볼 수 없다.
     * 
     * 반면에 FixedUpdate의 경우 일정한 주기를 가지고 힘을 가할 수 있기때문에 예측할 수 있는
     * 물리적 연산 결과가 화면에 나타날 수 있다.
     * 
     * 실제로 Update 문에서 힘을 주면 어떨때는 속도가 빠르고 어떨때는 속도가 더 빠른것을 볼 
     * 수 있는데 이는 프레임당 힘이 가해지는 시간이 다르기때문에 그렇다.
     * 
     * 
     * 
     */

   
    public enum Mode
    {
        FORCE, POS, COMPLICATED_FORCE, COMPLICATED_FORCE_V2, IMPULSE
    }

    public bool useFriction;
    public bool useFullGravity;

    [SerializeField]
    Mode mode;
    [SerializeField]
    Rigidbody2D rb;

    #region ForceMode
    [Header("ForceMode")]

    [SerializeField]
    float forceAmount;

    
    #endregion

    #region PositionMode

    [Header("PositionMode")]
    [SerializeField]
    float posModeVel;
    #endregion


    #region Complicated Mode
    [Header("Complicated Mode")]
    [SerializeField]
    float maxSpeed;

    [Range(0f, 1f)]
    [SerializeField]
    float lerpAmount;

    [SerializeField]
    float runAccelAmount;

    [SerializeField]
    float runDeccelAmount;

    [SerializeField]
    float frictionAmount;

    [SerializeField]
    float velPower;
    #endregion

    #region IMPULSE
    [Header("Impulse")]
    [SerializeField]
    float impulseAmount;


    #endregion

    #region JUMP
    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravityScale;

    [SerializeField]
    float fallGravityMultiplier;

    [SerializeField, Range(0f, 1f)]
    float jumpCutMultiplier;
    
    #endregion

    #region Ground Check
    [Header("Ground Check")]
    [SerializeField]
    Transform groundCheckPos;

    [SerializeField]
    Vector3 groundCheckSize;

    [SerializeField]
    LayerMask maskGround;

    #endregion

    #region Timer
   

    [SerializeField]
    float coyoteTime;

    #endregion


    // float LastOnGroundTime = lastonGroundTime;

    #region State Variable
    public bool enableShowGroundCheckCollider;
    float lastOnGroundTime;
    bool isJumping;
    #endregion

    private void OnDrawGizmos()
    {
        if(enableShowGroundCheckCollider)
            Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }


    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
    }

    private void Update()
    {
        
        if (CanJump())
        {
            Jump();
        }

        if (useFullGravity)
        {
            UseFullGravity();
        }

        if (Input.GetKeyUp(InputManager.manager.KJump))
        {
            OnJumpUp();
        }

        



    }

    // Update is called once per frame
    void FixedUpdate()
    {


        switch (mode)
        {
            case Mode.FORCE:
                RunWithForce();
                break;
            case Mode.POS:
                RunWithPosMode();
                break;
            case Mode.COMPLICATED_FORCE:
                RunWithComplicatedForceMode();
                break;
            case Mode.COMPLICATED_FORCE_V2:
                RunWithComplicatedForceMode();
                break;
            case Mode.IMPULSE:
                RunWithImpulse();
                break;
        }





    }


    void RunWithPosMode()
    {
        if (InputManager.manager.isPressedLMove())
        {
            transform.position += posModeVel * Vector3.left * Time.deltaTime;
        }

        if (InputManager.manager.isPressedRMove())
        {
            transform.position += posModeVel * Vector3.right * Time.deltaTime;
        }
    }

    void RunWithForce()
    {

        if (InputManager.manager.isPressedLMove())
        {
            rb.AddForce(Vector2.left * forceAmount);
        }

        if (InputManager.manager.isPressedRMove())
        {
            rb.AddForce(Vector2.right * forceAmount);
        }

    }

    void RunWithImpulse()
    {

        if (InputManager.manager.isPressedLMove())
        {
            rb.AddForce(Vector2.left * impulseAmount, ForceMode2D.Impulse);
        }

        if (InputManager.manager.isPressedRMove())
        {
            rb.AddForce(Vector2.right * impulseAmount, ForceMode2D.Impulse);
        }

    }

    void RunWithComplicatedForceMode()
    {
        float dir;

        // 방향을 결정
        if (InputManager.manager.isPressedLMove())
        {
            dir = -1; 
        }
        else if (InputManager.manager.isPressedRMove())
        {
            dir = 1;
        }
        else
        {
            dir = 0;
        }

        #region AddForce
        float targetSpeed = dir * maxSpeed; //오른쪽 이동 => maxSpeed, 왼쪽 이동 = -maxSpeed, 정지 = 0

        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount); // 현재 speed 에서 targetSpeed까지 사이에서 lerpAmount 비율만큼의 값을 반환, 비율이 크면 클수록 프레임당 더 많이 이동한다.

       

        float accelRate;

        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount; // 이동할 경우 runAccelAmount만큼 rate을 지정, 정지할 경우 runDeccelAmount만큼 rate 지정

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.velocity.x; //(오차인거임) - 목표 속도까지 따라갈 수록 가해지는 힘은 줄어들며 목표 속도에 도달했을때는 힘을 부여하지 않음
        float movement;

        if(mode == Mode.COMPLICATED_FORCE)
        {
            movement = speedDif * accelRate;
        }
        else if(mode == Mode.COMPLICATED_FORCE_V2)
        {
            movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        }
        else
        {
            movement = 0;
        }
         
        //float movement = speedDif * accelRate; // 변동이 빠르도록 하고 싶다면 runAccelAmount 값을 크게 정하기, 감속을 빠르게 하고 싶다면 runDeccelAmount값을 크게 정하기

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);


        #endregion
        


        #region Friction
        if (useFriction)
        {
           
            if (dir == 0)
            {
                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

                amount *= Mathf.Sign(rb.velocity.x);

                //현재 진행중인 방향과는 반대의 힘을 작용한다.
                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }

        }

        #endregion


    }

    /// <summary>
    /// JumpCut
    /// </summary>
    void OnJumpUp()
    {
        if(rb.velocity.y > 0 && isJumping)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);

        }

        


    }



    void Jump()
    {
        
        // PressedDown은 한번 누르면 True이지만 누른 상태를 유지하고 있으면 False를 출력
        // Pressed는 누른 시점부터 유지되는 시점까지 True를 반환
        // FixedUpdate는 고정된 주기를 가지고 물리연산 업데이트를 수행하는데,
        // 만약 입력감지를 FixedUpdate에서 사용하면 주기가 Update와 다르게 길기때문에
        // 입력감지의 주기가 길어서 PressedDown같은 정확한 입력감지가 힘듦
        // 따라서 Pressed를 사용하고 루틴 처리를 한번만 수행하도록 처리해야한다.

        if (InputManager.manager.isPressedDownJump())
        {
            Debug.Log("JUMP");
            float force = jumpForce;
            
            

            // 중력때문에 점프가 되지않을때 음의 속도값의 절댓값만큼 위의 방향으로 더해줌
            if (rb.velocity.y < 0) 
            {
                force -= rb.velocity.y;
            }

            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            isJumping = true;
           

        }

      
      
        





    }

    public bool isOnGround()
    {
       
        // groundCheckPos에서 groundCheckSize의 x만큼 가로 방향으로, y만큼 세로 방향으로 충돌 검출
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, maskGround);

    }

    public bool CanJump()
    {
        if (isOnGround()) // 지면위에 존재할땐 무조건 점프가능
        {
            lastOnGroundTime = coyoteTime;
            isJumping = false;
            return true;
        }
        else
        {
            if (isJumping) // 지면위에 존재하지않고 현재 점프중이라면 점프 불가능
            {
                return false;

            }
            else // 지면위에 존재하지않고 현재 점프중이 아니라면 특정 시간내에만 점프를 허용한다.
            {
                lastOnGroundTime -= Time.deltaTime;

                if (lastOnGroundTime > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }






    }

    public void UseFullGravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }


    }

}
