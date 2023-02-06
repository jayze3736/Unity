using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EnemyData/EnemyStatData", order = 1)]
public class EnemyStatData : ScriptableObject
{
    public float HP { get { return hp; } }
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
    /// 무게 - 넉백시 무거울 수록 넉백 저항이 큼
    /// </summary>
    [SerializeField]
    float mass;   

    /// <summary>
    /// 넉백이 진행되는 시간
    /// </summary>
    [SerializeField]
    float knockBackTime;

    /// <summary>
    /// 공격 속도
    /// </summary>
    [SerializeField]
    float attackSpeedTime;

    /// <summary>
    /// 사망 애니메이션 플레이 시간
    /// </summary>
    [SerializeField]
    float deathAnimPlayTime;

    








}
