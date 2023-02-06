using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jsh;

public class SwordStateMachine 
{
    //CharacterState state;
    ISwordState currentState;

    public SwordStateMachine()
    {
        currentState = null;
    }


    // Start is called before the first frame update
    public void Start()
    {
        //currentState = new Sword_IdleState();
    }

    public void OnEquip()
    {
        currentState = new Sword_IdleState();
    }

    // Update is called once per frame
    public void Update(Sword sword)
    {
        if(currentState != null)
        {
            currentState.Update(sword);
            currentState = currentState.ChangeState(sword);
        }
       

    }

    public void FixedUpdate(Sword sword)
    {
        if (currentState != null)
        {
            currentState.FixedUpdate(sword);
            currentState = currentState.ChangeState(sword);
        }
        
    }

}
