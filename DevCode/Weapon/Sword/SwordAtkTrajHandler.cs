using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using jsh;

/// <summary>
/// 원형으로 공격 판정을 계산하는 관리자
/// </summary>
[System.Serializable]
public class SwordAtkTrajHandler : JUnity
{
    public enum STEP
    {
        IDLE ,FIRST_STEP, SECOND_STEP, THIRD_STEP
    }


    Vector3 center;

    #region Custom Setting
    [Header("Guide Object Setting")]

    // 원의 반지름
    [SerializeField]
    double radius;

    // 회전 속도
    [SerializeField]
    double speed;

    // 첫번째 공격 가이드 궤적
    [SerializeField]
    GuideObject firstAtkGuide;

    // 두번째 공격 가이드 궤적
    [SerializeField]
    GuideObject secondAtkGuide;

    [Header("Arc Renderer Setting")]
    
    // 호를 구성하는 점들의 개수
    [SerializeField]
    int arc_resolution;
    
    // 호를 그리는 선 렌더러
    [SerializeField]
    LineRenderer lr;

    [SerializeField]
    float lineWidth = 0.7f;

    [Header("Attack Settting")]

    [SerializeField]
    float atk_distance;

    [SerializeField]
    float atk_delay;

    #endregion

    

    [SerializeField]
    ArcTrajGenerator arcGenerator;



    CircleTrajectoryController cirTrjController;
    RevoluteJoint arm;
    AttackHandler handler;
    Timer timer;
    Transform owner;
    STEP step;



    // Start is called before the first frame update
    public void JStart(Transform owner)
    {
        cirTrjController = new CircleTrajectoryController();
        arm = new RevoluteJoint();
        handler = new AttackHandler();
        timer = new Timer(atk_delay);

        //arcGenerator = new ArcTrajGenerator();

        
        this.owner = owner;
        arcGenerator.Init(arc_resolution, (float)radius);
        step = STEP.IDLE;
        ShowGuideObject();


    }


    /// <summary>
    /// 무기를 집었을때 일반 상태
    /// </summary>
    public void Idle()
    {
        // 첫번째 가이드 오브젝트는 시계방향으로 회전
        cirTrjController.CWRotate(firstAtkGuide);

        // 두번째 가이드 오브젝트는 첫번째 가이드 오브젝트가 나와야 활성화 되므로 비활성화 해둠
        secondAtkGuide.Disable();

        // 공격 스텝으로 넘어가기전에 첫번째 가이드 오브젝트의 상대위치를 저장한다.
        cirTrjController.SaveCurrentPosition(firstAtkGuide);


    }

    
    /// <summary>
    /// 무기를 집었을때 공격 상태
    /// </summary>
    public void Attack()
    {






    }

    



    // Update is called once per frame
    public bool JUpdate()
    {
        if(owner == null)
        {
            return true;
        }

        center = owner.position;
        cirTrjController.Init(center, radius, speed);
 
        

        switch (step)
        {
            case STEP.IDLE:
                
                cirTrjController.CWRotate(firstAtkGuide);

                // -두번째 가이드 오브젝트는 첫번째 가이드 오브젝트가 나와야 활성화 되므로
                // 비활성화 해둠
                secondAtkGuide.Disable();

                // 다음 스텝으로 넘어가기전에 첫번째 가이드 오브젝트의 위치를 기억함
                cirTrjController.SaveCurrentPosition(firstAtkGuide);

                MoveNextState();

                // 여기가 문제 여기는 idLE 여야하는데 
                return true;
            case STEP.FIRST_STEP:

                // 첫번째는 고정
                
                cirTrjController.Stop(firstAtkGuide, center);

                // 첫번째 가이드 오브젝트가 고정되었으므로 두번째를 활성화 시킴
                secondAtkGuide.Enable();

                // 고정되었으므로 두번째는 회전
                cirTrjController.CWRotate(secondAtkGuide);

                //arm.PointToPoint(center, firstAtkGuide.GetWorldPosition(), this.owner);

                MoveNextState();

                //여기는 Attack 임
                return false;

            case STEP.SECOND_STEP:


                // 공격 처리
                // 두 궤적 사이를 잇는 호의 궤적을 생성(애니메이션 재생)
                arcGenerator.DrawArc2D(firstAtkGuide.GetWorldPosition(), center, secondAtkGuide.GetWorldPosition(), lr, lineWidth);
                CoroutineHelper.helper.StartCoroutine(arcGenerator.FadeAwayArc2D(lr));

                Vector2 sDir = firstAtkGuide.GetWorldPosition() - center;
                CoroutineHelper.helper.StartCoroutine(arm.JointRotateCycle(sDir,arc_resolution, owner)); // change

                // 팔 움직임
               
                step = STEP.THIRD_STEP;

                return false;

            case STEP.THIRD_STEP:

                if (timer.Awake()) // 애니메이션이 지나면 로직 처리
                {
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

                    step = STEP.IDLE;
                    arm.PointToDir(Vector2.right, owner);

                }

                // 디버깅용 공격 범위 표시
                handler.VisualizeArcAtkRange(firstAtkGuide.GetWorldPosition(),
                  center, secondAtkGuide.GetWorldPosition(), arc_resolution, atk_distance, Color.white);

                return false;


        }

        return true;
       



    }


    void MoveNextState()
    {
        if (Input.GetKeyDown(InputManager.manager.KAttack))
        {
            step++;

        }
    }

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

    public STEP GetCurStep()
    {
        return step;
    }















}
