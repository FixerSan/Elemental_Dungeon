using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Statuses<T> where T : class
{
    public float hp;
    public float speed;

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
