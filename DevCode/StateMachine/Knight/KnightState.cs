using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KnightState : IKnightState
{
    protected IKnightState prevState;

    public abstract IKnightState ChangeState(KnightPlayer knight);
    public abstract IKnightState Enter(KnightPlayer knight);
    public abstract void Exit(KnightPlayer knight);
    public abstract void FixedUpdate(KnightPlayer knight);
    public abstract void Update(KnightPlayer knight);

    
}
