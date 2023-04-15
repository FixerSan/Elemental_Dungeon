using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMachine <T> where T : class
{
    public List<Status<T>> curStatuses = new List<Status<T>>();

    public T entity;

    //private void Update()
    //{
    //    foreach (var item in currentStatuses)
    //    {
    //        if(item.isUsing)

    //    }
    
    public void SetStatus(Status<T> status, float duration, float damage)
    {
        status.damage = damage;
        status.duration = duration;
        status.isUsing = true;
        curStatuses.Add(status);
        status.Enter(entity);
    }

    public void Update()
    {
        foreach (var item in curStatuses)
        {
            if(item.isUsing)
                item.Update(entity);
        }   
    }

    public void ExitStatus(Status<T> status)
    {
        status.Exit(entity);
        curStatuses.Remove(status);
    }

    public void Setup(T entity_)
    {
        entity = entity_;
        
    }
}
