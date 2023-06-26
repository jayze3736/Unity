using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsh
{

    /// <summary>
    /// 키보드의 각 키는 누르면 정해진 기능을 수행해야함. 예를 들어, 무기를 장착할때 어떤 무기는 E로 장착하고 어떤 무기는 Q로 장착하는 경우가 발생하면 안됨
    /// 두번째 역할은 입력키가 들어오면 해당 입력키의 반응을 여러개 가지는 오브젝트들 중에서 어떤 오브젝트에 이벤트를 전달해야할지 알려줌
    /// </summary>
    /// 

    public class InputManager : MonoBehaviour
    {

        public static InputManager manager;

        

        // Default Keycodes
        KeyCode kEquip;
        KeyCode kRightMove;
        KeyCode kLeftMove;
        KeyCode kUpMove;
        KeyCode kDownMove;
        KeyCode kAttack;

        public KeyCode KEquip { get => kEquip; protected set => kEquip = value; }
        public KeyCode KRightMove { get => kRightMove; protected set => kRightMove = value; }
        public KeyCode KLeftMove { get => kLeftMove; protected set => kLeftMove = value; }
        public KeyCode KJump { get => kUpMove; protected set => kUpMove = value; }
        public KeyCode KCrouch { get => kDownMove; protected set => kDownMove = value; }
        public KeyCode KAttack { get => kAttack; protected set => kAttack = value; }

        private void Awake()
        {
            if(manager != null)
            {
                Destroy(manager.gameObject);
            }

            if (manager == null)
                manager = this;
        }

        // Start is called before the first frame update
        void Start()
        {
           Init();


        }

      

        void Init()
        {
           
            kEquip = KeyCode.E;
            kRightMove = KeyCode.RightArrow;
            kLeftMove = KeyCode.LeftArrow;
            kUpMove = KeyCode.UpArrow;
            kDownMove = KeyCode.DownArrow;
            kAttack = KeyCode.Space;
        }

        
       
        public bool isPressedEquip()
        {
            return Input.GetKey(kEquip);
        }

        public bool isPressedDownEquip()
        {
            return Input.GetKeyDown(kEquip);
        }

        public bool isPressedRMove()
        {
            return Input.GetKey(kRightMove);
        }

        public bool isPressedDownRMove()
        {
            return Input.GetKeyDown(kRightMove);
        }

        public bool isPressedLMove()
        {
            return Input.GetKey(kLeftMove);
        }

        public bool isPressedDownLMove()
        {
            return Input.GetKeyDown(kLeftMove);
        }

        public bool isPressedJump()
        {
            return Input.GetKey(KJump);
        }

        public bool isPressedDownJump()
        {
            return Input.GetKeyDown(KJump);
        }

        public bool isPressedUpJump()
        {
            return Input.GetKeyUp(KJump);
        }

        public bool isPressedCrouch()
        {
            return Input.GetKey(KCrouch);
        }

        public bool isPressedDownCrouch()
        {
            return Input.GetKeyDown(KCrouch);
        }

        public bool isPressedAttack()
        {
            return Input.GetKey(KAttack);
        }

        public bool isPressedDownAttack()
        {
            return Input.GetKeyDown(KAttack);
        }







    }
}

