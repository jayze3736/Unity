using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public void Update(Enemy enemy);
    public IEnemyState ChangeState(Enemy enemy);
    public void Enter(Enemy enemy);
    public void Exit(Enemy enemy);






}
