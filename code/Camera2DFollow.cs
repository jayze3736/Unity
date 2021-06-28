using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float yRestriction = -1.0f;


        float NextTimeToSearch = 0;
        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        

        // Update is called once per frame
        private void Update()
        {
            //만약 target이 없어지거나 없으면 어떠한 계산을 하지않도록 종료
            if (target == null)
            {
                // 먼저 target을 잃게 되면 SearchPlayer 함수가 호출되어 target을 재탐색하고 return한다.
                // 만약 재탐색 후에도 target이 null이라면 if 문을 벗어나지 못하고 return을 만나 Update 함수가 종료될 것이고
                // 재탐색 후에 target을 찾는다면 Update 문이 재호출되었을때 if문이 수행되지않고 아래의 계산이 진행될 것이다.
                // 이렇게 target을 찾는 이유는 처음에 Target에 첨부된 Player Prefab이 소멸되면 이후에 생성되는 Player Prefab 객체는
                // 완전히 다른 객체이기 때문이다. just like dynamic memory allocation
                SearchPlayer();
                return;
            }
                

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
            //카메라의 위치(transform.postion)이 newPos에 해당한다. 따라서 카메라의 위치에 제약을 두고싶으면 newPos을 수정하면 될 것이다.

            newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yRestriction, Mathf.Infinity), newPos.z);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }


        private void SearchPlayer()
        {
            if (NextTimeToSearch <= Time.time)
            {
                GameObject SearchResult = GameObject.FindGameObjectWithTag("Player");
                if (SearchResult != null)
                {
                    target = SearchResult.transform;
                }
                NextTimeToSearch = Time.time + 0.5f; //Delay: 0.5초, twice per frame, 한 프레임당 두번 if문이 수행됨
            }

        }
    }


   
}
