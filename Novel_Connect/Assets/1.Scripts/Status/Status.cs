using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status <T> where T : class
{
    public bool isUsing = true;
    public float duration;
    protected float checkTime;
    public float damage;
    public abstract void Enter(T entity);
    public abstract void Update(T entity);
    public abstract void Exit(T entity);

    public  void CheckDuration()
    {
        if (checkTime < duration)
            checkTime += Time.deltaTime;
        else
        {
            checkTime = 0;
            isUsing = false;
        }
    }
}
