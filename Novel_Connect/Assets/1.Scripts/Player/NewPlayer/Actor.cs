using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : HitableObejct
{
    public Statuses statuses = new Statuses();
    public Transform hpBarPos;
    public Elemental elemental = Elemental.Default;
    private void Awake()
    {
        Setup();
    }
    public abstract void Setup();
    public abstract void SetTarget(GameObject target);
}
