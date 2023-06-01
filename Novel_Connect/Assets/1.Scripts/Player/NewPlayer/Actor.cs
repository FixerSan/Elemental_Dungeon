using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Statuses<Actor> statuses = new Statuses<Actor>();
    public Elemental elemental;
    private void Awake()
    {
        Setup();
    }
    public abstract void Setup();
    public abstract void GetDamage(float damage);
}
