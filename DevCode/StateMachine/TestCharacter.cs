using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using jsh;


namespace jsh
{
    public class TestCharacter : MonoBehaviour
    {
       
        CharacterStateMachine stateMachine;

        //public WeaponEquipManager wpManager;

        //public CharacterMove mvManager;

        public bool showGroundDetectCollider;


        [SerializeField]
        Arm arm;

        private void OnDrawGizmos()
        {
            if(showGroundDetectCollider)
                Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckSize.x, groundCheckSize.y, 0));

                

        }




        // Start is called before the first frame update
        void Start()
        {
            stateMachine = new CharacterStateMachine();
            stateMachine.Start();
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update(this);

            Debug.DrawRay(weaponDetectPos.position, Vector2.right * detectRange);
            Debug.DrawRay(weaponDetectPos.position, Vector2.left * detectRange);

           
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate(this);
        }

        #region Attack
        Weapon curWeapon;

        public bool Attack()
        {
            if (curWeapon != null)
            {
                //return curWeapon.Attack();
                return true;
            }
            else
            {
                return true;
            }


        }


        #endregion

        #region Equip
        [Header("# Weapon Equip")]

        [SerializeField]
        Transform weaponDetectPos;

        [SerializeField]
        float detectRange;


        void SetWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = weapon.GetRelativeEqPoint();

            curWeapon = weapon;
            curWeapon.Init(arm.transform);
            curWeapon.OnEquip();

        }

        // Manager는 입력 감지를 알필요없고 그냥 장비 기능만 알고 있으면 됨
        public bool Equip()
        {

            // 앞 방향에 물체를 관통하는 Raycast 사출
            RaycastHit2D[] rHits = Physics2D.RaycastAll(weaponDetectPos.position, Vector2.right, detectRange);


            // 뒤 방향에 물체를 관통하는 Raycast 사출
            RaycastHit2D[] lHits = Physics2D.RaycastAll(weaponDetectPos.position, Vector2.left, detectRange);


            foreach (RaycastHit2D hit in rHits)
            {
                Weapon wp = hit.transform.GetComponent<Weapon>();
                if (wp != null)
                {
                    Debug.Log("Set Weapon");
                    SetWeapon(wp);
                    return true;
                }

            }

            foreach (RaycastHit2D hit in lHits)
            {
                Weapon wp = hit.transform.GetComponent<Weapon>();
                if (wp != null)
                {
                    Debug.Log("Set Weapon");
                    SetWeapon(wp);
                    return true;
                }

            }

            return false;




        }



        #endregion

        #region IDLE
        
        public void IDLE()
        {   
            


           
        }

        #endregion

        #region Move
        [Header("# MoveMent")]
        [SerializeField]
        Transform groundCheckPos;

        [SerializeField]
        Vector2 groundCheckSize;

        [SerializeField]
        LayerMask maskGround;

        public void TurnRight()
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }

        public void TurnLeft()
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
        }
        public bool isOnGround()
        {
            //RaycastHit2D fhit = Physics2D.Raycast(groundCheckPos.position, Vector3.forward, 1, maskGround); // z 방향으로 raycast 사출
            //RaycastHit2D bhit = Physics2D.Raycast(groundCheckPos.position, Vector3.back, 1, maskGround); // z 방향으로 raycast 사출
            //return fhit || bhit;
            
            // groundCheckPos에서 groundCheckSize의 x만큼 가로 방향으로, y만큼 세로 방향으로 충돌 검출
            return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, maskGround);

        }

        public void LMove()
        {


        }

        public void RMove()
        {


        }

        public void Jump()
        {

        }




        #endregion












    }

}
