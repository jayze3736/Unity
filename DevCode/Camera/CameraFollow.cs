using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 쫓는 카메라 오브젝트
/// </summary>
/// 

namespace jsh
{
    public class CameraFollow : MonoBehaviour
    {
        // 타겟을 찾을 수 있어야함
        [SerializeField]
        Camera cam;

        [SerializeField]
        Transform target;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MoveCamera();
        }

        public void FindTarget()
        {

        }

        public void MoveCamera()
        {
            float x = target.position.x;
            float y = target.position.y;

            cam.transform.position = new Vector3(x,y,cam.transform.position.z); 



        }






    }
}

