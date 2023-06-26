using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static���� �����ϴ� ������ �� State ���� �ʵ尡 ���������ʱ� ������ �� ��ü���� ������
/// �ʿ䰡 ����. ���� 1���� ��ü�� �����ϸ� �ǹǷ� static ��ü�� ����
/// </summary>
class CharacterState
{
    public static AttackState attack = new AttackState();
    public static EquipState equip = new EquipState();
    public static MoveState move = new MoveState();
    public static IdleState idle = new IdleState();


}
