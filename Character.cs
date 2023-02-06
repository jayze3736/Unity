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
        // inspector에서 설정하면 객체가 새로 생긴다?
        admin = new StepAdmin();

        wpManager.JStart(arm.transform);
        movement.JStart(transform);

        // 가입 순서에 따라서 처리 루틴이 달라짐
        admin.Subscribe(wpManager.Attack); // 먼저 공격을 우선시한다. 즉, 자신이 공격중이라면 다른 행동은 하지 못한다.
        admin.Subscribe(wpManager.JUpdate); // 자신이 공격을 하지 않고 있다면 장비 장착을 허락한다.
        admin.Subscribe(movement.JUpdate); // 자신이 공격범위를 설정했다면 이동이 불가능하다.


    }

    // Update is called once per frame
    void Update()
    {

        wpManager.Attack();
        wpManager.JUpdate();



        //admin.Operate();
        
        


        // 현재 먹은게 Sword이다. -> SwordAtkTrajHandler가 필요
        // 전투중에 다른 작업을 금지한다. 예를 들면, 무기로 공격이 끝나지 않았는데 무기를 교체하는 경우나 전투중에 상점을 클릭한다던가 등등
    }

    void FixedUpdate()
    {
        movement.JUpdate();


    }
}
