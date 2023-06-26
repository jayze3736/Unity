using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using jsh;

/// <summary>
/// �������� ���� ������ ����ϴ� ������
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

    // ���� ������
    [SerializeField]
    double radius;

    // ȸ�� �ӵ�
    [SerializeField]
    double speed;

    // ù��° ���� ���̵� ����
    [SerializeField]
    GuideObject firstAtkGuide;

    // �ι�° ���� ���̵� ����
    [SerializeField]
    GuideObject secondAtkGuide;

    [Header("Arc Renderer Setting")]
    
    // ȣ�� �����ϴ� ������ ����
    [SerializeField]
    int arc_resolution;
    
    // ȣ�� �׸��� �� ������
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
    /// ���⸦ �������� �Ϲ� ����
    /// </summary>
    public void Idle()
    {
        // ù��° ���̵� ������Ʈ�� �ð�������� ȸ��
        cirTrjController.CWRotate(firstAtkGuide);

        // �ι�° ���̵� ������Ʈ�� ù��° ���̵� ������Ʈ�� ���;� Ȱ��ȭ �ǹǷ� ��Ȱ��ȭ �ص�
        secondAtkGuide.Disable();

        // ���� �������� �Ѿ������ ù��° ���̵� ������Ʈ�� �����ġ�� �����Ѵ�.
        cirTrjController.SaveCurrentPosition(firstAtkGuide);


    }

    
    /// <summary>
    /// ���⸦ �������� ���� ����
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

                // -�ι�° ���̵� ������Ʈ�� ù��° ���̵� ������Ʈ�� ���;� Ȱ��ȭ �ǹǷ�
                // ��Ȱ��ȭ �ص�
                secondAtkGuide.Disable();

                // ���� �������� �Ѿ������ ù��° ���̵� ������Ʈ�� ��ġ�� �����
                cirTrjController.SaveCurrentPosition(firstAtkGuide);

                MoveNextState();

                // ���Ⱑ ���� ����� idLE �����ϴµ� 
                return true;
            case STEP.FIRST_STEP:

                // ù��°�� ����
                
                cirTrjController.Stop(firstAtkGuide, center);

                // ù��° ���̵� ������Ʈ�� �����Ǿ����Ƿ� �ι�°�� Ȱ��ȭ ��Ŵ
                secondAtkGuide.Enable();

                // �����Ǿ����Ƿ� �ι�°�� ȸ��
                cirTrjController.CWRotate(secondAtkGuide);

                //arm.PointToPoint(center, firstAtkGuide.GetWorldPosition(), this.owner);

                MoveNextState();

                //����� Attack ��
                return false;

            case STEP.SECOND_STEP:


                // ���� ó��
                // �� ���� ���̸� �մ� ȣ�� ������ ����(�ִϸ��̼� ���)
                arcGenerator.DrawArc2D(firstAtkGuide.GetWorldPosition(), center, secondAtkGuide.GetWorldPosition(), lr, lineWidth);
                CoroutineHelper.helper.StartCoroutine(arcGenerator.FadeAwayArc2D(lr));

                Vector2 sDir = firstAtkGuide.GetWorldPosition() - center;
                CoroutineHelper.helper.StartCoroutine(arm.JointRotateCycle(sDir,arc_resolution, owner)); // change

                // �� ������
               
                step = STEP.THIRD_STEP;

                return false;

            case STEP.THIRD_STEP:

                if (timer.Awake()) // �ִϸ��̼��� ������ ���� ó��
                {
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

                    step = STEP.IDLE;
                    arm.PointToDir(Vector2.right, owner);

                }

                // ������ ���� ���� ǥ��
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
