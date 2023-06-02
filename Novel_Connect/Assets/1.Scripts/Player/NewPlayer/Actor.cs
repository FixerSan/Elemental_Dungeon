using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Statuses statuses = new Statuses();
    public Elemental elemental = Elemental.Default;
    private void Awake()
    {
        Setup();
    }
    public abstract void Setup();
    public abstract void GetDamage(float damage);
    public abstract void SetTarget(GameObject target);
}
