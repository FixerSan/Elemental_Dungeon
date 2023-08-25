using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class
{
    public abstract void EnterState(T _entity);
    public abstract void UpdateState(T _entity);
    public abstract void ExitState(T _entity, System.Action _callback);
}