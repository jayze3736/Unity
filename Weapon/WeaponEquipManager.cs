using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace jsh
{

    /// <summary>
    /// 무기를 장착하기 위해 장착 메소드를 제공하는 함수
    /// </summary>
    [System.Serializable]
    public class WeaponEquipManager : JUnity
    {

        #region Parameter
        // 무기를 장착할 수 있는 거리
        [SerializeField]
        float equipDist;

        [SerializeField]
        Transform equipStartPos;
        #endregion

        Vector3 vInit;
        Transform owner;
        Weapon curWeapon;

        /// <summary>
        /// 무기의 소유자 transform 정보를 전달한다.
        /// </summary>
        /// <param name="owner"></param>
        public void JStart(Transform owner)
        {
            this.owner = owner;
            Init();

        }

        /// <summary>
        /// 장비 메소드
        /// </summary>
        /// <returns></returns>
        public bool JUpdate()
        {
            
            //Equip();
            return true;
        }

        /// <summary>
        /// 무기의 메소드이자 공격 메소드
        /// </summary>
        /// <returns></returns>
        public bool Attack()
        {
            return true;
            //if (curWeapon != null)
            //{
            //    return curWeapon.JUpdate();
            //}
            //else
            //{
            //    return true;
            //}


        }

        public void Init()
        {
            vInit = equipStartPos.position - owner.position;
        }

        void SetWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(owner);
            weapon.transform.localPosition = weapon.GetRelativeEqPoint();

            // 이 Weapon의 소유자를 '나'로 지정
            curWeapon = weapon;
            curWeapon.Init(owner);

        }

        // Manager는 입력 감지를 알필요없고 그냥 장비 기능만 알고 있으면 됨
        public bool Equip()
        {
            
            // 앞 방향에 물체를 관통하는 Raycast 사출
            RaycastHit2D[] rHits = Physics2D.RaycastAll(equipStartPos.position, Vector2.right, equipDist);


            // 뒤 방향에 물체를 관통하는 Raycast 사출
            RaycastHit2D[] lHits = Physics2D.RaycastAll(equipStartPos.position, Vector2.left, equipDist);


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


        //public bool Equip()
        //{

        //    Vector3 curPos = owner.position;
        //    Vector3 localPos = vInit + curPos;


        //    // 앞 방향에 물체를 관통하는 Raycast 사출
        //    RaycastHit2D[] rHits = Physics2D.RaycastAll(localPos, Vector2.right, equipDist);


        //    // 뒤 방향에 물체를 관통하는 Raycast 사출
        //    RaycastHit2D[] lHits = Physics2D.RaycastAll(localPos, Vector2.left, equipDist);


        //    foreach (RaycastHit2D hit in rHits)
        //    {
        //        Weapon wp = hit.transform.GetComponent<Weapon>(); // 문제
        //        if (wp != null)
        //        {
        //            Debug.Log("Set Weapon");
        //            SetWeapon(wp);
        //            return true;
        //        }

        //    }

        //    foreach (RaycastHit2D hit in lHits)
        //    {
        //        Weapon wp = hit.transform.GetComponent<Weapon>();
        //        if (wp != null)
        //        {
        //            Debug.Log("Set Weapon");
        //            SetWeapon(wp);
        //            return true;
        //        }

        //    }



        //    Debug.DrawRay(localPos, Vector3.right * equipDist, Color.white);
        //    Debug.DrawRay(localPos, Vector3.left * equipDist, Color.white);
        //    return false;




        //}




        //public bool Equip()
        //{
        //    if (Input.GetKeyDown(InputManager.manager.KEquip))
        //    {
        //        // 앞 방향에 물체를 관통하는 Raycast 사출
        //        RaycastHit2D[] rHits = Physics2D.RaycastAll(curPos, Vector2.right, equipDist);


        //        // 뒤 방향에 물체를 관통하는 Raycast 사출
        //        RaycastHit2D[] lHits = Physics2D.RaycastAll(curPos, Vector2.left, equipDist);


        //        foreach (RaycastHit2D hit in rHits)
        //        {
        //            Weapon wp = hit.transform.GetComponent<Weapon>(); // 문제
        //            if (wp != null)
        //            {
        //                Debug.Log("Set Weapon");
        //                SetWeapon(wp);
        //                return true;
        //            }

        //        }

        //        foreach (RaycastHit2D hit in lHits)
        //        {
        //            Weapon wp = hit.transform.GetComponent<Weapon>();
        //            if (wp != null)
        //            {
        //                Debug.Log("Set Weapon");
        //                SetWeapon(wp);
        //                return true;
        //            }

        //        }
        //    }


        //    Debug.DrawRay(curPos, Vector3.right * equipDist, Color.white);
        //    Debug.DrawRay(curPos, Vector3.left * equipDist, Color.white);
        //    return false;




        //}


    }

}
