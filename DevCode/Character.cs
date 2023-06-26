using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using jsh;

/// <summary>
/// Main Process starts from here
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField]
    WeaponEquipManager wpManager;

    StepAdmin admin;

    [SerializeField]
    CharacterMove movement;

    [SerializeField]
    Arm arm;

    // Start is called before the first frame update
    void Start()
    {
        //wpManager = new WeaponEquipManager();
        // inspector���� �����ϸ� ��ü�� ���� �����?
        admin = new StepAdmin();

        wpManager.JStart(arm.transform);
        movement.JStart(transform);

        // ���� ������ ���� ó�� ��ƾ�� �޶���
        admin.Subscribe(wpManager.Attack); // ���� ������ �켱���Ѵ�. ��, �ڽ��� �������̶�� �ٸ� �ൿ�� ���� ���Ѵ�.
        admin.Subscribe(wpManager.JUpdate); // �ڽ��� ������ ���� �ʰ� �ִٸ� ��� ������ ����Ѵ�.
        admin.Subscribe(movement.JUpdate); // �ڽ��� ���ݹ����� �����ߴٸ� �̵��� �Ұ����ϴ�.


    }

    // Update is called once per frame
    void Update()
    {

        wpManager.Attack();
        wpManager.JUpdate();



        //admin.Operate();
        
        


        // ���� ������ Sword�̴�. -> SwordAtkTrajHandler�� �ʿ�
        // �����߿� �ٸ� �۾��� �����Ѵ�. ���� ���, ����� ������ ������ �ʾҴµ� ���⸦ ��ü�ϴ� ��쳪 �����߿� ������ Ŭ���Ѵٴ��� ���
    }

    void FixedUpdate()
    {
        movement.JUpdate();


    }
}
