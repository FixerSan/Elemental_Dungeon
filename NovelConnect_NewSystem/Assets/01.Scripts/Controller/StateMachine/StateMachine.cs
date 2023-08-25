using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T entity;
    private State<T> currentState;

    public void ChangeState(State<T> _state)
    {
        if (currentState != null)
        {
            currentState.ExitState(entity, () =>
            {
                currentState = _state;
                currentState.EnterState(entity);
            });
        }
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState(entity);
        }
    }

    public StateMachine(T _entity, State<T> _firstState)
    {
        entity = _entity;
        currentState = _firstState;
        currentState.EnterState(_entity);
    }
}