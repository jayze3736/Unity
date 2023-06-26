using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace jsh
{
   
    public class Sword : Weapon
    {
        //[SerializeField]
        //SwordAtkTrajHandler handler;

        #region Parameter
        [Header("Guide Object Setting")]

        // ���� ������
        [SerializeField]
        double radius;

        // ȸ�� �ӵ�
        [SerializeField]
        double speed;

        [Header("Arc Renderer Setting")]

        // ȣ�� �����ϴ� ������ ����
        [SerializeField]
        int arc_resolution;

        [SerializeField]
        float lineWidth = 0.7f;

        [Header("Attack Settting")]

        [SerializeField]
        float atk_distance;

        [SerializeField]
        float atk_delay;
        #endregion


        #region Component
        // ù��° ���� ���̵� ����
        [SerializeField]
        GuideObject firstAtkGuide;

        // �ι�° ���� ���̵� ����
        [SerializeField]
        GuideObject secondAtkGuide;

        // ȣ�� �׸��� �� ������
        [SerializeField]
        LineRenderer lr;

        [SerializeField]
        ArcTrajGenerator arcGenerator;

        #endregion

        CircleTrajectoryController cirTrjController;
        RevoluteJoint arm;
        AttackHandler handler;
        Timer timer;
        State state;

        public SwordStateMachine stateMachine;


        public enum State
        {
            READY, SHOW_VISUAL, LOGIC
        }

        public double Radius { get { return radius; } }
        public Timer Timer { get { return timer; } }
        


      

        public void HideGuideObject()
        {
            firstAtkGuide.Disable();
            secondAtkGuide.Disable();
        }

        public void ShowGuideObject()
        {
            firstAtkGuide.Enable();
            secondAtkGuide.Enable();
        }


        public override void OnEquip()
        {
            stateMachine.OnEquip();
            
            cirTrjController = new CircleTrajectoryController();
            arm = new RevoluteJoint();
            handler = new AttackHandler();
            timer = new Timer(atk_delay);


            arcGenerator.Init(arc_resolution, (float)radius);
            ShowGuideObject();


        }

        public override void OnUnEquip()
        {

        }

       
        public void VisualizeAttackRange()
        {
           
            Vector3 center = owner.position;
            // ������ ���� ���� ǥ��
            handler.VisualizeArcAtkRange(firstAtkGuide.GetWorldPosition(),
                center, secondAtkGuide.GetWorldPosition(), arc_resolution, atk_distance, Color.white);

        }

        public void MoveNextState()
        {
            //�ڽ��� ���� �����϶� �� �ٽ� Attack�� ������ ���� ���� Step���� �Ѿ���� 
            if (InputManager.manager.isPressedDownAttack())
            {
                state++;
                if (state > State.LOGIC)
                {
                    state = State.READY;
                }
            }
           

        }

        public void GiveDamage()
        {
            Vector3 center = owner.position;
            // ���� �����ȿ� ���� ��� ������ ������ ������(���� ���� ó��)
            Stack<Enemy> enemies = handler.DetectEnemyInArcRange(firstAtkGuide.GetWorldPosition(), center, secondAtkGuide.GetWorldPosition(), arc_resolution, atk_distance);

            // ��� ���鿡�� ���� ó��
            while (enemies.Count > 0)
            {

                handler.Attack(enemies.Pop(), 1); // HARD CODED

            }

            //�ʱ�ȭ ��Ű�� ���� �۾��� �����ϰ� �ٽ� IDLE ���·� �̵�
            secondAtkGuide.Disable(); // �ٽ� �ι�° GuideObject�� ��Ȱ��ȭ
            cirTrjController.Release(firstAtkGuide);


            arm.PointToDir(Vector2.right, owner);
            
        }



        /// <summary>
        /// ���� ���� ������ ������ �ð� ȿ��
        /// </summary>
        public void ShowVisualEffect()
        {
            // ���� ó��
            // �� ���� ���̸� �մ� ȣ�� ������ ����(�ִϸ��̼� ���)
            Vector3 center = owner.position;
            arcGenerator.DrawArc2D(firstAtkGuide.GetWorldPosition(), center, secondAtkGuide.GetWorldPosition(), lr, lineWidth);
            CoroutineHelper.helper.StartCoroutine(arcGenerator.FadeAwayArc2D(lr));

            Vector2 sDir = firstAtkGuide.GetWorldPosition() - center;
            CoroutineHelper.helper.StartCoroutine(arm.JointRotateCycle(sDir, arc_resolution, owner)); // change
                                                                                                      //CoroutineHelper.helper.StartCoroutine(arm.JointRotateCycle(sDir, arc_resolution, this.transform));
        }

        /// <summary>
        /// ���⸦ ��� ������ ȣ���� �Լ�
        /// </summary>
        public override void IDLE()
        {

            #region VIEW
            Vector3 center = owner.position;
            cirTrjController.Init(center, radius, speed);

            // ù��° ���̵� ������Ʈ�� �ð�������� ȸ��
            cirTrjController.CWRotate(firstAtkGuide);

            // �ι�° ���̵� ������Ʈ�� ù��° ���̵� ������Ʈ�� ���;� Ȱ��ȭ �ǹǷ� ��Ȱ��ȭ �ص�
            Debug.Log("SWORD_IDLE IS OPERATING");
            secondAtkGuide.Disable();

            // ���� �������� �Ѿ������ ù��° ���̵� ������Ʈ�� �����ġ�� �����Ѵ�.
            cirTrjController.SaveCurrentPosition(firstAtkGuide);
            #endregion

        }
        
        public void Ready()
        {
           
            Vector3 center = owner.position;
            cirTrjController.Stop(firstAtkGuide, center);

            // ù��° ���̵� ������Ʈ�� �����Ǿ����Ƿ� �ι�°�� Ȱ��ȭ ��Ŵ
            secondAtkGuide.Enable();

            // �����Ǿ����Ƿ� �ι�°�� ȸ��
            cirTrjController.CWRotate(secondAtkGuide);
        }

        

        // ��Ʈ ����
        // 


        // Start is called before the first frame update
        void Start()
        {
            //handler = new SwordAtkTrajHandler();

            // ó�� Sword�� �ν��Ͻ�ȭ �Ǹ� GuideObject�� ��Ȱ��ȭ
            //handler.HideGuideObject();
            HideGuideObject();
            stateMachine = new SwordStateMachine();
            stateMachine.Start();

        }

        // Update is called once per frame
        void Update()
        {
            // ���⿡�� �������� ������(�ٸ� ��ü�� ��Ű� ������ �͵��� �۾�)
            stateMachine.Update(this);
        }

        public override void PlayAnimation()
        {
            
        }
    }

}
