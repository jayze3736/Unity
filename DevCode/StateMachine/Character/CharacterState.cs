using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static으로 구현하는 이유는 각 State 별로 필드가 존재하지않기 때문에 각 객체끼리 구별할
/// 필요가 없다. 따라서 1개의 객체만 존재하면 되므로 static 객체로 구현
/// </summary>
class CharacterState
{
    public static AttackState attack = new AttackState();
    public static EquipState equip = new EquipState();
    public static MoveState move = new MoveState();
    public static IdleState idle = new IdleState();


}
