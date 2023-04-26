using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses<T> where T : class
{
    public T entity;
    public bool isBurn = false;  
    
    public void Setup(T entity_)
    {
        entity = entity_;
    }
    public void ExitAllEffect()
    {
        isBurn = false;
    }
}
