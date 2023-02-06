using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using jsh;

[RequireComponent(typeof(Rigidbody2D))]
[System.Serializable]
public class CharacterMove : JUnity
{
    InputManager instance;

    [SerializeField]
    bool posMode;

    [SerializeField]
    bool velMode;

    [SerializeField]
    bool forceMode;

    [SerializeField]
    float velocity;

    [SerializeField]
    Vector3 vGroundCheck;

    [SerializeField]
    LayerMask maskGround;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float jumpAbility;

    [SerializeField]
    float jumpTime;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float drag;

    Transform owner;
    Vector3 diff;

    Vector2 curVel;

    float curTime;
    
    // Start is called before the first frame update
    public void JStart(Transform owner)
    {
        instance = InputManager.manager;
        this.owner = owner;
        diff = vGroundCheck - owner.position;
        curTime = 0;

    }

    // Update is called once per frame
    public bool JUpdate()
    {
        
        return true;
       
    }

    void TurnRight()
    {
        Vector3 scale = owner.localScale;
        owner.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
    }

    void TurnLeft()
    {
        Vector3 scale = owner.localScale;
        owner.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
    }

    void Jump()
    {
        
        //Ground Check
        if (isGround())
        {

            #region using linear function
            //rb.velocity = Vector2.zero;
            //float yMax = owner.position.y + jumpHeight;
            //float y0 = owner.position.y;
            //CoroutineHelper.helper.StartCoroutine(Jump(y0, yMax));
            //Debug.Log("On Air");
            #endregion 

             curVel.y = Vector3.up.y * jumpAbility;
           
        }


    }

    

    IEnumerator Jump(float y0, float yMax)
    {

        int cnt = (int)(jumpTime / Time.fixedDeltaTime);
        Debug.Log("Cnt:" + cnt);
        float curTime = 0;

        for(int i = 0; i <= cnt; i++)
        {
            // 그냥 Lerp함수는 y0와 yMax를 잇는 직선상에서 t값에 해당하는 값을 반환함
            // yMax까지 무조건 도달하지 않음

            //float y = (yMax - y0) / (JumpTime - 0) (t - 0) + y0
            curTime += Time.fixedDeltaTime;
            float y = ((yMax - y0) / (jumpTime)) * curTime + y0;
            float x = owner.position.x;
            float z = owner.position.z;

            owner.position = new Vector3(x, y, z);

            yield return null;
            
        }




    }

    void Drag()
    {
        if (!isGround())
        {
            rb.velocity += Vector2.down * drag;
        }
    }



    bool isGround()
    {
        
        Vector3 localPos = owner.position + diff;
        RaycastHit2D fhit = Physics2D.Raycast(localPos, Vector3.forward, 1, maskGround); // z 방향으로 raycast 사출
        RaycastHit2D bhit = Physics2D.Raycast(localPos, Vector3.back, 1, maskGround); // z 방향으로 raycast 사출
        return fhit || bhit;
        
    }

    /* 점프의 경우에는 velocity를 조정하지만, 이동의 경우에는 현재 값이 불연속적으로 더해져서 이동함 
     * owner.position += Vector3.right * velocity; 형태로 더하면 매 프레임 업데이트마다 velocity의 값만큼 x+ 방향으로 위치값이 더해지는데
     * 이때 velocity가 만약 0.3이라면 매 프레임마다 0.3씩 더해지고 불연속적으로 값이 더해지기 때문에 부드럽게 만들어줄 필요가 있음
     * 
     */



    /// <summary>
    /// 동일 프레임에서 두 키 입력을 동시에 받아서 포물선 형태의 경로로도 캐릭터 조작이 가능해야함 -> 점프, 이동 둘다 가능해야함
    /// </summary>
    void HorizontalMoveWithPosUpdate()
    {
        //Vector3 localPos = owner.position + diff;
        //Debug.DrawRay(localPos, Vector3.forward, Color.red);
        //Debug.DrawRay(localPos, Vector3.back, Color.red);

        if (Input.GetKey(instance.KRightMove))
        {
            owner.position += Vector3.right * velocity;
            TurnRight();
           
        }

        if (Input.GetKey(instance.KLeftMove))
        {
            owner.position += Vector3.left * velocity;
            TurnLeft();
            

        }

      
        
    }

    void HorizontalMoveWithVelocity()
    {
        //Vector3 localPos = owner.position + diff;
        //Debug.DrawRay(localPos, Vector3.forward, Color.red);
        //Debug.DrawRay(localPos, Vector3.back, Color.red);

        if (Input.GetKey(instance.KRightMove))
        {
            owner.position += Vector3.right * velocity;
            //curVel.x = Vector2.right.x * velocity;
            TurnRight();

        }

        if (Input.GetKey(instance.KLeftMove))
        {
            owner.position += Vector3.left * velocity;
            //curVel.x = Vector2.left.x * velocity;
            TurnLeft();


        }



    }

    void HorizontalMoveWithForce()
    {
        //Vector3 localPos = owner.position + diff;
        //Debug.DrawRay(localPos, Vector3.forward, Color.red);
        //Debug.DrawRay(localPos, Vector3.back, Color.red);

        if (Input.GetKey(instance.KRightMove))
        {
            owner.position += Vector3.right * velocity;
            //curVel.x = Vector2.right.x * velocity;
            TurnRight();

        }

        if (Input.GetKey(instance.KLeftMove))
        {
            owner.position += Vector3.left * velocity;
            //curVel.x = Vector2.left.x * velocity;
            TurnLeft();


        }



    }








}
