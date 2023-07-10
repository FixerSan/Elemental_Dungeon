using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : HitableObejct
{
    public float hp;
    public override void GetDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            DestroyObejct();
        }
    }

    public override void Hit(float damage)
    {

    }

    void DestroyObejct()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
    }
}
