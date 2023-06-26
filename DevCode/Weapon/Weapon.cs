using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace jsh
{
    public abstract class Weapon : Item
    {

        protected Transform owner;
        public Transform Owner { get { return owner; } }


        /// <summary>
        /// OnValidate�� ����� �ؾ�������: Trasnform�� ������ -> Transform�� Pos�� �ش� ������ Inspector�� ������Ʈ
        /// </summary>

        [Header("Should be relative by owner")]
        [SerializeField]
        Vector3 eqPoint;

        // ������
        int wpDmg;
        // ���� �ӵ�
        int wpSpeed;

        /// <summary>
        /// ������ �ɷ�ġ�� �ʱ�ȭ �Ǵ� �����Ѵ�. 
        /// </summary>
        /// <param name="wpDmg"></param>
        /// <param name="wpSpeed"></param>
        public void Init(int wpDmg, int wpSpeed)
        {

        }

        public virtual void Init(Transform owner)
        {
            this.owner = owner;
        }


        public abstract void IDLE();
        

        public Vector3 GetRelativeEqPoint()
        {
            return eqPoint;
        }

        public abstract void PlayAnimation();


        public abstract void OnEquip();


        public abstract void OnUnEquip();


    }



}

