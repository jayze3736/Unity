using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

/// <summary>
/// Force�� �ۿ������� � �������� ���̴��� Ȯ���ϱ� ���ؼ� ���� Ŭ����
/// </summary>
public class TestForceMovement : MonoBehaviour
{
    /* FixedUpdate�� ������ �ֱ⸦ ������ ȣ��ǹǷ� �������� ���� ���࿡ ���ȴ�.
     * ������ ������ ����ϴµ� ������ �ֱⰡ �ʿ��� ������ ���� �Է¿� ���� 
     * ���� ����� �����ϱ� �����̴�.
     * ���� ��, �Է�Ű�� �������� ������ ���� ���Ѵٰ� �ϸ� Update ��������
     * frame per second ��ŭ�� �ð����� ���� ���Ѵ�. ������ CPU ���¿� �۾� ���¿� ����
     * FPS�� �޶����⿡ ������ �ð����� ���� ���Ѵٰ� �� �� ����.
     * 
     * �ݸ鿡 FixedUpdate�� ��� ������ �ֱ⸦ ������ ���� ���� �� �ֱ⶧���� ������ �� �ִ�
     * ������ ���� ����� ȭ�鿡 ��Ÿ�� �� �ִ�.
     * 
     * ������ Update ������ ���� �ָ� ����� �ӵ��� ������ ����� �ӵ��� �� �������� �� 
     * �� �ִµ� �̴� �����Ӵ� ���� �������� �ð��� �ٸ��⶧���� �׷���.
     * 
     * 
     * 
     */

   
    public enum Mode
    {
        FORCE, POS, COMPLICATED_FORCE, COMPLICATED_FORCE_V2, IMPULSE
    }

    public bool useFriction;

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


    #region State Variable
    public bool enableShowGroundCheckCollider;
    float LastOnGroundTime = 0;

    #endregion

    private void OnDrawGizmos()
    {
        if(enableShowGroundCheckCollider)
            Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }


    // Start is called before the first frame update
    void Start()
    {
     
    }

    private void Update()
    {
        LastOnGroundTime += Time.deltaTime;
        if (isOnGround())
        {
            Jump();
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

        // ������ ����
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
        float targetSpeed = dir * maxSpeed; //������ �̵� => maxSpeed, ���� �̵� = -maxSpeed, ���� = 0

        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount); // ���� speed ���� targetSpeed���� ���̿��� lerpAmount ������ŭ�� ���� ��ȯ, ������ ũ�� Ŭ���� �����Ӵ� �� ���� �̵��Ѵ�.

       

        float accelRate;

        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount; // �̵��� ��� runAccelAmount��ŭ rate�� ����, ������ ��� runDeccelAmount��ŭ rate ����

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.velocity.x; //(�����ΰ���) - ��ǥ �ӵ����� ���� ���� �������� ���� �پ��� ��ǥ �ӵ��� ������������ ���� �ο����� ����
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
         
        //float movement = speedDif * accelRate; // ������ �������� �ϰ� �ʹٸ� runAccelAmount ���� ũ�� ���ϱ�, ������ ������ �ϰ� �ʹٸ� runDeccelAmount���� ũ�� ���ϱ�

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);


        #endregion
        


        #region Friction
        if (useFriction)
        {
           
            if (dir == 0)
            {
                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

                amount *= Mathf.Sign(rb.velocity.x);

                //���� �������� ������� �ݴ��� ���� �ۿ��Ѵ�.
                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }

        }

        #endregion


    }

   



    void Jump()
    {
        // PressedDown�� �ѹ� ������ True������ ���� ���¸� �����ϰ� ������ False�� ���
        // Pressed�� ���� �������� �����Ǵ� �������� True�� ��ȯ
        // FixedUpdate�� ������ �ֱ⸦ ������ �������� ������Ʈ�� �����ϴµ�,
        // ���� �Է°����� FixedUpdate���� ����ϸ� �ֱⰡ Update�� �ٸ��� ��⶧����
        // �Է°����� �ֱⰡ �� PressedDown���� ��Ȯ�� �Է°����� ����
        // ���� Pressed�� ����ϰ� ��ƾ ó���� �ѹ��� �����ϵ��� ó���ؾ��Ѵ�.

        if (InputManager.manager.isPressedDownJump())
        {
            Debug.Log("JUMP");
            float force = jumpForce;
            


            // �߷¶����� ������ ���������� ���� �ӵ����� ���񰪸�ŭ ���� �������� ������
            if (rb.velocity.y < 0) 
            {
                force -= rb.velocity.y;
            }

            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        }

      
        





    }

    public bool isOnGround()
    {
        //RaycastHit2D fhit = Physics2D.Raycast(groundCheckPos.position, Vector3.forward, 1, maskGround); // z �������� raycast ����
        //RaycastHit2D bhit = Physics2D.Raycast(groundCheckPos.position, Vector3.back, 1, maskGround); // z �������� raycast ����
        //return fhit || bhit;

        // groundCheckPos���� groundCheckSize�� x��ŭ ���� ��������, y��ŭ ���� �������� �浹 ����
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, maskGround);

    }

}
