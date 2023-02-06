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
        /// OnValidate로 계산을 해야할지도: Trasnform을 가져옴 -> Transform의 Pos를 해당 값으로 Inspector에 업데이트
        /// </summary>

        [Header("Should be relative by owner")]
        [SerializeField]
        Vector3 eqPoint;

        // 데미지
        int wpDmg;
        // 공격 속도
        int wpSpeed;

        /// <summary>
        /// 무기의 능력치를 초기화 또는 설정한다. 
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

