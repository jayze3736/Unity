using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �Ѵ� ī�޶� ������Ʈ
/// </summary>
/// 

namespace jsh
{
    public class CameraFollow : MonoBehaviour
    {
        // Ÿ���� ã�� �� �־����
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

