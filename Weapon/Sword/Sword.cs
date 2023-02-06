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

        // 원의 반지름
        [SerializeField]
        double radius;

        // 회전 속도
        [SerializeField]
        double speed;

        [Header("Arc Renderer Setting")]

        // 호를 구성하는 점들의 개수
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
        // 첫번째 공격 가이드 궤적
        [SerializeField]
        GuideObject firstAtkGuide;

        // 두번째 공격 가이드 궤적
        [SerializeField]
        GuideObject secondAtkGuide;

        // 호를 그리는 선 렌더러
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
            // 디버깅용 공격 범위 표시
            handler.VisualizeArcAtkRange(firstAtkGuide.GetWorldPosition(),
                center, secondAtkGuide.GetWorldPosition(), arc_resolution, atk_distance, Color.white);

        }

        public void MoveNextState()
        {
            //자신이 공격 상태일때 또 다시 Attack을 누르면 다음 공격 Step으로 넘어가도록 
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
            // 공격 범위안에 들어온 모든 적들의 정보를 가져옴(공격 로직 처리)
            Stack<Enemy> enemies = handler.DetectEnemyInArcRange(firstAtkGuide.GetWorldPosition(), center, secondAtkGuide.GetWorldPosition(), arc_resolution, atk_distance);

            // 모든 적들에게 공격 처리
            while (enemies.Count > 0)
            {

                handler.Attack(enemies.Pop(), 1); // HARD CODED

            }

            //초기화 시키기 위한 작업을 수행하고 다시 IDLE 상태로 이동
            secondAtkGuide.Disable(); // 다시 두번째 GuideObject를 비활성화
            cirTrjController.Release(firstAtkGuide);


            arm.PointToDir(Vector2.right, owner);
            
        }



        /// <summary>
        /// 검을 내려 찍을때 보여줄 시각 효과
        /// </summary>
        public void ShowVisualEffect()
        {
            // 공격 처리
            // 두 궤적 사이를 잇는 호의 궤적을 생성(애니메이션 재생)
            Vector3 center = owner.position;
            arcGenerator.DrawArc2D(firstAtkGuide.GetWorldPosition(), center, secondAtkGuide.GetWorldPosition(), lr, lineWidth);
            CoroutineHelper.helper.StartCoroutine(arcGenerator.FadeAwayArc2D(lr));

            Vector2 sDir = firstAtkGuide.GetWorldPosition() - center;
            CoroutineHelper.helper.StartCoroutine(arm.JointRotateCycle(sDir, arc_resolution, owner)); // change
                                                                                                      //CoroutineHelper.helper.StartCoroutine(arm.JointRotateCycle(sDir, arc_resolution, this.transform));
        }

        /// <summary>
        /// 무기를 들고 있을때 호출할 함수
        /// </summary>
        public override void IDLE()
        {

            #region VIEW
            Vector3 center = owner.position;
            cirTrjController.Init(center, radius, speed);

            // 첫번째 가이드 오브젝트는 시계방향으로 회전
            cirTrjController.CWRotate(firstAtkGuide);

            // 두번째 가이드 오브젝트는 첫번째 가이드 오브젝트가 나와야 활성화 되므로 비활성화 해둠
            Debug.Log("SWORD_IDLE IS OPERATING");
            secondAtkGuide.Disable();

            // 공격 스텝으로 넘어가기전에 첫번째 가이드 오브젝트의 상대위치를 저장한다.
            cirTrjController.SaveCurrentPosition(firstAtkGuide);
            #endregion

        }
        
        public void Ready()
        {
           
            Vector3 center = owner.position;
            cirTrjController.Stop(firstAtkGuide, center);

            // 첫번째 가이드 오브젝트가 고정되었으므로 두번째를 활성화 시킴
            secondAtkGuide.Enable();

            // 고정되었으므로 두번째는 회전
            cirTrjController.CWRotate(secondAtkGuide);
        }

        

        // 히트 판정
        // 


        // Start is called before the first frame update
        void Start()
        {
            //handler = new SwordAtkTrajHandler();

            // 처음 Sword가 인스턴스화 되면 GuideObject를 비활성화
            //handler.HideGuideObject();
            HideGuideObject();
            stateMachine = new SwordStateMachine();
            stateMachine.Start();

        }

        // Update is called once per frame
        void Update()
        {
            // 여기에는 로직과는 무관한(다른 객체와 통신과 무관한 것들을 작업)
            stateMachine.Update(this);
        }

        public override void PlayAnimation()
        {
            
        }
    }

}
