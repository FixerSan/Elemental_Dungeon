using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T ownerEntity;
    public State<T> beforeState;
    private State<T> currentState;

    public void Setup(T owner, State<T> firstState)
    {
        ownerEntity = owner;
        ChangeState(firstState);
    }

    public void ChangeState(State<T> state)
    {
        if(currentState != null)
        {
            beforeState = currentState; 
            currentState.ExitState(ownerEntity);
        }
        currentState = state;
        currentState.EnterState(ownerEntity);
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState(ownerEntity);
        }
    }

    public void BackState()
    {
        ChangeState(beforeState);
    }

}
