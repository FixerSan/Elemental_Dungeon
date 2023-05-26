using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Statuses<Actor> statuses = new Statuses<Actor>();
    public virtual void Setup()
    {
        statuses.Setup(this);
    }
    public abstract void Hit(float damage);
}
