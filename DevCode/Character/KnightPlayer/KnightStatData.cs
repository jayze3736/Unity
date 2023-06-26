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
    /// �ӽ������� KnockBackPower�� Stat��, �����δ� ������ KnockBackPower�� ���� �޶�������
    /// </summary>
    public float KnockBackPower;

    public float AttackCoolTime { get => attackCoolTime; }

    #region Cool Time
    [Range(0f, 2f)]
    [SerializeField]
    public float attackCoolTime;

    #endregion




}
