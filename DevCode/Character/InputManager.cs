using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsh
{

    /// <summary>
    /// Ű������ �� Ű�� ������ ������ ����� �����ؾ���. ���� ���, ���⸦ �����Ҷ� � ����� E�� �����ϰ� � ����� Q�� �����ϴ� ��찡 �߻��ϸ� �ȵ�
    /// �ι�° ������ �Է�Ű�� ������ �ش� �Է�Ű�� ������ ������ ������ ������Ʈ�� �߿��� � ������Ʈ�� �̺�Ʈ�� �����ؾ����� �˷���
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

