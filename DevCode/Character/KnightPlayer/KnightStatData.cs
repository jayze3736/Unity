using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "KnightData/KnightStatData", order = 1)]
public class KnightStatData : ScriptableObject
{
    [SerializeField]
    float maxHP;

    [SerializeField]
    float atk;

    public float MAXHP { get => maxHP; }
    public float ATK { get => atk; }

    /// <summary>
    /// 임시적으로 KnockBackPower를 Stat에, 실제로는 무기의 KnockBackPower에 따라서 달라져야함
    /// </summary>
    public float KnockBackPower;

    public float AttackCoolTime { get => attackCoolTime; }

    #region Cool Time
    [Range(0f, 2f)]
    [SerializeField]
    public float attackCoolTime;

    #endregion




}
