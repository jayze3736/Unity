using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EnemyData/EnemyStatData", order = 1)]
public class EnemyStatData : ScriptableObject
{
    public float MAXHP { get { return hp; } }
    public float Damage { 
        get { if(damage < 0) return 0; else return damage;} }
    public float Mass { get { return (mass == 0) ? 1 : mass;  } }
    public float KnockBackTime { get { return knockBackTime; } }
    public float ACT { get { return attackSpeedTime; } }
    public float DeathAnimPlayTime { get { return deathAnimPlayTime; } }

    [SerializeField]
    float hp;

    [SerializeField]
    float damage;

    /// <summary>
    /// ���� - �˹�� ���ſ� ���� �˹� ������ ŭ
    /// </summary>
    [SerializeField]
    float mass;   

    /// <summary>
    /// �˹��� ����Ǵ� �ð�
    /// </summary>
    [SerializeField]
    float knockBackTime;

    /// <summary>
    /// ���� �ӵ�
    /// </summary>
    [SerializeField]
    float attackSpeedTime;

    /// <summary>
    /// ��� �ִϸ��̼� �÷��� �ð�
    /// </summary>
    [SerializeField]
    float deathAnimPlayTime;

    








}
