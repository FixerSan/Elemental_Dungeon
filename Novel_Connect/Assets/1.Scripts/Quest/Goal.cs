using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public string description;
    public bool completed;
    public int currentAmount;
    public int requiredAmount;

    public virtual void Init()
    {

    }

    public void Evaluate()
    {
        if(currentAmount >= requiredAmount)
        {
            Complete();
        }    
    }
      
    public void Complete()
    {

    }
}
