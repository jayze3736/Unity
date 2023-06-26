using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace jsh
{

    /// <summary>
    /// ���⸦ �����ϱ� ���� ���� �޼ҵ带 �����ϴ� �Լ�
    /// </summary>
    [System.Serializable]
    public class WeaponEquipManager : JUnity
    {

        #region Parameter
        // ���⸦ ������ �� �ִ� �Ÿ�
        [SerializeField]
        float equipDist;

        [SerializeField]
        Transform equipStartPos;
        #endregion

        Vector3 vInit;
        Transform owner;
        Weapon curWeapon;

        /// <summary>
        /// ������ ������ transform ������ �����Ѵ�.
        /// </summary>
        /// <param name="owner"></param>
        public void JStart(Transform owner)
        {
            this.owner = owner;
            Init();

        }

        /// <summary>
        /// ��� �޼ҵ�
        /// </summary>
        /// <returns></returns>
        public bool JUpdate()
        {
            
            //Equip();
            return true;
        }

        /// <summary>
        /// ������ �޼ҵ����� ���� �޼ҵ�
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

            // �� Weapon�� �����ڸ� '��'�� ����
            curWeapon = weapon;
            curWeapon.Init(owner);

        }

        // Manager�� �Է� ������ ���ʿ���� �׳� ��� ��ɸ� �˰� ������ ��
        public bool Equip()
        {
            
            // �� ���⿡ ��ü�� �����ϴ� Raycast ����
            RaycastHit2D[] rHits = Physics2D.RaycastAll(equipStartPos.position, Vector2.right, equipDist);


            // �� ���⿡ ��ü�� �����ϴ� Raycast ����
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


        //    // �� ���⿡ ��ü�� �����ϴ� Raycast ����
        //    RaycastHit2D[] rHits = Physics2D.RaycastAll(localPos, Vector2.right, equipDist);


        //    // �� ���⿡ ��ü�� �����ϴ� Raycast ����
        //    RaycastHit2D[] lHits = Physics2D.RaycastAll(localPos, Vector2.left, equipDist);


        //    foreach (RaycastHit2D hit in rHits)
        //    {
        //        Weapon wp = hit.transform.GetComponent<Weapon>(); // ����
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
        //        // �� ���⿡ ��ü�� �����ϴ� Raycast ����
        //        RaycastHit2D[] rHits = Physics2D.RaycastAll(curPos, Vector2.right, equipDist);


        //        // �� ���⿡ ��ü�� �����ϴ� Raycast ����
        //        RaycastHit2D[] lHits = Physics2D.RaycastAll(curPos, Vector2.left, equipDist);


        //        foreach (RaycastHit2D hit in rHits)
        //        {
        //            Weapon wp = hit.transform.GetComponent<Weapon>(); // ����
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
