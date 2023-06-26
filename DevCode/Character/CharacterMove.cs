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
            // �׳� Lerp�Լ��� y0�� yMax�� �մ� �����󿡼� t���� �ش��ϴ� ���� ��ȯ��
            // yMax���� ������ �������� ����

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
        RaycastHit2D fhit = Physics2D.Raycast(localPos, Vector3.forward, 1, maskGround); // z �������� raycast ����
        RaycastHit2D bhit = Physics2D.Raycast(localPos, Vector3.back, 1, maskGround); // z �������� raycast ����
        return fhit || bhit;
        
    }

    /* ������ ��쿡�� velocity�� ����������, �̵��� ��쿡�� ���� ���� �ҿ��������� �������� �̵��� 
     * owner.position += Vector3.right * velocity; ���·� ���ϸ� �� ������ ������Ʈ���� velocity�� ����ŭ x+ �������� ��ġ���� �������µ�
     * �̶� velocity�� ���� 0.3�̶�� �� �����Ӹ��� 0.3�� �������� �ҿ��������� ���� �������� ������ �ε巴�� ������� �ʿ䰡 ����
     * 
     */



    /// <summary>
    /// ���� �����ӿ��� �� Ű �Է��� ���ÿ� �޾Ƽ� ������ ������ ��ηε� ĳ���� ������ �����ؾ��� -> ����, �̵� �Ѵ� �����ؾ���
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
